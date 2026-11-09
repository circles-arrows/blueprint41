# Copilot Instructions

## Project Guidelines
- Blueprint41 Async flavor: sync LazyGet/LazySet throw via EnsureFlavor; async properties use LazyGetNoLoad/LazySetNoLoad; key registration is delayed until after load for Async. Concurrent load races are not guarded in-process; use locked loads (LoadAsync(key, locked: true)) for that.