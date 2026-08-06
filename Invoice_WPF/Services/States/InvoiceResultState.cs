using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IInvoiceDetailState : IEntityState<int, InvoiceFullResultDTO> { }

public class InvoiceDetailState : EntityState<int, InvoiceFullResultDTO>, IInvoiceDetailState
{
    protected override int GetId(InvoiceFullResultDTO obj)
    {
        return obj.InvoiceDetailId;
    }
}
