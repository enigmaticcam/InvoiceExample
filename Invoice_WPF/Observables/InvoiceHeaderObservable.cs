using CommunityToolkit.Mvvm.ComponentModel;
using Invoice_WPF.Models;

namespace Invoice_WPF.Observables;

public class InvoiceHeaderObservable : ObservableObject
{
    private InvoiceHeaderModel _invoiceHeader;

    public InvoiceHeaderObservable(InvoiceHeaderModel invoiceHeader)
    {
        _invoiceHeader = invoiceHeader;
    }

    public int InvoiceHeaderId
    {
        get => _invoiceHeader.InvoiceHeaderId;
        set => SetProperty(_invoiceHeader.InvoiceHeaderId, value, _invoiceHeader, (u, n) => u.InvoiceHeaderId = n);
    }

    public int Customer
    {
        get => _invoiceHeader.Customer;
        set => SetProperty(_invoiceHeader.Customer, value, _invoiceHeader, (u, n) => u.Customer = n);
    }

    public DateOnly InvoiceDate
    {
        get => _invoiceHeader.InvoiceDate;
        set => SetProperty(_invoiceHeader.InvoiceDate, value, _invoiceHeader, (u, n) => u.InvoiceDate = n);
    }

    public int StatusTypeId
    {
        get => _invoiceHeader.StatusTypeId;
        set => SetProperty(_invoiceHeader.StatusTypeId, value, _invoiceHeader, (u, n) => u.StatusTypeId = n);
    }

    public string Description
    {
        get => _invoiceHeader.Description;
        set => SetProperty(_invoiceHeader.Description, value, _invoiceHeader, (u, n) => u.Description = n);
    }
}
