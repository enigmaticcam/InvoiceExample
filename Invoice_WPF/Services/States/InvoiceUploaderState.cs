using Invoice_WPF.Models;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IInvoiceUploaderState : IEntityState<int, InvoiceHeaderModel> { }

public class InvoiceUploaderState : EntityState<int, InvoiceHeaderModel>, IInvoiceUploaderState
{
    protected override int GetId(InvoiceHeaderModel obj)
    {
        return obj.InvoiceHeaderId;
    }
}
