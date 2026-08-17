using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands.InvoiceUploader;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceUploaderViewModel : ViewModelBase, IDisposable
{
    private HttpClient _client;
    private IInvoiceUploaderInvoker _invoiceUploaderInvoker;
    private IInvoiceUploaderState _invoiceUploaderState;
    private INavigation _navigation;
    private InvokerToken _token;

    public InvoiceUploaderViewModel(HttpClient client, IInvoiceUploaderInvoker invoiceUploaderInvoker, IInvoiceUploaderState invoiceUploaderState, INavigation navigation, InvokerToken token)
    {
        _client = client;
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
        var fullUri = _client.BaseAddress?.ToString() + "api/invoiceuploader/template";
        using var stream = await _client.GetStreamAsync(fullUri);
        using var target = new FileStream(location, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
        await stream.CopyToAsync(target);
        var process = new ProcessStartInfo
        {
            FileName = location,
            UseShellExecute = true
        };
        Process.Start(process);
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
