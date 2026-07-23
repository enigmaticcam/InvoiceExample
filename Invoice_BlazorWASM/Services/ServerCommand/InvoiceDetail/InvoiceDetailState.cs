using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceDetail;

public interface IInvoiceDetailState : IEntityState<int, DTO_InvoiceDetail>
{ }

public class InvoiceDetailState : EntityState<int, DTO_InvoiceDetail>, IInvoiceDetailState
{
    private Dictionary<int, bool> _headerLoaded = new();
    public InvoiceDetailState(IClearCollection clearCollection) : base(clearCollection)
    {
    }

    public override string EntityName => "InvoiceDetailState";
}
