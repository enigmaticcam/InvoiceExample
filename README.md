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
This layer is responsible for keeping the cache up to date as data is queried and changed. Similar to DbEntity, it uses a UoW approach and caches changes that are only committed when SaveChanges is called in the IRepository. All Cache Entity classes inherit from CacheEntity to standardize how data is stored in the cache. Also similar to DbEntity, data domains without Cache Entity do not depend on each other.

Each domain object is stored by id in a set so that an entire domain set can be cleared without needing to know what it contains. A list of objects can be retrieved by a list of id's and only what is not in the cache will be requested from the database (supporting db entity object). A list of id's can also be stored together so that the db need not be queried at all when displaying a list of objects.

## Core
Core contains primary business logic. Each core class can only use the Cache Entities within its domain, but any Core class can use other Core classes. Core functions that are complex and more than a few lines long will be moved to an action class to keep the Core class simple.

## API
This is not the Web API, but rather an API into the class library. All core business logic is reduced to a single API interface (IAPICaller) where cross-application logic is relegated via pipeline classes. Essentially, anything that needs to happen for every API call will have its own class in the pipeline, such as exception logging, api logging, security authorization checks, etc. All API calls are wrapped in a Result object. This Result object is designed only to capture business logic errors (handled or unhandled); web api will still return standard HTTP errors otherwise.

# Solution Project: Invoice_API (SQL Project)
This is the Minimal Web API project. There should be no business logic here except only to satisfy necessary web API tasks. Otherwise, everything is delegated via IAPICaller.

# Solution Project: Invoice_BlazorWASM
Blazor Web Assembly project assisted by MudBlazor.

## ServiceClient / ServiceWrapper (Services\Core)
I use NSwagStudio to generate the ServiceClient class that is used for calling all the API functions. API specification does not support generics, such as Result<T>, so the ServiceWrapper class will convert all the ServiceClient classes into the Result<T> equivalent (although in this projected it's BlazorResult<T>).

## Entities (Services\Entities)
EntityState persists data by domain and has functions that allow for adding/updating/removing data after it has changed via an API call.

## ServerCommand (Services\ServerCommand)
Several classes work together to update the UI during API calls using a Command pattern.

### Requirements:
- When an API call is made, multiple controls might need to be disabled on any given page
- Multiple API calls might be made concurrently, where order of completion is not guaranteed and any of which might return a fail result
- All error results need to be captured and displayed to the user

### Classes:
- IServerCommand: Interface implemented by a command
- ServerStatus: Broadcast when the concurrent API count increases from 0 to 1, or when it decreases from 1 to 0.
- ServerInvoker: Implement the command pattern by invoking ServerStatus before executing a command, then executing the command, then invoking the ServerStatus again afterward
- BroadcastToken: lightweight event token used by any component that needs to subscribe to API-call events

# Solution Project: Invoice_WPF & Invoice_Avalonia
These projects are an attempt to teach myself WPF, MMV, Avalonia, and ReactiveUI.

# Conclusion
While this might seem like over-engineering, the actual version of this application was much more complex and had many other business layers. I used these concepts across all my applications, and it was always easy to incorporate them into new designs that had significantly different objectives.
