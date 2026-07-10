CREATE TABLE [dbo].[PriceDeals] (
    [PriceDealId]    INT             IDENTITY (1, 1) NOT NULL,
    [ItemCode]       NVARCHAR (15)   NOT NULL,
    [Customer]       INT             NOT NULL,
    [Rate]           NUMERIC (18, 2) NOT NULL,
    [EffectiveMonth] INT             NOT NULL,
    [EndMonth]       INT             NOT NULL,
    CONSTRAINT [PK__PriceDea__F0B2B9A7E50270FC] PRIMARY KEY CLUSTERED ([PriceDealId] ASC)
);

