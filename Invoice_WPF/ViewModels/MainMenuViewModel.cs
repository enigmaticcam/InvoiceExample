using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;
using Invoice_WPF.Stores;

namespace Invoice_WPF.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    private NavigationStore _navigationStore;
    private IFactory _factory;
    public MainMenuViewModel(NavigationStore navigationStore, IFactory factory)
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
