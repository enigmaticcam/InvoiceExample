using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Models;
using System.Collections.ObjectModel;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceSearchViewModel : ViewModelBase
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
        SearchCommand = new AsyncRelayCommand(Search);
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
            ByCustomer = true;
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
            ByHeader = true;
        }
    }

    public IAsyncRelayCommand SearchCommand { get; }
    private async Task Search()
    {
        await Task.Delay(3000);
    }
}
