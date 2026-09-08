# Invoice Mock Application
This is a mock version of an invoice validating application I built for a prior employer. It does not contain proprietary business logic or data, and is instead meant to demonstrate my coding style/abilities and means of problem solving

## Business Problem statement
When selling wine in the US, you are not allowed to sell directly to a retailer. Instead, you must sell to a distributor who will in turn sell the wine to a retailer for you. When a distributor sells a product, that is called a depletion. To incentivize distributors to sell our product, our business would offer reimbursements at a rate per case depleted. Therefore, at the end of the month a distributor would send us an invoice stating which products were depleted, how many cases, and the agreed reimbursement rate.
This is all fine, except distributors often are not accurate in what they ask for. So I built an application to support Finance in validating these invoices.

## Requirements
- Load and persist datasets from vendors to support validation
- Provide an easy way to load invoices
- Provide line by line validation on an invoice, whether a line passes or fails, and a reason for failure
- Export exceptions into common template used by Commercial Finance, whi
- Extract completed invoice 

Three primary datasets are used for validation:

### Depletions
Distributors report depletions to a third-party data vendor. We would receive this data and load into a data warehouse nightly.

### Pricing
Our pricing is maintained by a third-party pricing vendor, which would send us daily exports that would be loaded into a data warehouse nightly.

### Invoices
Invoices would be loaded manually into the application by the finance team when they are received.

