using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands.InvoiceUploader;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceUploaderViewModel : ViewModelBase, IDisposable
{
    private IFileDownload _fileDownload;
    private IInvoiceUploaderInvoker _invoiceUploaderInvoker;
    private IInvoiceUploaderState _invoiceUploaderState;
    private INavigation _navigation;
    private InvokerToken _token;

    public InvoiceUploaderViewModel(IFileDownload fileDownload, IInvoiceUploaderInvoker invoiceUploaderInvoker, IInvoiceUploaderState invoiceUploaderState, INavigation navigation, InvokerToken token)
    {
        _fileDownload = fileDownload;
        _invoiceUploaderInvoker = invoiceUploaderInvoker;
        _invoiceUploaderState = invoiceUploaderState;
        _navigation = navigation;
        _token = token;
        CloseCommand = new AsyncRelayCommand(Close);
        _token.OnRunning += SetIsOnRunning;
    }

    [ObservableProperty]
    public partial bool NotIsRunning { get; set; }

    public void Dispose()
    {
        _token.OnRunning -= SetIsOnRunning;
    }

    public IAsyncRelayCommand CloseCommand { get; }
    public async Task LoadData()
    {
        await _invoiceUploaderInvoker.Get(_token);
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
}
