# Summary
The checkout failures are caused by the payment client timing out after ~30 ms instead of the intended 30 seconds. The gateway itself is healthy; the client cancels requests before the gateway responds, which matches the midday spike when response latency occasionally exceeds the cutoff.

# Timeline
- 2026-11-09 11:58:22: morning checkout succeeded.
- 2026-11-09 12:14:07: failed order A-1040 starts.
- 2026-11-09 12:14:08.170: PaymentClient logs `timeoutMs=30` before the request is sent.
- 2026-11-09 12:14:08.201: PaymentClient logs `Request cancelled ... elapsedMs=31 timeoutMs=30` and the order fails with `Payment timed out`.
- 2026-11-09 12:14:13.502: GatewayMonitor reports normal gateway latency (`avg=20ms p95=41ms`), which confirms the gateway is not failing.

# Evidence (good vs bad case)
- Good case: order A-1041 at 12:14:09 completed successfully with `Gateway replied ... elapsedMs=28 status=OK`.
- Bad case: order A-1040 at 12:14:07 failed with `Request cancelled ... elapsedMs=31 timeoutMs=30` and then `Checkout failed ... reason="Payment timed out"`.
- Good case: the same gateway remained healthy in the morning, with checkout A-1003 completing normally at 11:58.
- The variance is small but enough: gateway latency is normally 15-40 ms, and the configured cutoff of 30 ms causes false failures when the request lands just over that threshold.

# Root cause
The deployed client timeout is effectively 30 milliseconds, not 30 seconds. The source code intends to use `Payment:TimeoutSeconds` and convert it to a `TimeSpan` in seconds, but the runtime logs show the timeout value being treated as 30 ms: `timeoutMs=30` and `elapsedMs=31`, followed by `Request cancelled ... timeoutMs=30` in [../investigation/attachments_DEMO-101/app.log](../investigation/attachments_DEMO-101/app.log). The intended contract is documented in [../docs/payment-gateway.md](../docs/payment-gateway.md), which explicitly states that the recommended timeout is 30 seconds and that a shorter timeout causes false "Payment timed out" errors.

The code path that applies the timeout is in [../source/order-service/PaymentClient.cs](../source/order-service/PaymentClient.cs), where the client sets `HttpClient.Timeout` from the configured timeout and later logs the same timeout value. The configuration contract is in [../source/order-service/appsettings.json](../source/order-service/appsettings.json), which is meant to hold `"TimeoutSeconds": 30` (30 seconds), not 30 milliseconds.

# Fix recommendation
- Correct the payment timeout configuration to 30 seconds (or `30000` ms in any direct millisecond configuration).
- Validate the timeout in logs as `timeoutMs=30000` instead of `30` before rollout.
- Keep the gateway monitoring in place to detect future latency regressions; the issue is a client-side timeout, not a payment-gateway outage.

# Confidence
High. The evidence shows the gateway is healthy and the failure begins only when the client cancels requests after 31-34 ms, which is consistent with a 30 ms timeout.

# Missing evidence
We do not have the live deployment config for the production order-service instance. If the live appsettings or environment override was set to `30` instead of `30` seconds, that would explain the exact runtime behavior. The repo files confirm the intended contract, while the deployment artifact is the missing piece needed to prove the exact misconfiguration source.
