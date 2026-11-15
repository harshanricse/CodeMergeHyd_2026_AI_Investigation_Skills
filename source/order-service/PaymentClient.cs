// Sample code for the Code Merge hands-on. Rad-only: it is not meant to be built.
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderService
{
    public class PaymentOptions
    {
        public string GatewayUrl { get; set; }
        public int TimeoutSeconds { get; set; }
        public int MaxRetries { get; set; }
    }

    public class PaymentClient
    {
        private readonly HttpClient _httpClient;
        private readonly PaymentOptions _options;
        private readonly ILogger _log;

        public PaymentClient(HttpClient httpClient, PaymentOptions options, ILogger log)
        {
            _httpClient = httpClient;
            _options = options;
            _log = log;

            //Apply the configured payment timeout to the HttpClient
            _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
        }

        public async Task<PaymentResult> ChargeAsync(Order order)
        {
            _log.Info($"Calling gateway order={order.Id} timeoutMs={_http.Timeout.TotalMilliseconds}");
            var started = DateTime.UtcNow;
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_options.GatewayUrl, order.ToPaymentRequest());
                var elapsed = (DateTime.UtcNow - started).TotalMilliseconds;
                _log.Info($"Gateway replied order={order.Id} elapsedMs={elapsed:0} status={response.StatusCode}");
                return PaymentResult.From(response);
            }
            catch(TaskCanceledException)
            {
                var elapsed = (DateTime.UtcNow - started).TotalMilliseconds;
                _log.Warn($"Request cancelled order={order.Id} elapsedMs={elapsed:0} timeoutMs={_http.Timeout.TotalMilliseconds}");
                return PaymentResult.Timeout();
            }

    }
}