using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Models;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands;
using System.Collections.ObjectModel;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceSearchViewModel : ViewModelBase, IDisposable
{
    private IFactory _factory;
    public InvoiceSearchViewModel(IFactory factory)
    {
        _factory = factory;
        _invoices = new();
        SearchCommand = new AsyncRelayCommand(Search);
        _factory.InvoiceSearchState.OnChanged += LoadDataAsync;
        LoadData();
    }

    public void Dispose()
    {
        _factory.InvoiceSearchState.OnChanged -= LoadDataAsync;
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
        var command = new InvoiceSearchGetCommand(_factory.ServiceWrapper, _factory.InvoiceSearchState);
        await command.Perform(new Services.Core.InvoiceFilterDTO()
        {
            ByCustomer = _byCustomer,
            ByHeader = _byHeader,
            ByMonth = false,
            Customer = _customer,
            HeaderId = _headerId
        });
    }

    private Task LoadDataAsync()
    {
        LoadData();
        return Task.CompletedTask;
    }

    private void LoadData()
    {
        if (_factory.InvoiceSearchState.Item != null)
        {
            _invoices.Clear();
            foreach (var invoice in _factory.InvoiceSearchState.Item.Invoices)
            {
                _invoices.Add(new InvoiceHeaderModel(invoice));
            }
        }
    }
}
