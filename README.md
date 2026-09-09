# Invoice Mock Application
This is a simplified mock version of an invoice validating application I built for a prior employer. It does not contain proprietary business logic or data, and is instead meant to demonstrate my coding style/abilities and means of problem solving.

## Business Problem Statement
When selling wine in the US, you are not allowed to sell directly to a retailer. Instead, you must sell to a distributor who will in turn sell the wine to a retailer for you. When a distributor sells a product, that is called a depletion. To incentivize distributors to sell our product, our business would offer reimbursements at a rate per case depleted. Therefore, at the end of the month a distributor would send us an invoice stating which products were depleted, how many cases, and the agreed reimbursement rate.
This is all fine, except distributors often are not accurate in what they ask for. So I built an application to support Finance in validating these invoices.

## Requirements
- Load and persist datasets from vendors to support validation
- Provide a standard template for loading invoices
- Provide line by line validation on an invoice, whether a line passes or fails, and a reason for failure
- Export exceptions (lines that failed) into a common template that can be emailed to stakeholders to make decisions as to what rate should be paid or if it should be paid at all. This template can then be imported back to upload changes
- Export finished invoices to ERP
- Access granted only to authenticated users via Azure/Windows
- Utilize custom in-house authorization API to manage permissions within the application

## Application Layers
- SQL Server database
- C#.NET Web API hosted in IIS
- Blazor Web Assembly Front-end hosted in IIS

## Datasets
Three primary datasets are needed to perform invoice validation.

### Depletions
Distributors report depletions to a third-party data vendor. We would receive this data and load into a data warehouse nightly.

### Pricing
Our pricing is maintained by a third-party pricing vendor, which would send us daily exports that would be loaded into a data warehouse nightly.

### Invoices
Invoices would be loaded manually into the application by the finance team when they are received.

# Solution Project: Invoice_SQL (SQL Project)
This is a standard relational database. This mock version only contains the essentials; the real application necessitated many tables to support various and nuanced validation logic. Since this does not contain real data, there are several functions and stored procedures to assist with randomly generating fake data.

# Solution Project: Invoice_Logic (C# Class Library Project)
This contains all primary business logic and is the core of the application. The best way to understand this layer is to follow the data from the database all the way up to the API

## Data Persistence - SQL
As stated earlier, this is a standard relational database. Depletions are stored in dbo.CaseSummary, Pricing in dbo.PriceDeal, and Invoices split between dbo.InvoiceHeader and dbo.InvoiceDetail. The main interest is the ProcessInvoices stored procedure which has all the business rules on validating an invoice line by line. These rules are in a stored procedure instead of .NET because the datasets are large, and it was more efficient to bring the logic directly to the data instead of loading data in the API.

## Entity Framework/Dapper
EF Core is the primary ORM that I use. Dapper is used occasionally for easy DTO mapping. I use a database-first approach and therefore do not use EF migrations. This gives me more control over the data at the cost of some manual work when it comes to schema changes.

## Db Entity (Repositories\DbEntities)
This layer is responsible for CRUD operations against the database. Generally speaking, I don't keep interfaces in separate folders; but I do here in case the ORM changes from EF Core to something else like Dapper. Regardless of which ORM is used, ORM coupling is kept entirely within the Db Entity classes; only DTO's are exposed to any layers above.

DbEntity classes are separated by data domain, and they do not have any dependencies between each other. Also, all DbEntity classes work under a UoW approach to caching changes and committing only when SaveChanges is called in the IRepository implementation. IRepository is not meant to be a true repository, as EF Core already fulfills that role, and is instead meant to coordinate several sets of classes when data is saved to the db.

Because DbEntity caches changes, Update and Create methods return a Late Loader object that initially is empty. This object will be populated with results after SaveChanges. This is useful for retrieving Db generated values like auto-increment ids.

## Cache Entity (Repositories\CacheEntities)
This layer is responsible for keeping the cache update to date as data is queried and changed. Similar to DbEntity, it uses a UoW approach and cache changes and comitting only when SaveChanges is called in the IRepository. All Cache Entity classes inherit from CacheEntity
