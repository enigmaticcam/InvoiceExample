using Invoice_Logic.Data.DTOs.Entity;

namespace Invoice_Logic.Data.DTOs;

public class InvoiceSearchDTO
{
    public InvoiceSearchDTO()
    {
        Filter = new();
        Invoices = new();
    }

    public InvoiceSearchDTO(InvoiceFilterDTO filter, List<InvoiceHeaderEntity> invoices)
    {
        Filter = filter;
        Invoices = invoices;
    }

    public InvoiceFilterDTO Filter { get; set; }
    public List<InvoiceHeaderEntity> Invoices { get; set; }
}
