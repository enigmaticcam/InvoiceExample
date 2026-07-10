CREATE function dbo.GenerateRandomIntegers(@total int, @length int)
returns table
return (
	with List as (
		select 
			dbo.RandomInteger(@length) as Item
			, 1 as Total

		union all

		select 
			dbo.RandomInteger(@length) as Item
			, Total + 1 as Total
		from List a
		where Total < @total
	)
	select Item 
	from List
);