// Sample Code for the Code Merge hands-on. Read-only: it is not meant to be built.
using System;

namespace OrderService
{
    public class TaxCalculator
    {
        private readonly decimal _taxRate;
        private readonly ILogger _log;

        public TaxCalculator(decimal taxRate, ILogger Log){
            _taxRate = taxRate;
            _log = Log;
        }

        public decimal CalculateTax(decimal subtotal, decimal discount)
        {
            // Tax is calculated on the substotal.
            var tax = Math.Round(subtotal * _taxRate, 2);

            _log.Info($"subtotal={subtotal:0.00} discount={discount:0.00} taxRate={_taxRate} tax={tax:0.00}");
            return tax;
        }
    }
}