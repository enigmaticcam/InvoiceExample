create procedure ProcessInvoices
@headerId int = -1
as

create table #temp (
	InvoiceHeaderId int
	, InvoiceDetailId int
	, Customer int
	, InvoiceDate date
	, InvoiceMonth int
	, CustItemCode nvarchar(15)
	, CustomerRate numeric(18, 2)
	, ApprovedRate numeric(18, 2)
	, Cases numeric(18, 2)
	, OurItemCode nvarchar(15) 
	, CasesRemaining numeric(18, 2)
	, HasFailedCase bit
	, HasFailedRate bit
	, ResultStatusTypeId int
)

insert into #temp (
	InvoiceHeaderId
	, InvoiceDetailId
	, Customer
	, InvoiceDate
	, InvoiceMonth
	, CustItemCode
	, CustomerRate
	, ApprovedRate
	, Cases
	, ResultStatusTypeId
	, HasFailedCase
	, HasFailedRate
	, OurItemCode
	, CasesRemaining
)
select
	a.InvoiceHeaderId
	, b.InvoiceDetailId
	, a.Customer
	, a.InvoiceDate
	, year(a.InvoiceDate) * 100 + month(a.InvoiceDate) as InvoiceMonth
	, b.CustItemCode
	, b.CustomerRate
	, b.ApprovedRate
	, b.Cases
	, 0 as ResultStatusTypeId
	, 0 as HasFailedCase
	, 0 as HasFailedRate
	, '' as OurItemCode
	, 0 as CasesRemaining
from InvoiceHeader a
inner join InvoiceDetail b on b.InvoiceHeaderId = a.InvoiceHeaderId
where a.StatusTypeId = 1
and (a.InvoiceHeaderId = @headerId or @headerId = -1)

/*
----------------------------------------------------
Find our itemcode
----------------------------------------------------
*/

update a
set OurItemCode = case when b.OurItemCode is null then '' else b.OurItemCode end
	, HasFailedRate = case when b.OurItemCode is null then 1 else 0 end
	, ResultStatusTypeId = case when b.OurItemCode is null then 2 else 0 end
from #temp a
left join (
	select
		a.InvoiceDetailId
		, b.OurItemCode
		, sum(b.Cases) as Cases
		, ROW_NUMBER() over (partition by a.InvoiceDetailId order by sum(b.Cases) desc) as RowNum
	from #temp a
	inner join CaseSummary b
		on b.CustomerId = a.Customer
		and b.CustomerItemCode = a.CustItemCode
		and b.MonthId = a.InvoiceMonth
	group by
		a.InvoiceDetailId
		, b.OurItemCode
) b on b.InvoiceDetailId = a.InvoiceDetailId
where a.HasFailedRate = 0

/*
----------------------------------------------------
Match rates
----------------------------------------------------
*/

update a
set ApprovedRate = b.Rate
	, HasFailedRate = case when b.Rate is null then 1 else 0 end
	, ResultStatusTypeId = case when b.Rate is null then 3 else 0 end
from #temp a
left join PriceDeals b
	on b.Customer = a.Customer
	and a.InvoiceMonth between b.EffectiveMonth and b.EndMonth
	and b.ItemCode = a.OurItemCode
	and b.Rate = a.CustomerRate
where a.HasFailedRate = 0

/*
----------------------------------------------------
Calculate cases
----------------------------------------------------
*/

update a
set CasesRemaining = isnull(b.Cases, 0) - isnull(a.Cases, 0)
	, HasFailedCase = case when b.Cases - a.Cases < 0 then 1 else 0 end
from #temp a
left join (
	select
		a.CustomerId
		, a.OurItemCode
		, a.MonthId
		, sum(a.Cases) as Cases
	from CaseSummary a
	group by
		a.CustomerId
		, a.OurItemCode
		, a.MonthId
) b
	on b.CustomerId = a.Customer
	and b.MonthId = a.InvoiceMonth
	and b.OurItemCode = a.OurItemCode
left join (
	select
		a.Customer
		, c.OurItemCode
		, year(a.InvoiceDate) * 100 + month(a.InvoiceDate) as InvoiceMonthId
		, sum(b.Cases) as Cases
	from InvoiceHeader a
	inner join InvoiceDetail b on b.InvoiceHeaderId = a.InvoiceHeaderId
	inner join InvoiceResult c on c.InvoiceDetailId = b.InvoiceDetailId
	group by
		a.Customer
		, c.OurItemCode
		, a.InvoiceDate
) c
	on c.Customer = a.Customer
	and c.InvoiceMonthId = a.InvoiceMonth
	and c.OurItemCode = a.OurItemCode
where a.HasFailedCase = 0

/*
----------------------------------------------------
Pass everything not failed
----------------------------------------------------
*/

update #temp
set ResultStatusTypeId = 1
where HasFailedCase = 0 and HasFailedRate = 0

/*
----------------------------------------------------
Upload results
----------------------------------------------------
*/

set xact_abort on
begin transaction

update a
set a.CasesRemaining = b.CasesRemaining
	, a.HasFailedCase = b.HasFailedCase
	, a.HasFailedRate = b.HasFailedRate
	, a.ResultStatusTypeId = b.ResultStatusTypeId
	, a.OurItemCode = b.OurItemCode
from InvoiceResult a
inner join #temp b on b.InvoiceDetailId = a.InvoiceDetailId

insert into InvoiceResult (
	InvoiceHeaderId
	, InvoiceDetailId
	, OurItemCode
	, CasesRemaining
	, HasFailedCase
	, HasFailedRate
	, ResultStatusTypeId
)
select
	a.InvoiceHeaderId
	, a.InvoiceDetailId
	, a.OurItemCode
	, a.CasesRemaining
	, a.HasFailedCase
	, a.HasFailedRate
	, a.ResultStatusTypeId
from #temp a
left join InvoiceResult b on b.InvoiceDetailId = a.InvoiceDetailId
where b.InvoiceDetailId is null

commit transaction

drop table #temp