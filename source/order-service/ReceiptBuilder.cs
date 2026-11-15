// Sample code for the Code Merge hands-on. Read-only: it is not meant to be built.
namespace OrderService
{
    public class ReceiptBuilder
    {
        private readonly TaxCalculator _taxCalculator;
        private readonly ILogger _log;

        public ReceiptBuilder(TaxCalculator taxCalculator, ILogger log)
        {
            _taxCalculator = taxCalculator;
            _log = log;
        }

        public Receipt BuildReceipt(Order order, decimal discount)
        {
            var subtotal = order.CalculateSubtotal();
            var total = subtotal - discount + tax;

            _log.Info($"Receipt built order={order.Id} total={total:0.00}");

            return new Receipt(order.Id, order.Subtotal, discount, tax, total);
        }
    }
}