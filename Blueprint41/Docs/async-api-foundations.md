# Async API Foundations

This note captures the groundwork decisions for introducing an asynchronous Blueprint41 API. It establishes the programming model, outlines how the existing synchronous surface will co-exist with async, inventories the remaining blocking sync entry points, and defines the baselines and rollout posture the next phases will build upon.

## Programming Model (current status vs. target)

- **Return types** *(done)*: Core entry points now expose `Task`/`Task<T>` alongside their synchronous counterparts. We still default to `Task` for clarity; `ValueTask` remains an optimization we will revisit when profiling calls for it.
- **Streaming** *(in progress)*: Raw results returned from `RunAsync` are still buffer-backed. We introduced `Neo4jCursorRawResult` to avoid `CustomTaskScheduler`, but it materialises records synchronously when enumerated. The future plan is to extend `RawResult` (or a sibling type) with `IAsyncEnumerable<RawRecord>` so large result sets can stream without full buffering.
- **Cancellation** *(done)*: Async methods accept a `CancellationToken` propagated down to the native driver calls. Synchronous wrappers still throw on cancelled tokens before blocking.
- **Context usage** *(partially done)*: Internal awaits on driver calls use `ConfigureAwait(false)`. Some sync wrappers still rely on `.GetAwaiter().GetResult()` by design; no new `.Result`/`.Wait()` calls were introduced.

## Synchronous Wrapper Rules

- Existing synchronous methods stay public and delegate into the new async implementations by calling `GetAwaiter().GetResult()` on the underlying task. Guardrails:
  - Wrappers live in one place (e.g. `Session.Run` calls `RunAsync(...).GetAwaiter().GetResult()`), which keeps the blocking behavior centralized and easier to audit.
  - Wrappers document that they are *not* safe to use on threads with a synchronization context that disallows blocking (UI, ASP.NET request threads configured for async only).
  - Internal code is forbidden from calling the sync wrappers; new code must prefer async and allow the caller to decide if it wants to block.
- Where a method currently has multiple overloads (e.g. with/without parameter dictionaries) the async variant mirrors the shape so generated code continues to compile with minimal diff.

## Inventory of Blocking Sync Touch Points

The following list tracks what has moved to async and what remains. Paths are workspace-relative.

- ✅ `Blueprint41/IStatementRunner.cs` – async overloads added.
- ✅ `Blueprint41/Session.cs` / `Blueprint41/Transaction.cs` – now expose async entry points; sync methods call into them.
- ⛔ `Blueprint41/Core/QueryExecutionContext.cs` – still materialises results synchronously; needs async evaluation/streaming rewrite.
- ⛔ `Blueprint41/Core/PersistenceProvider.cs` – factories still return sync-only implementations; multi-provider orchestration is untouched.
- Drivers:
  - ✅ v5/v4/Memgraph `Neo4jSession`/`Neo4jTransaction` – async paths call the native driver directly and return `Neo4jCursorRawResult` (no scheduler dependency). Sync paths remain blocking for back-compat.
  - ⛔ v3 driver – async wrappers still use `Task.Run` to bridge the synchronous Bolt client. We must replace this when we upgrade or drop v3 support.
- ⛔ Void persistence scaffolding (`Blueprint41/Neo4j/Persistence/Void`) – mock implementations are synchronous.
- ⛔ Generated domain surface (`Blueprint41/DatastoreTemplates/*.tt`) – codegen still produces sync-only CRUD/query methods.

This list drives the backlog for Phase 1 and Phase 2 work; additions must be tracked here to remain in sync with the refactor scope.

## Baseline Performance & Thread-Safety Targets

- **Throughput**: Capture current timings for `Session.Begin`, `Transaction.Begin/Commit`, `Transaction.Flush`, and representative `Run` operations using the existing integration playlists (`tests-neo4j.playlist`, `tests-memgraph.playlist`). These timings become comparison baselines; async implementations must stay within ±10 % unless a regression is justified and documented.
- **Latency**: Measure single-query latency for small (≤100 records) and large (≥10 000 records) result sets to ensure the async pipeline does not introduce material per-call overhead.
- **Concurrency**: Document the current expectation that `Session` and `Transaction` instances are not thread-safe but are affinity-bound to a logical unit of work. The async implementation will preserve this model—callers still obtain per-thread/per-request scopes.
- **Event ordering**: Validate (and capture) the existing ordering guarantees around entity events (`RaiseOnSave`, `RaiseOnAfterSave`, etc.) so the async rewrite can assert the same sequence even when awaiting.
- **Resource usage**: Record the current connection pool behavior (max concurrent connections, retry policies). Async changes must not increase the minimum pool requirement or leak sockets; instrumentation hooks need to confirm this via load tests in later phases.

Baseline collection is owned by the test harness team; results will be stored alongside the playlists so we can automate regressions checks once async lands.

## Rollout Strategy

- **Parallel surface** *(active)*: Ship async methods beside the sync ones. Sync remains the default entry point for consumers until streaming + tooling work land.
- **Opt-in code generation** *(todo)*: Add a `GeneratorSettings.GenerateAsync` switch once the generated surface can emit stable async methods.
- **Preview cadence** *(todo)*: Publish a preview package once the streaming story is ready and tests cover both Neo4j and Memgraph async paths.
- **Telemetry & feedback** *(todo)*: Document feedback channels and add usage counters before preview.
- **GA criteria** *(unchanged)*: Async reaches GA when driver variants pass integration suites, performance stays within baselines, documentation/toolling are synced, and early adopters validate real workloads without blockers.

With the core contracts now async-aware, the next milestones are (1) streaming support in `RawResult`, (2) query execution pipeline updates, (3) async code generation, and (4) retiring the `Task.Run` bridge in the v3 driver.

## Delivery Roadmap Status

### Phase 0 – Foundations
- **Async programming model** – Task-based methods and cancellation tokens are in place; streaming remains future work.
- **Inventory of sync touch-points** – Completed and reflected above; keeps the backlog updated.
- **Baseline performance/thread-safety targets** – Not yet collected; to be scheduled before preview.
- **Rollout strategy** – Documented; awaits telemetry and preview gating implementation.

### Phase 1 – Core Contracts
- ✅ `IStatementRunner` async overloads landed.
- ✅ `Session` / `Transaction` expose async `Run` methods and honour cancellation.
- ⛔ Query execution (`QueryExecutionContext`) still synchronous; needs async batching/streaming work.

### Phase 2 – Drivers & Persistence
- ✅ v5/v4/Memgraph sessions/transactions use native driver async APIs and avoid `CustomTaskScheduler` on async paths.
- ⚠️ v3 driver still relies on `Task.Run` as a temporary bridge.
- ⛔ Persistence provider factory and retry/bookmark handling need a dedicated async audit.

### Phase 3 – Generated Surface
- ⛔ T4 templates and runtime partials remain sync-only.
- ⛔ Configurable async code generation pending.

### Phase 4 – Quality
- ⚠️ Unit/integration suites exercising async paths exist only at driver level; broader coverage (generated models, playlists) still needed.
- ⛔ Performance/soak tests and analyzer enforcement not yet implemented.
- ⛔ Modeller/tooling validation outstanding.

### Phase 5 – Release
- ⛔ Migration guidance, preview package, and telemetry plans pending.
- ⛔ Support playbooks to be prepared closer to preview.
