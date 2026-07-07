namespace Invoice_Logic.Data.DTOs.Entity;

public record InvoiceDetailEntity(
    int InvoiceDetailId,
    int InvoiceHeaderId,
    string CustItemCode,
    string CustItemDesc,
    decimal CustomerRate,
    decimal ApprovedRate,
    decimal Cases
);
