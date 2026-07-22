using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Enums;

namespace Invoice_Logic.Core.Objects.InvoiceHeaderTypes;

public class InvoiceHeaderTypeApproved : InvoiceHeaderType
{
    public override InvoicePermissionsDTO GetPermissions(InvoiceHeaderEntity header)
    {
        return new InvoicePermissionsDTO()
        {
            CanDelete = false,
            StatusChanges = new List<int>()
            {
                (int)enumStatusType.Draft,
                (int)enumStatusType.Finished
            }
        };
    }
}
