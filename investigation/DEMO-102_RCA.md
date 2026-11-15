# Summary
The receipt bug is caused by tax being calculated on the full subtotal even when a discount code has already reduced the price. That matches the report: orders with a discount show the wrong tax and total, while undiscounted orders remain correct.

# Timeline
- 2026-11-09 10:05:01.100: order A-2002 without a discount calculates tax correctly (`subtotal=50.00 ... tax=5.00`).
- 2026-11-09 10:42:19.300: order A-2001 with a 20% discount starts checkout.
- 2026-11-09 10:42:19.311: `DiscountService` applies the discount (`discount=20.00`).
- 2026-11-09 10:42:19.322: `TaxCalculator` logs `subtotal=100.00 discount=20.00 taxRate=0.10 tax=10.00`.
- 2026-11-09 10:42:19.325: `ReceiptBuilder` writes a receipt total of `90.00`, which is the wrong value for a discounted subtotal.

# Evidence (good vs bad case)
- Good case: A-2002 without a discount logs `subtotal=50.00 discount=0.00 taxRate=0.10 tax=5.00` and finishes with total `55.00`.
- Bad case: A-2001 with a 20% discount logs `subtotal=100.00 discount=20.00 taxRate=0.10 tax=10.00` and then a receipt total of `90.00` instead of `88.00`.
- The pricing contract in [../docs/pricing-rules.md](../docs/pricing-rules.md) says tax is calculated on the discounted amount and `Total = subtotal - discount + tax`.
- The expected calculation in the doc is: 100.00 - 20.00 = 80.00; tax at 10% is 8.00; total is 88.00.

# Root cause (file and line)
The root cause is in [../source/order-service/TaxCalculator.cs](../source/order-service/TaxCalculator.cs): the method `CalculateTax(decimal subtotal, decimal discount)` ignores the `discount` parameter and calculates tax directly from the original subtotal:

- `// Tax is calculated on the substotal.` at [../source/order-service/TaxCalculator.cs](../source/order-service/TaxCalculator.cs#L16-L19)
- `var tax = Math.Round(subtotal * _taxRate, 2);` at [../source/order-service/TaxCalculator.cs](../source/order-service/TaxCalculator.cs#L18-L21)

This contradicts the required pricing rule in [../docs/pricing-rules.md](../docs/pricing-rules.md#L1-L4), which states that tax is computed on the discounted amount.

# Fix recommendation
- Change `CalculateTax` to compute tax on `subtotal - discount`, not on the original subtotal.
- Make sure the receipt builder uses that calculated tax value for both the receipt and total.
- Add a regression test for a discounted order like A-2001 to confirm tax = 8.00 and total = 88.00.

# Confidence
High. The log lines and the documented pricing rules line up exactly: the code uses the subtotal before discount while the expected business rule taxes the discounted amount.

# Missing evidence
We do not have the exact production deployment artifact for the order-service instance, so we cannot confirm whether an environment override or a generated build differs from the source file in the repo. The in-repo source and the app log are sufficient to prove the defect and its root cause.
