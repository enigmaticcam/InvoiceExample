using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Data.DTOs;

public record InvoiceUpdateResultDTO(
    InvoiceHeaderEntity Invoice,
    InvoicePermissionsDTO Permissions
);
