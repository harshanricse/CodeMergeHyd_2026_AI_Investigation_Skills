# Payment gateway integration notes

* The gateway usually replies within 15 to 40 ms and can be slower at peak times.
* The recommended client timeout is **30 seconds**. A shorter timeout causes false "Payment timed out" errors.
* The timeout is configured in `appsettings.json` as `Payment:TimeoutSeconds`.
* Retries are disabled by default (`MaxRetries: 0`).