using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Services.States;

public interface IInvoiceDetailUpdateState : IEntityUpdateState<int, InvoiceDetailUpdateDTO> { }

public class InvoiceDetailUpdateState : EntityUpdateState<InvoiceDetailUpdateDTO, int, InvoiceFullResultDTO>, IInvoiceDetailUpdateState
{
    public InvoiceDetailUpdateState(IInvoiceDetailState state) : base(state)
    {
    }
}
