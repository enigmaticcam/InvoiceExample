CREATE procedure [dbo].[GenerateRandomData]
as

--Generate our item codes
select * 
into #OurItems
from dbo.GenerateRandomCodes(1000, 7)
option (maxrecursion 0)

--Generate customers
select *
into #Customers
from dbo.GenerateRandomIntegers(100, 6);

--Generate one year's time period
with TimePeriod as (
	select 
		cast('2025-01-01' as date) as TimeMonth
		, 1 as MonthCount
	union all
	select
		dateadd(m,1,TimeMonth)
		, MonthCount + 1
	from TimePeriod
	where MonthCount < 12
)
select *, Year(TimeMonth) * 100 + month(TimeMonth) as MonthId
into #TimePeriod
from TimePeriod


--Generate CaseSummary
select
	b.Item as CustomerId
	, dbo.RandomCode(7) as CustomerItemCode
	, '' as CustomerItemDesc
	, year(c.TimeMonth) * 100 + month(c.TimeMonth) as MonthId
	, a.Item as OurItemCode
	, cast((ABS(CHECKSUM(NewId())) % 1000000) as numeric(18, 2)) / 100 as Cases
into #CaseSummary
from #OurItems a
cross join #Customers b
cross join #TimePeriod c;

--Generate PriceDeals
select
	a.Item as ItemCode
	, b.Item as Customer
	, cast((ABS(CHECKSUM(NewId())) % 10000) as numeric(18, 2)) / 100 as Rate
	, c.EffectiveMonth
	, c.EndMonth
into #PriceDeals
from #OurItems a
cross join #Customers b
cross join (
	select 
		min(MonthId) as EffectiveMonth
		, max(MonthId) as EndMonth
	from (
		select 
			*
			, case count(*) over (partition by null) / MonthCount
				when 1 then 1 else 0 end as DivisionByHalf
		from #TimePeriod
	) a
	where DivisionByHalf = 0

	union

		select 
		min(MonthId) as EffectiveMonth
		, max(MonthId) as EndMonth
	from (
		select 
			*
			, case count(*) over (partition by null) / MonthCount
				when 1 then 1 else 0 end as DivisionByHalf
		from #TimePeriod
	) a
	where DivisionByHalf = 1
) c

--Insert into tables
truncate table CaseSummary
insert into CaseSummary(
	CustomerId,
	CustomerItemCode,
	CustomerItemDesc,
	MonthId,
	OurItemCode,
	Cases
)
select
	CustomerId,
	CustomerItemCode,
	CustomerItemDesc,
	MonthId,
	OurItemCode,
	Cases
from #CaseSummary

truncate table PriceDeals
insert into PriceDeals(
	ItemCode,
	Customer,
	Rate,
	EffectiveMonth,
	EndMonth
)
select
	ItemCode,
	Customer,
	Rate,
	EffectiveMonth,
	EndMonth
from #PriceDeals


drop table #OurItems
drop table #Customers
drop table #TimePeriod
drop table #CaseSummary
drop table #PriceDeals