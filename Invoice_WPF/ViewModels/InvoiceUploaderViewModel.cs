using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Models;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands.InvoiceUploader;
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
    private InvokerToken _token;

    private readonly ObservableCollection<InvoiceHeaderModel> _invoices;

    public InvoiceUploaderViewModel(IFileDownload fileDownload, IInvoiceUploaderInvoker invoiceUploaderInvoker, IInvoiceUploaderState invoiceUploaderState, INavigation navigation, IModalNavigation modalNavigation, InvokerToken token)
    {
        _fileDownload = fileDownload;
        _invoiceUploaderInvoker = invoiceUploaderInvoker;
        _invoiceUploaderState = invoiceUploaderState;
        _navigation = navigation;
        _modalNavigation = modalNavigation;
        _token = token;
        _invoices = new();
        CloseCommand = new AsyncRelayCommand(Close);
        ShowRandomModalCommand = new AsyncRelayCommand(ShowRandomModal);
        _token.OnRunning += SetIsOnRunning;
    }

    public IEnumerable<InvoiceHeaderModel> Invoices => _invoices;
    public bool HasData => _invoices.Count > 0;
    public bool NoData => _invoices.Count == 0;

    [ObservableProperty]
    public partial bool NotIsRunning { get; set; }

    public void Dispose()
    {
        _token.OnRunning -= SetIsOnRunning;
    }

    public IAsyncRelayCommand CloseCommand { get; }
    public IAsyncRelayCommand ShowRandomModalCommand { get; }
    public async Task LoadData()
    {
        var result = await _invoiceUploaderInvoker.Get(_token);
        if (result.IsSuccess && result.Obj != null)
        {
            _invoices.Clear();
            foreach (var item in result.Obj)
            {
                _invoices.Add(new InvoiceHeaderModel(item));
            }
            OnPropertyChanged(nameof(HasData));
            OnPropertyChanged(nameof(NoData));
        }

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

    }

    private async Task Close()
    {
        await _navigation.NavigateToMainMenuAsync();
    }

    private Task SetIsOnRunning(bool isRunning)
    {
        NotIsRunning = !isRunning;
        return Task.CompletedTask;
    }

    public async Task ShowRandomModal()
    {
        await _modalNavigation.ShowInvoiceRandomDialog();
    }
}
