using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Enums;

namespace Invoice_Logic.Core.Objects.InvoiceHeaderTypes;

public class InvoiceHeaderTypeDraft : InvoiceHeaderType
{
    protected override bool CanPerformDeleteOrUndelete => true;
    protected override bool CanPerformRefreshResults => true;

    public override InvoicePermissionsDTO GetPermissions(InvoiceHeaderEntity header)
    {
        return new InvoicePermissionsDTO()
        {
            CanDelete = true,
            CanEdit = true,
            StatusChanges = new List<int>()
            {
                (int)enumStatusType.Approved
            }
        };
    }
}
