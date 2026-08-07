using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;

namespace Invoice_WPF.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    private INavigation _navigationStore;
    private IFactory _factory;
    public MainMenuViewModel(INavigation navigationStore, IFactory factory)
    {
        OpenInvoiceSearchCommand = new AsyncRelayCommand(OpenInvoiceSearch);
        _navigationStore = navigationStore;
        _factory = factory;
    }
    public IAsyncRelayCommand OpenInvoiceSearchCommand { get; }
    public async Task OpenInvoiceSearch()
    {
        await _navigationStore.NavigateToInvoiceSearchView();
    }
}
