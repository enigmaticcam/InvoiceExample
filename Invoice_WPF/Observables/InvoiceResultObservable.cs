using CommunityToolkit.Mvvm.ComponentModel;
using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Observables;

public class InvoiceResultObservable : ObservableObject
{
    private InvoiceFullResultDTO _line;

    public InvoiceResultObservable(InvoiceFullResultDTO line)
    {
        _line = line;
    }

    public int InvoiceHeaderId => _line.InvoiceHeaderId;
    public int InvoiceDetailId => _line.InvoiceDetailId;
    public string CustItemCode => _line.CustItemCode;
    public string CustItemDesc => _line.CustItemDesc;
    public decimal CustomerRate => _line.CustomerRate;
    public decimal ApprovedRate
    {
        get => _line.ApprovedRate;
        set => SetProperty(_line.ApprovedRate, value, _line, (u, n) => u.ApprovedRate = n);
    }
    public decimal Cases => _line.Cases;
    public string OurItemCode => _line.OurItemCode;
    public decimal? CasesRemaining => _line.CasesRemaining;
    public bool? HasFailedCase => _line.HasFailedCase;
    public bool? HasFailedRate => _line.HasFailedRate;
    public int? ResultStatusTypeId => _line.ResultStatusTypeId;
}
