# BWDK C# SDK — Integration Guide for Agents

You are integrating **BWDK (Buy With DigiKala)** into a .NET project (ASP.NET Core / Minimal API / Blazor server) via this SDK. Read this file **first**, then consult the companion references below.

## BWDK constants

- **Host:** `https://bwdk-backend.digify.shop`
- **Auth scheme:** `MerchantAPIKeyAuth` — header `Authorization: <api_key>`.
- **Main namespace:** `bwdk_sdk` (API under `bwdk_sdk.Api`, models under `bwdk_sdk.Model`, client under `bwdk_sdk.Client`).
- **Solution:** `bwdk_sdk.sln` at the SDK root; library project in `src/bwdk_sdk/`, tests in `src/bwdk_sdk.Test/`.

## Companion references

| File                            | When to read                                              |
|---------------------------------|-----------------------------------------------------------|
| `src/bwdk_sdk/README.md`        | Authoritative install, auth wiring, and "Getting Started" example. (The top-level `README.md` only redirects here.) |
| `FLOWCHART.md`                  | Canonical order state machine. All callback/webhook branching MUST match these state names. |
| `docs/DefaultApi.md`            | Exact method names and signatures per endpoint.           |
| `docs/*.md`                     | Per-model reference (e.g. `docs/OrderCreate.md`).         |

Do **not** duplicate install or method-signature details here — fetch `src/bwdk_sdk/README.md`.

## Minimal wrapper pattern

```csharp
using bwdk_sdk.Api;
using bwdk_sdk.Client;
using bwdk_sdk.Model;

var config = new Configuration
{
    BasePath = "https://bwdk-backend.digify.shop",
};
config.ApiKey["Authorization"] = Environment.GetEnvironmentVariable("BWDK_API_KEY");

var api = new DefaultApi(config);
OrderCreateResponse order = api.OrderApiV1CreateOrderCreate(payload);
```

Method names are PascalCase and OpenAPI-generated (e.g. `OrderApiV1CreateOrderCreate`, `OrderApiV1ManagerVerifyCreate`). Look them up in `docs/DefaultApi.md`; do **not** guess.

## Integration invariants

1. **SDK only.** Never call BWDK with `HttpClient`, `RestSharp`, or `Flurl`.
2. **Callback flow.** After payment, BWDK redirects the customer to your `callback_url`. Load the order (`OrderApiV1ManagerRetrieve`), switch on `order.Status` per `FLOWCHART.md`, then call `OrderApiV1ManagerVerifyCreate` — `verify` is mandatory before `SHIPPED`.
3. **Errors.** Catch `bwdk_sdk.Client.ApiException`; inspect `.ErrorCode` (HTTP status) and `.ErrorContent`. Retry only on transport errors, never on 4xx.
4. **DI:** the SDK ships `IServiceCollectionExtensions` — register the client with `services.AddApi(config)` in `Program.cs` rather than `new DefaultApi(...)` per request.
5. **Pinning.** Pin a concrete version (matching a `vX.Y.Z` tag) in your `.csproj`; never use floating versions in production.

## Project conventions

- **ASP.NET Core:** inject `DefaultApi` (or `IDefaultApi` if you wrap it) into your controllers; do not `new` a client per request — `HttpClient` under the hood is pooled.
- **Async:** prefer the generated `*Async` variants in async pipelines; don't mix `.Result` / `.Wait()` (deadlock risk).
- **Nullability:** generated models use nullable reference types; enable `<Nullable>enable</Nullable>` in your `.csproj` to catch `null` issues at compile time.
