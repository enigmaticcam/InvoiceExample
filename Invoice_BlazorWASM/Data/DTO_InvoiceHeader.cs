using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Data;

public class DTO_InvoiceHeader : IEntity<int>
{
    public DTO_InvoiceHeader(InvoiceHeaderEntity source)
    {
        InvoiceHeaderId = source.InvoiceHeaderId;
        Customer = source.Customer;
        InvoiceDate = source.InvoiceDate;
        StatusTypeId = source.StatusTypeId;
        Description = source.Description;
    }

    public DTO_InvoiceHeader(int invoiceHeaderId, int customer, DateOnly invoiceDate, int statusTypeId, string description)
    {
        InvoiceHeaderId = invoiceHeaderId;
        Customer = customer;
        InvoiceDate = invoiceDate;
        StatusTypeId = statusTypeId;
        Description = description;
    }

    public int InvoiceHeaderId { get; set; }
    public int Customer { get; set; }
    public DateOnly InvoiceDate { get; set; }
    public int StatusTypeId { get; set; }
    public string Description { get; set; }

    public int Id => InvoiceHeaderId;

    public IEntity<int> Copy()
    {
        return new DTO_InvoiceHeader(
            invoiceHeaderId: InvoiceHeaderId,
            customer: Customer,
            invoiceDate: InvoiceDate,
            statusTypeId: StatusTypeId,
            description: Description
        );
    }
}
