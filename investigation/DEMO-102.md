# DEMO-102: Receipt shows wrong tax when a discount code is used

* Type: Bug
* Priority: Medium
* Component: order-service
* Version: 2.4.1
* Reporter: Support team

## Description

Customers who use a discount code see a higher tax amount on the receipt than expected.
Example: order A-2001 had items worth 100.00, a 20 percent discount and 10 percent tax.
The receipt shows tax 10.00 and total 90.00. Support expected tax 8.00 and total 88.00
Orders without a discount code look correct (for example order A-2002).

## Attachments

* app.log (order-service log for 2026-11-09)