# Async API Foundations

This note captures the groundwork decisions for introducing an asynchronous Blueprint41 API. It establishes the programming model, outlines how the existing synchronous surface will co-exist with async, inventories the remaining blocking sync entry points, and defines the baselines and rollout posture the next phases will build upon.

## Programming Model

- **Return types**: Library entry points that execute Cypher or mutate state will return `Task`/`Task<T>`. `ValueTask` is reserved for extremely hot paths where profiling proves the allocation reduction matters; all new signatures default to `Task` to preserve clarity and tooling support.
- **Streaming**: Result sets that today expose `IEnumerable<RawRecord>` will gain `IAsyncEnumerable<RawRecord>`. The `RawResult` abstraction will grow async-friendly members (`MoveNextAsync`, `ToListAsync`, etc.) while keeping the enumerator contract intact for synchronous consumers.
- **Cancellation**: Every async method that can block on I/O takes an optional `CancellationToken` with a default value of `default`. Tokens flow all the way into the Neo4j/Memgraph drivers so upstream callers can terminate long-running queries deterministically.
- **Context usage**: All awaited operations inside the library call `ConfigureAwait(false)` to avoid capturing ambient synchronization contexts. Public async methods never block synchronously (no `.Result`/`.Wait()`), and sync wrappers are the only sanctioned boundary where the async->sync transition occurs.

## Synchronous Wrapper Rules

- Existing synchronous methods stay public and delegate into the new async implementations by calling `GetAwaiter().GetResult()` on the underlying task. Guardrails:
  - Wrappers live in one place (e.g. `Session.Run` calls `RunAsync(...).GetAwaiter().GetResult()`), which keeps the blocking behavior centralized and easier to audit.
  - Wrappers document that they are *not* safe to use on threads with a synchronization context that disallows blocking (UI, ASP.NET request threads configured for async only).
  - Internal code is forbidden from calling the sync wrappers; new code must prefer async and allow the caller to decide if it wants to block.
- Where a method currently has multiple overloads (e.g. with/without parameter dictionaries) the async variant mirrors the shape so generated code continues to compile with minimal diff.

## Inventory of Blocking Sync Touch Points

The following surface areas still execute synchronously and must be updated during subsequent phases. Paths are workspace-relative.

- `Blueprint41/IStatementRunner.cs:10` – base interface exposes only synchronous `Run` signatures.
- `Blueprint41/Session.cs:15` – session lifecycle and query execution are synchronous; the type implements `IStatementRunner`.
- `Blueprint41/Transaction.cs:15` – transaction lifecycle, flush/commit, event dispatch are synchronous and implement `IStatementRunner`.
- `Blueprint41/Core/QueryExecutionContext.cs:18` – query batching, statistics capture, and cursor materialization occur synchronously.
- `Blueprint41/Core/PersistenceProvider.cs:24` – session/transaction factories return synchronous implementations only.
- Neo4j provider stacks:
  - `Blueprint41.Neo4jDriver.v5/Neo4j/Persistence/v5/Neo4jSession.cs:16` and `Neo4jTransaction.cs:16`
  - `Blueprint41.Neo4jDriver.v4/Neo4j/Persistence/v4/Neo4jSession.cs:16` and `Neo4jTransaction.cs:16`
  - `Blueprint41.Neo4jDriver.v3/Neo4j/Persistence/v3/Neo4jSession.cs:16` and `Neo4jTransaction.cs:16`
  - `Blueprint41.Neo4jDriver.Memgraph/Neo4j/Persistence/Memgraph/Neo4jSession.cs:13` and `Neo4jTransaction.cs:13`
- Void persistence scaffolding (`Blueprint41/Neo4j/Persistence/Void`) that backs the modeller and offline tools.
- Generated domain surface (`Blueprint41/DatastoreTemplates/*.tt`) currently emits only synchronous methods for CRUD and queries.

This list drives the backlog for Phase 1 and Phase 2 work; additions must be tracked here to remain in sync with the refactor scope.

## Baseline Performance & Thread-Safety Targets

- **Throughput**: Capture current timings for `Session.Begin`, `Transaction.Begin/Commit`, `Transaction.Flush`, and representative `Run` operations using the existing integration playlists (`tests-neo4j.playlist`, `tests-memgraph.playlist`). These timings become comparison baselines; async implementations must stay within ±10 % unless a regression is justified and documented.
- **Latency**: Measure single-query latency for small (≤100 records) and large (≥10 000 records) result sets to ensure the async pipeline does not introduce material per-call overhead.
- **Concurrency**: Document the current expectation that `Session` and `Transaction` instances are not thread-safe but are affinity-bound to a logical unit of work. The async implementation will preserve this model—callers still obtain per-thread/per-request scopes.
- **Event ordering**: Validate (and capture) the existing ordering guarantees around entity events (`RaiseOnSave`, `RaiseOnAfterSave`, etc.) so the async rewrite can assert the same sequence even when awaiting.
- **Resource usage**: Record the current connection pool behavior (max concurrent connections, retry policies). Async changes must not increase the minimum pool requirement or leak sockets; instrumentation hooks need to confirm this via load tests in later phases.

Baseline collection is owned by the test harness team; results will be stored alongside the playlists so we can automate regressions checks once async lands.

## Rollout Strategy

- **Parallel surface**: Ship the async API side-by-side with the existing synchronous methods. Sync remains the default for backward compatibility, and async methods are considered production-ready once the quality gates pass.
- **Opt-in code generation**: Introduce a generation switch (e.g. `GeneratorSettings.GenerateAsync`) so adopters can choose to emit async CRUD/query methods. Default is `false` for the first preview to avoid surprise diffs.
- **Preview cadence**: Produce a preview NuGet package with the async interfaces hidden behind an `AsyncApi` feature flag in configuration. The flag defaults to `false` and can be flipped per application or per session to trial the async path.
- **Telemetry & feedback**: Document guidance for early adopters to report issues (GitHub discussions/Issues tagged `async-api`). Instrumentation will include counters for async usage so we can monitor adoption.
- **GA criteria**: Async hits general availability once (1) all driver variants pass the integration suites, (2) performance stays within the defined baselines, (3) documentation and modeller support are updated, and (4) at least two real-world applications have validated the preview without blocking issues.

These foundations unblock Phase 1: the code-level interface extensions and the start of the persistence refactor.
