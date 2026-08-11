using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;

namespace Invoice_WPF.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    private INavigation _navigationStore;
    public MainMenuViewModel(INavigation navigationStore)
    {
        OpenInvoiceSearchCommand = new AsyncRelayCommand(OpenInvoiceSearch);
        _navigationStore = navigationStore;
    }
    public IAsyncRelayCommand OpenInvoiceSearchCommand { get; }
    public async Task OpenInvoiceSearch()
    {
        await _navigationStore.NavigateToInvoiceSearchView();
    }
}
