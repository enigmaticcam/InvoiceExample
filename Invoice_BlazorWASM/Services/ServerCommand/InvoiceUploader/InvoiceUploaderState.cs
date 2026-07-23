using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceUploader;

public interface IInvoiceUploaderState : IEntityState<int, DTO_InvoiceHeader> { }

public class InvoiceUploaderState : EntityState<int, DTO_InvoiceHeader>, IInvoiceUploaderState
{
    public InvoiceUploaderState(IClearCollection clearCollection) : base(clearCollection)
    {
    }

    public override string EntityName => "InvoiceUploaderState";
}
