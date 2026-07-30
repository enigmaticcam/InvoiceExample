namespace Invoice_Logic.Data.DTOs.Update;

public record InvoiceDetailUpdateDTO(
    int InvoiceDetailId,
    decimal ApprovedRate
);
