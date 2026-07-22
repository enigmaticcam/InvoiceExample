using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Enums;

namespace Invoice_Logic.Core.Objects;

public abstract class InvoiceHeaderType
{
    public abstract InvoicePermissionsDTO GetPermissions(InvoiceHeaderEntity header);
    protected virtual bool CanPerformDeleteOrUndelete { get; } = false;
    protected virtual bool CanPerformRefreshResults { get; } = false;
    public void CanPerformAction(enumInvoiceActionType actionType, IUserLogging userLogging)
    {
        if (!GetCanPerformAction(actionType))
        {
            userLogging.ThrowInvoiceHeaderPermissionException($"Requested action cannot be performed: {actionType}");
        }
    }

    private bool GetCanPerformAction(enumInvoiceActionType actionType)
        => actionType switch
        {
            enumInvoiceActionType.Delete => CanPerformDeleteOrUndelete,
            enumInvoiceActionType.RefreshResults => CanPerformRefreshResults,
            _ => throw new NotImplementedException($"{actionType} not implemented")
        };
}
