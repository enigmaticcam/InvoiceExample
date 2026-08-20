using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Models;

public class InvoiceHeaderModel
{
    public InvoiceHeaderModel(InvoiceHeaderEntity source, IStatusTypeState state)
    {
        InvoiceHeaderId = source.InvoiceHeaderId;
        Customer = source.Customer;
        InvoiceDate = source.InvoiceDate;
        Description = source.Description;
        StatusType = state.GetText(source.StatusTypeId);
    }
    public int InvoiceHeaderId { get; set; }
    public int Customer { get; set; }
    public DateOnly InvoiceDate { get; set; }
    public string StatusType { get; set; }
    public string Description { get; set; }
}
