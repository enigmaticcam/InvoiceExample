using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;

namespace Invoice_WPF.ViewModels;

public class InvoiceRandomViewModel : ViewModelBase, IDisposable
{
    private IModalNavigation _modalNavigation;

    public InvoiceRandomViewModel(IModalNavigation modalNavigation)
    {
        _modalNavigation = modalNavigation;
        CloseCommand = new AsyncRelayCommand(Close);
    }

    public IAsyncRelayCommand CloseCommand { get; }
    public void Dispose()
    {

    }

    public async Task Close()
    {
        await _modalNavigation.Close();
    }
}
