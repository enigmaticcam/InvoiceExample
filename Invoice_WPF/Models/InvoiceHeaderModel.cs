using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Models;

public class InvoiceHeaderModel
{
    public InvoiceHeaderModel(InvoiceHeaderEntity source)
    {
        InvoiceHeaderId = source.InvoiceHeaderId;
        Customer = source.Customer;
        InvoiceDate = source.InvoiceDate;
        StatusTypeId = source.StatusTypeId;
        Description = source.Description;
    }
    public int InvoiceHeaderId { get; set; }
    public int Customer { get; set; }
    public DateOnly InvoiceDate { get; set; }
    public int StatusTypeId { get; set; }
    public string Description { get; set; }
}
