using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Models;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands;
using Invoice_WPF.Stores;
using System.Collections.ObjectModel;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceSearchViewModel : ViewModelBase, IDisposable
{
    private IFactory _factory;
    private NavigationStore _navigationStore;

    public InvoiceSearchViewModel(IFactory factory, NavigationStore navigationStore)
    {
        _factory = factory;
        _invoices = new();
        SearchCommand = new AsyncRelayCommand(Search);
        CloseCommand = new AsyncRelayCommand(Close);
        _factory.InvoiceSearchState.OnChanged += LoadDataAsync;
        _navigationStore = navigationStore;
        //Load();
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
    public IAsyncRelayCommand CloseCommand { get; }
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

    private async Task Close()
    {
        await _navigationStore.NavigateToAsync(new MainMenuViewModel(_navigationStore, _factory));
    }

    private Task LoadDataAsync()
    {
        Load();
        return Task.CompletedTask;
    }

    public override async Task LoadData()
    {
        var command = new InvoiceSearchGetCommand(_factory.ServiceWrapper, _factory.InvoiceSearchState);
        await command.Perform();
    }

    private void Load()
    {
        if (_factory.InvoiceSearchState.Item != null)
        {
            var item = _factory.InvoiceSearchState.Item;
            _invoices.Clear();
            foreach (var invoice in item.Invoices)
            {
                _invoices.Add(new InvoiceHeaderModel(invoice));
            }
            Customer = item.Filter.Customer;
            HeaderId = item.Filter.HeaderId;
            ByCustomer = item.Filter.ByCustomer;
            ByHeader = item.Filter.ByHeader;
        }
    }
}
