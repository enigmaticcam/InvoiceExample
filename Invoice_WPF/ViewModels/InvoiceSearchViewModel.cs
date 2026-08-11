using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Models;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands.InvoiceSearch;
using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;
using Invoice_WPF.Services.Invoking;
using System.Collections.ObjectModel;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceSearchViewModel : ViewModelBase, IDisposable
{
    private IInvoiceSearchInvoker _invoiceSearchInvoker;
    private IInvoiceSearchState _invoiceSearchState;
    private INavigation _navigation;
    private InvokerToken _token;

    public InvoiceSearchViewModel(IInvoiceSearchInvoker invoiceSearchInvoker, IInvoiceSearchState invoiceSearchState, INavigation navigation, InvokerToken token)
    {
        _invoiceSearchInvoker = invoiceSearchInvoker;
        _invoiceSearchState = invoiceSearchState;
        _navigation = navigation;
        _token = token;
        _invoices = new();
        SearchCommand = new AsyncRelayCommand(Search);
        CloseCommand = new AsyncRelayCommand(Close);
        OpenCommand = new AsyncRelayCommand<InvoiceHeaderModel>(OpenInvoice);
        _invoiceSearchState.OnChanged += LoadDataAsync;
        _token.OnRunning += SetIsOnRunning;
    }

    public void Dispose()
    {
        _invoiceSearchState.OnChanged -= LoadDataAsync;
        _token.OnRunning -= SetIsOnRunning;
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

    private bool _notIsRunning;
    public bool NotIsRunning
    {
        get => _notIsRunning;
        set
        {
            _notIsRunning = value;
            OnPropertyChanged(nameof(NotIsRunning));
        }
    }

    private bool _isRunning;
    public bool IsRunning
    {
        get => _isRunning;
        set
        {
            _isRunning = value;
            OnPropertyChanged(nameof(IsRunning));
        }
    }

    private Task SetIsOnRunning(bool isRunning)
    {
        NotIsRunning = !isRunning;
        IsRunning = IsRunning;
        return Task.CompletedTask;
    }

    public IAsyncRelayCommand SearchCommand { get; }
    public IAsyncRelayCommand CloseCommand { get; }
    public IAsyncRelayCommand<InvoiceHeaderModel> OpenCommand { get; }
    private async Task Search()
    {
        await _invoiceSearchInvoker.Search(_token, new InvoiceFilterDTO()
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
        await _navigation.NavigateToMainMenuAsync();
    }

    private async Task OpenInvoice(InvoiceHeaderModel? header)
    {
        if (header != null)
        {
            await _navigation.NavigateToInvoiceView(_token, header.InvoiceHeaderId);
        }
    }

    private Task LoadDataAsync()
    {
        Load();
        return Task.CompletedTask;
    }

    public async Task LoadData()
    {
        await _invoiceSearchInvoker.Search(_token);
    }

    private void Load()
    {
        if (_invoiceSearchState.Item != null)
        {
            var item = _invoiceSearchState.Item;
            _invoices.Clear();
            foreach (var invoice in item.Invoices.OrderByDescending(x => x.InvoiceHeaderId))
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
