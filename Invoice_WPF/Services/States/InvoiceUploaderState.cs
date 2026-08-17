using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IInvoiceUploaderState : IEntityState<int, InvoiceHeaderEntity> { }

public class InvoiceUploaderState : EntityState<int, InvoiceHeaderEntity>, IInvoiceUploaderState
{
    protected override int GetId(InvoiceHeaderEntity obj)
    {
        return obj.InvoiceHeaderId;
    }
}
