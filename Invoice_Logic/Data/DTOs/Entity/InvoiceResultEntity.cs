namespace Invoice_Logic.Data.DTOs.Entity;

public record InvoiceResultEntity(
    int InvoiceResultId,
    int InvoiceDetailId,
    int InvoiceHeaderId,
    string OurItemCode,
    decimal CasesRemaining,
    bool HasFailedCase,
    bool HasFailedRate,
    int ResultStatusTypeId
);
