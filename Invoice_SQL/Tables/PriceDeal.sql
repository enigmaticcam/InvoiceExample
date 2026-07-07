CREATE TABLE [dbo].[PriceDeals]
(
	[PriceDealId] INT NOT NULL PRIMARY KEY, 
    [ItemCode] NVARCHAR(15) NOT NULL, 
    [StructureId] NVARCHAR(15) NOT NULL, 
    [Customer] INT NOT NULL IDENTITY, 
    [Rate] NUMERIC(18, 2) NOT NULL, 
    [EffectiveMonth] INT NOT NULL, 
    [EndMonth] INT NOT NULL
)
