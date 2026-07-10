CREATE function dbo.RandomMonth(@startMonthId int, @endMonthId int)
returns date
as
begin
	declare @startDate date = cast(cast(@startMonthId / 100 as nvarchar(max)) + '-' + cast(@startMonthId % 100 as nvarchar(max)) + '-01' as date)
	declare @endDate date = cast(cast(@endMonthId / 100 as nvarchar(max)) + '-' + cast(@endMonthId % 100 as nvarchar(max)) + '-01' as date)
	declare @monthDiff int = datediff(m, @startDate, @endDate)
	declare @rand int = (select RandValue from vRAndValue) * (@monthDiff + 1)

	return dateadd(m, @rand, @startDate)	
end;