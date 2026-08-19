using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands.InvoiceHeader;
using Invoice_WPF.Services.Invoking;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceRandomViewModel : ViewModelBase, IDisposable
{
    private IModalNavigation _modalNavigation;
    private IInvoiceHeaderInvoker _invoiceHeaderInvoker;
    private InvokerToken _token;

    public InvoiceRandomViewModel(IModalNavigation modalNavigation, IInvoiceHeaderInvoker invoiceHeaderInvoker, InvokerToken token)
    {
        _modalNavigation = modalNavigation;
        _invoiceHeaderInvoker = invoiceHeaderInvoker;
        _token = token;
        GetRandomInvoiceCommand = new AsyncRelayCommand(GetRandomInvoice);
        CloseCommand = new AsyncRelayCommand(Close);
        _token.OnRunning += SetIsOnRunning;
    }
    [ObservableProperty]
    public partial string? InvoiceHeader { get; set; }

    [ObservableProperty]
    public partial string? InvoiceDetail { get; set; }
    [ObservableProperty]
    public partial bool IsNotRunning { get; set; } = true;

    public IAsyncRelayCommand GetRandomInvoiceCommand { get; set; }
    public IAsyncRelayCommand CloseCommand { get; }
    public void Dispose()
    {
        _token.OnRunning -= SetIsOnRunning;
    }

    private Task SetIsOnRunning(bool isRunning)
    {
        IsNotRunning = !isRunning;
        return Task.CompletedTask;
    }

    private async Task GetRandomInvoice()
    {
        var result = await _invoiceHeaderInvoker.GetRandom(_token);
        if (result.IsSuccess)
        {
            InvoiceHeader = result.Obj?.Header;
            InvoiceDetail = result.Obj?.Detail;
        }
    }

    public async Task Close()
    {
        await _modalNavigation.Close();
    }
}
