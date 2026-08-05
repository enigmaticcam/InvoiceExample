using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IInvoiceHeaderState : IEntityState<int, InvoiceHeaderEntity> { }

public class InvoiceHeaderState : EntityState<int, InvoiceHeaderEntity>, IInvoiceHeaderState
{
    protected override int GetId(InvoiceHeaderEntity obj)
    {
        return obj.InvoiceHeaderId;
    }
}
