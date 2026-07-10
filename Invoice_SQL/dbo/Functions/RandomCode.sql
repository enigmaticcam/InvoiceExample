CREATE function [dbo].[RandomCode](@length int)
returns nvarchar(max)
as
begin
	declare @text nvarchar(max) = ''
	while len(@text) < @length
	begin
		select @text = @text + char(cast((select RandValue from vRandValue) * 26 + 65 as int))
	end
	return @text
end;