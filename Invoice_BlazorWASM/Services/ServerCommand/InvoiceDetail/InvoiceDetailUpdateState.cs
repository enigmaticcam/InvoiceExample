using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceDetail;

public interface IInvoiceDetailUpdateState : IEntityUpdateState<int, InvoiceDetailUpdateDTO> { }

public class InvoiceDetailUpdateState : EntityUpdateState<InvoiceDetailUpdateDTO, int, DTO_InvoiceDetail>, IInvoiceDetailUpdateState
{
    public InvoiceDetailUpdateState(IInvoiceDetailState state) : base(state)
    {
    }
}
