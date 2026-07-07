namespace Invoice_Logic.Data.DTOs.Entity;

public record InvoiceHeaderEntity(
    int InvoiceHeaderId,
    int Customer,
    DateOnly InvoiceDate,
    int StatusTypeId,
    string Description
);
