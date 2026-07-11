CREATE TABLE [dbo].[InvoiceHeader]
(
	[InvoiceHeaderId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Customer] INT NOT NULL, 
    [InvoiceDate] DATE NOT NULL, 
    [StatusTypeId] INT NOT NULL, 
    [Description] NVARCHAR(1000) NOT NULL, 
    CONSTRAINT [FK_InvoiceHeader_StatusType] FOREIGN KEY ([StatusTypeId]) REFERENCES [StatusType]([StatusTypeId])
)
