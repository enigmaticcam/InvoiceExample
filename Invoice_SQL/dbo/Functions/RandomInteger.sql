CREATE function dbo.RandomInteger(@length int)
returns int
as
begin
	declare @number int = 0
	declare @l int = 0
	declare @temp int = 0
	while @l < @length
	begin
		set @temp = cast((select RandValue from vRandValue) * 10 as int)
		while @temp = 0 and @l = 0
		begin
			set @temp = cast((select RandValue from vRandValue) * 10 as int)
		end
		set @number = @number * 10 + @temp
		set @l = @l + 1
	end
	return @number
end;