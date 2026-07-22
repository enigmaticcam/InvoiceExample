namespace Invoice_Logic.Data.DTOs;

public record InvoiceFullResultDTO(
    int InvoiceHeaderId,
    int InvoiceDetailId,
    string CustItemCode,
    string CustItemDesc,
    decimal CustomerRate,
    decimal ApprovedRate,
    decimal Cases,
    string? OurItemCode,
    decimal? CasesRemaining,
    bool? HasFailedCase,
    bool? HasFailedRate,
    int? ResultStatusTypeId
);

