# DEMO-101: Checkout fails with "Payment timed out" at lunch time

* Type: Bug
* Priority: High
* Component: order-service
* Version: 2.4.1
* Reporter: QA team

## Description

Since Monday, about 5 to 10 percent of checkouts fail between 12:00 and 13:00 with the message "Payment timed out".
In the morning the same checkout always works. The payment gateway dashboard shows no errors on their side.

Example failed order: A-1040 at about 12:14 on 2026-11-09.

## Attachments

* app.log (order-service log for 2026-11-09)