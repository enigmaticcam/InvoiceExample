using Invoice_BlazorWASM.Data;

namespace Invoice_BlazorWASM.Services.Entities.ServerCommand.InvoiceHeader;

public interface IInvoiceHeaderState : IEntityState<int, DTO_InvoiceHeader> { }

public class InvoiceHeaderState : EntityState<int, DTO_InvoiceHeader>, IInvoiceHeaderState
{
    public InvoiceHeaderState(IClearCollection clearCollection) : base(clearCollection)
    {
    }

    public override string EntityName => "InvoiceHeaderState";
}
