using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services.Core;
using System.Windows;

namespace Invoice_WPF.Observables;

public class InvoiceResultObservable : ObservableObject
{
    private InvoiceFullResultDTO _line;

    public InvoiceResultObservable(InvoiceFullResultDTO line)
    {
        _line = line;
        Pay = new RelayCommand(() => ApprovedRate = CustomerRate);
        RemovePay = new RelayCommand(() => ApprovedRate = 0);
    }

    public int InvoiceHeaderId => _line.InvoiceHeaderId;
    public int InvoiceDetailId => _line.InvoiceDetailId;
    public string CustItemCode => _line.CustItemCode;
    public string CustItemDesc => _line.CustItemDesc;
    public decimal CustomerRate => _line.CustomerRate;

    public decimal ApprovedRate
    {
        get => _line.ApprovedRate;
        set
        {
            IsChanged = true;
            SetProperty(_line.ApprovedRate, value, _line, (u, n) => u.ApprovedRate = n);
            OnPropertyChanged(nameof(CanPay));
            OnPropertyChanged(nameof(CanRemovePay));
            ChangedEvent?.Invoke();
        }
    }
    public decimal Cases => _line.Cases;
    public string OurItemCode => _line.OurItemCode;
    public decimal? CasesRemaining => _line.CasesRemaining;
    public bool? HasFailedCase => _line.HasFailedCase;
    public bool? HasFailedRate => _line.HasFailedRate;
    public int? ResultStatusTypeId => _line.ResultStatusTypeId;
    public bool IsChanged { get; private set; }
    public Visibility CanPay => ApprovedRate == 0 ? Visibility.Visible : Visibility.Collapsed;
    public Visibility CanRemovePay => ApprovedRate != 0 ? Visibility.Visible : Visibility.Collapsed;
    public IRelayCommand Pay { get; }
    public IRelayCommand RemovePay { get; }
    public Action? ChangedEvent { get; set; }
}
