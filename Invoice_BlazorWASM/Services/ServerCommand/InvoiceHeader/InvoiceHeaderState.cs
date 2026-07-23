using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;

public interface IInvoiceHeaderState : IEntityState<int, DTO_InvoiceHeader> { }

public class InvoiceHeaderState : EntityState<int, DTO_InvoiceHeader>, IInvoiceHeaderState
{
    public InvoiceHeaderState(IClearCollection clearCollection) : base(clearCollection)
    {
    }

    public override string EntityName => "InvoiceHeaderState";
}
