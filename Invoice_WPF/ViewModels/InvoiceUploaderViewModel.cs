using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Models;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands.InvoiceUploader;
using Invoice_WPF.Services.Commands.StatusType;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;
using System.Collections.ObjectModel;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceUploaderViewModel : ViewModelBase, IDisposable
{
    private IFileDownload _fileDownload;
    private IInvoiceUploaderInvoker _invoiceUploaderInvoker;
    private IInvoiceUploaderState _invoiceUploaderState;
    private INavigation _navigation;
    private IModalNavigation _modalNavigation;
    private IStatusTypeInvoker _statusTypeInvoker;
    private IStatusTypeState _statusTypeState;
    private InvokerToken _token;

    private readonly ObservableCollection<InvoiceHeaderDisplayModel> _invoices;

    public InvoiceUploaderViewModel(IFileDownload fileDownload, IInvoiceUploaderInvoker invoiceUploaderInvoker, IInvoiceUploaderState invoiceUploaderState, INavigation navigation, IModalNavigation modalNavigation, InvokerToken token, IStatusTypeInvoker statusTypeInvoker, IStatusTypeState statusTypeState)
    {
        _fileDownload = fileDownload;
        _invoiceUploaderInvoker = invoiceUploaderInvoker;
        _invoiceUploaderState = invoiceUploaderState;
        _navigation = navigation;
        _modalNavigation = modalNavigation;
        _token = token;
        _statusTypeInvoker = statusTypeInvoker;
        _statusTypeState = statusTypeState;
        _invoices = new();
        CloseCommand = new AsyncRelayCommand(Close);
        ShowRandomModalCommand = new AsyncRelayCommand(ShowRandomModal);
        OpenCommand = new AsyncRelayCommand<InvoiceHeaderDisplayModel>(Open);
        _token.OnRunning += SetIsOnRunning;
    }

    public IEnumerable<InvoiceHeaderDisplayModel> Invoices => _invoices;
    public bool HasData => _invoices.Count > 0;
    public bool NoData => _invoices.Count == 0;
    private InvoiceHeaderDisplayModel? _selectedInvoice;
    public InvoiceHeaderDisplayModel? SelectedInvoice
    {
        get => _selectedInvoice;
        set
        {
            _selectedInvoice = value;
            OnPropertyChanged(nameof(SelectedInvoice));
            OnPropertyChanged(nameof(CanOpenInvoice));
        }
    }
    public bool CanOpenInvoice => SelectedInvoice != null && IsNotRunning;

    private bool _isNotRunning = true;
    public bool IsNotRunning
    {
        get => _isNotRunning;
        set
        {
            _isNotRunning = value;
            OnPropertyChanged(nameof(IsNotRunning));
            OnPropertyChanged(nameof(CanOpenInvoice));
        }
    }

    public void Dispose()
    {
        _token.OnRunning -= SetIsOnRunning;
    }

    public IAsyncRelayCommand CloseCommand { get; }
    public IAsyncRelayCommand ShowRandomModalCommand { get; }
    public IAsyncRelayCommand<InvoiceHeaderDisplayModel> OpenCommand { get; }
    public async Task LoadData()
    {
        if (!_statusTypeState.IsLoaded)
        {
            await _statusTypeInvoker.Get(_token);
        }
        if (!_invoiceUploaderState.IsLoaded)
        {
            await _invoiceUploaderInvoker.Get(_token);
        }
        if (_invoiceUploaderState.IsLoaded)
        {
            LoadData(_invoiceUploaderState.Items);
        }
    }

    private void LoadData(IEnumerable<InvoiceHeaderModel> items)
    {
        _invoices.Clear();
        foreach (var item in items)
        {
            _invoices.Add(new InvoiceHeaderDisplayModel(item, _statusTypeState));
        }
        OnPropertyChanged(nameof(HasData));
        OnPropertyChanged(nameof(NoData));
    }

    public async Task Download(string location)
    {
        await _fileDownload.Download(
            uri: "api/invoiceuploader/template",
            location: location,
            openAfterDownload: true);
    }

    public async Task Upload(string file)
    {
        var result = await _invoiceUploaderInvoker.Upload(_token, file);
        if (result.IsSuccess)
        {
            LoadData(_invoiceUploaderState.Items);
        }
    }

    private async Task Open(InvoiceHeaderDisplayModel? invoice)
    {
        if (invoice != null)
        {
            await _navigation.NavigateToInvoiceView(_token, invoice.InvoiceHeaderId);
        }
    }

    private async Task Close()
    {
        await _navigation.NavigateToMainMenuAsync();
    }

    private Task SetIsOnRunning(bool isRunning)
    {
        IsNotRunning = !isRunning;
        return Task.CompletedTask;
    }

    public async Task ShowRandomModal()
    {
        await _modalNavigation.ShowInvoiceRandomDialog();
    }
}
