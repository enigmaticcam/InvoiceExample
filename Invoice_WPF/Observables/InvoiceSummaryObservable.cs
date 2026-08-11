using CommunityToolkit.Mvvm.ComponentModel;

namespace Invoice_WPF.Observables;

public partial class InvoiceSummaryObservable : ObservableObject
{
    [ObservableProperty]
    public partial decimal TotalRequestedDollars { get; set; }

    [ObservableProperty]
    public partial decimal TotalExceptionDollars { get; set; }

    [ObservableProperty]
    public partial decimal TotalAgreedDollars { get; set; }

    public void Calc(IEnumerable<InvoiceResultObservable> lines)
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
