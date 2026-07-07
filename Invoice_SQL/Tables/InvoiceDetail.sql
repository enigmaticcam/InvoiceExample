CREATE TABLE [dbo].[InvoiceDetail]
(
	[InvoiceDetailId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvoiceHeaderId] INT NOT NULL, 
    [CustItemCode] NVARCHAR(15) NOT NULL, 
    [CustItemDesc] NVARCHAR(50) NOT NULL, 
    [CustomerRate] NUMERIC(18, 2) NOT NULL, 
    [ApprovedRate] NUMERIC(18, 2) NOT NULL, 
    [Cases] NUMERIC(18, 2) NOT NULL, 
    CONSTRAINT [FK_InvoiceDetail_InvoiceHeader] FOREIGN KEY ([InvoiceHeaderId]) REFERENCES [InvoiceHeader]([InvoiceHeaderId])
)
