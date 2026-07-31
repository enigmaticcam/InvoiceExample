namespace Invoice_WPF.Models;

public class InvoiceHeaderModel
{
    public int InvoiceHeaderId { get; set; }
    public int Customer { get; set; }
    public DateOnly InvoiceDate { get; set; }
    public int StatusTypeId { get; set; }
    public string Description { get; set; }
}
