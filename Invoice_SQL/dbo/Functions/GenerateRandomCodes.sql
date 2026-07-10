CREATE function dbo.[GenerateRandomCodes](@total int, @length int)
returns table
return (
	with List as (
		select 
			dbo.RandomCode(10) as Item
			, 1 as Total

		union all

		select 
			dbo.RandomCode(10) as Item
			, Total + 1 as Total
		from List a
		where Total < @total
	)
	select Item 
	from List
);