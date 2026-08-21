using Invoice_WPF.Models;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IInvoiceDetailState : IEntityState<int, InvoiceFullResultModel> { }

public class InvoiceDetailState : EntityState<int, InvoiceFullResultModel>, IInvoiceDetailState
{
    protected override int GetId(InvoiceFullResultModel obj)
    {
        return obj.InvoiceDetailId;
    }
}
