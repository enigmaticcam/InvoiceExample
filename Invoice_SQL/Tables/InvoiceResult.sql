CREATE TABLE [dbo].[InvoiceResult]
(
	[InvoiceResultId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvoiceDetailId] INT NOT NULL, 
    [InvoiceHeaderId] INT NOT NULL, 
    [OurItemCode] NVARCHAR(15) NOT NULL, 
    [CasesRemaining] NUMERIC(18, 2) NOT NULL, 
    [HasFailedCase] BIT NOT NULL, 
    [HasFailedRate] BIT NOT NULL, 
    [ResultStatusTypeId] INT NOT NULL, 
    CONSTRAINT [FK_InvoiceResult_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [InvoiceDetail]([InvoiceDetailId]), 
    CONSTRAINT [FK_InvoiceResult_ResultStatusType] FOREIGN KEY ([ResultStatusTypeId]) REFERENCES [ResultStatusType]([ResultStatusTypeId]) 
)
