namespace Invoice_Logic.Data.DTOs.Create;

public record InvoiceDetailCreateDTO(
    int InvoiceHeaderId,
    string CustItemCode,
    string CustItemDesc,
    decimal CustomerRate,
    decimal ApprovedRate,
    decimal Cases
);
