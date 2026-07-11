CREATE TABLE [dbo].[CaseSummary]
(
	[CaseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [CustomerItemCode] NVARCHAR(15) NOT NULL, 
    [CustomerItemDesc] NVARCHAR(50) NOT NULL, 
    [MonthId] INT NOT NULL, 
    [OurItemCode] NVARCHAR(15) NOT NULL, 
    [Cases] NUMERIC(18, 2) NOT NULL
)
