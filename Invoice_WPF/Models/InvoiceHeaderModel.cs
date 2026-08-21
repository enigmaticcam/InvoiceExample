using Invoice_WPF.Services;
using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Models;

public class InvoiceHeaderModel : ICopy<InvoiceHeaderModel>
{
    public InvoiceHeaderModel(int invoiceHeaderId, int customer, DateOnly invoiceDate, int statusTypeId, string description)
    {
        InvoiceHeaderId = invoiceHeaderId;
        Customer = customer;
        InvoiceDate = invoiceDate;
        StatusTypeId = statusTypeId;
        Description = description;
    }

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
    public System.DateOnly InvoiceDate { get; set; }
    public int StatusTypeId { get; set; }
    public string Description { get; set; }

    public InvoiceHeaderModel Copy()
    {
        return new InvoiceHeaderModel(
            invoiceHeaderId: InvoiceHeaderId,
            customer: Customer,
            invoiceDate: InvoiceDate,
            statusTypeId: StatusTypeId,
            description: Description
        );
    }
}
