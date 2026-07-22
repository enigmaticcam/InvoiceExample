namespace Invoice_BlazorWASM.Data;

public class DTO_InvoiceSummary
{
    public decimal TotalRequestedDollars { get; set; }
    public decimal TotalExceptionDollars { get; set; }
    public decimal TotalAgreedDollars { get; set; }

    public void Calc(IEnumerable<DTO_InvoiceDetail> lines)
    {
        TotalRequestedDollars = 0;
        TotalExceptionDollars = 0;
        TotalAgreedDollars = 0;
        foreach (var l in lines)
        {
            TotalRequestedDollars += l.CustomerRate * l.Cases;
            TotalExceptionDollars += l.ApprovedRate == 0 ? l.CustomerRate * l.Cases : 0;
            TotalAgreedDollars += l.ApprovedRate * l.Cases;
        }
    }
}
