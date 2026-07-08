namespace Invoice_Logic.Data.DTOs;

public class InvoiceFilterDTO
{
    public bool ByCustomer { get; set; }
    public bool ByHeader { get; set; }
    public bool ByMonth { get; set; }
    public int? Customer { get; set; }
    public int? HeaderId { get; set; }
    public int? MonthId { get; set; }
}
