using Invoice_WPF.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Invoice_WPF.ViewModels;

public class InvoiceSearchViewModel : ViewModelBase
{
    public InvoiceSearchViewModel()
    {
        _invoices = new();
        _invoices.Add(new InvoiceHeaderModel()
        {
            Customer = 111222,
            Description = "Description 1",
            InvoiceDate = DateOnly.FromDayNumber(100),
            InvoiceHeaderId = 1,
            StatusTypeId = 1
        });
        _invoices.Add(new InvoiceHeaderModel()
        {
            Customer = 222333,
            Description = "Description 2",
            InvoiceDate = DateOnly.FromDayNumber(200),
            InvoiceHeaderId = 2,
            StatusTypeId = 2
        });
    }

    private readonly ObservableCollection<InvoiceHeaderModel> _invoices;
    public IEnumerable<InvoiceHeaderModel> Invoices => _invoices;

    private bool _byCustomer;
    public bool ByCustomer
    {
        get => _byCustomer;
        set
        {
            _byCustomer = value;
            OnPropertyChanged(nameof(ByCustomer));
        }
    }

    private bool _byHeader;
    public bool ByHeader
    {
        get => _byHeader;
        set
        {
            _byHeader = value;
            OnPropertyChanged(nameof(ByHeader));
        }
    }

    private int? _customer;
    public int? Customer
    {
        get => _customer;
        set
        {
            _customer = value;
            OnPropertyChanged(nameof(Customer));
        }
    }

    private int? _headerId;
    public int? HeaderId
    {
        get => _headerId;
        set
        {
            _headerId = value;
            OnPropertyChanged(nameof(HeaderId));
        }
    }

    public ICommand SearchCommand { get; }
}
