using Invoice_WPF.Models;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IInvoiceHeaderState : IEntityState<int, InvoiceHeaderModel> { }

public class InvoiceHeaderState : EntityState<int, InvoiceHeaderModel>, IInvoiceHeaderState
{
    protected override int GetId(InvoiceHeaderModel obj)
    {
        return obj.InvoiceHeaderId;
    }
}
