namespace Invoice_Logic.Data.DTOs.Create;

public record InvoiceHeaderCreateDTO(
    int Customer,
    DateOnly InvoiceDate,
    int StatusTypeId,
    string Description,
    IEnumerable<InvoiceDetailCreateDTO> Detail
);
