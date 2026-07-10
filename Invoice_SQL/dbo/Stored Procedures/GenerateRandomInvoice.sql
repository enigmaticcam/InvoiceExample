CREATE procedure GenerateRandomInvoice
@insertIntoTable bit = 0
as

declare @minMonth int
declare @maxMonth int

select 
	@minMonth = min(EffectiveMonth)
	, @maxMonth = max(EndMonth)
from PriceDeals

declare @effectiveDate date
declare @effectiveMonth int
set @effectiveDate = dbo.RandomMonth(@minMonth, @maxMonth)
set @effectiveMonth = year(@effectiveDate) * 100 + month(@effectiveDate)

select top 100 
	a.Customer
	,@effectiveDate as InvoiceDate
	, b.CustomerItemCode
	, case ABS(CHECKSUM(NewId())) % 5 when 1 then a.Rate * 1.5 else a.Rate end as Rate
	, b.Cases * (cast((ABS(CHECKSUM(NewId())) % 150) as numeric(18, 2)) / 100) as Cases
into #temp
from PriceDeals a
inner join (
	select CustomerItemCode, OurItemCode, Cases, CustomerId
	from CaseSummary
) b 
	on b.OurItemCode = a.ItemCode
	and b.CustomerId = a.Customer
where @effectiveMonth between EffectiveMonth and EndMonth
and a.Customer = (
	select top 1 Customer
	from PriceDeals
	order by ABS(CHECKSUM(NewId()))
)
order by ABS(CHECKSUM(NewId()))

if @insertIntoTable = 1
begin
	declare @id Table (InvoiceHeaderId int)

	insert into InvoiceHeader (
		Customer, 
		InvoiceDate,
		StatusTypeId,
		[Description]
	)
	output inserted.InvoiceHeaderId into @id
	select top 1
		Customer
		, InvoiceDate
		, 1
		, ''
	from #temp

	insert into InvoiceDetail (
		InvoiceHeaderId,
		CustItemCode,
		CustItemDesc,
		CustomerRate,
		ApprovedRate,
		Cases
	)
	select
		(select InvoiceHeaderId from @id) as InvoiceHeaderId
		, CustomerItemCode as CustItemCode
		, '' as CustItemDesc
		, Rate as CustomerRate
		, 0 as ApprovedRate
		, Cases
	from #temp
end

select *
from #temp