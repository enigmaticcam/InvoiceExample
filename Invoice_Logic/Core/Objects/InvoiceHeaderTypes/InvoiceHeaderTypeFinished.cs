using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Core.Objects.InvoiceHeaderTypes;

public class InvoiceHeaderTypeFinished : InvoiceHeaderType
{
    public override InvoicePermissionsDTO GetPermissions(InvoiceHeaderEntity header)
    {
        return new InvoicePermissionsDTO()
        {
            CanDelete = false,
            StatusChanges = new List<int>()
        };
    }
}
