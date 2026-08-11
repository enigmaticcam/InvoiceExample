using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Invoking;

namespace Invoice_WPF.ViewModels;

public partial class MainViewModel : ViewModelBase, IDisposable
{
    private readonly INavigation _navigationStore;
    private InvokerToken _token;
    public MainViewModel(INavigation navigationStore, InvokerToken token)
    {
        _navigationStore = navigationStore;
        _token = token;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        OpenInvoiceSearchCommand = new AsyncRelayCommand(OpenInvoiceSearch);
        _token.OnRunning += SetIsOnRunning;
    }

    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
    [ObservableProperty]
    public partial bool IsNotRunning { get; set; } = true;

    public Task OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
        return Task.CompletedTask;
    }
    public IAsyncRelayCommand OpenInvoiceSearchCommand { get; }
    public async Task OpenInvoiceSearch()
    {
        await _navigationStore.NavigateToInvoiceSearchView();
    }

    private Task SetIsOnRunning(bool isRunning)
    {
        IsNotRunning = !isRunning;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _token.OnRunning -= SetIsOnRunning;
    }
}
