using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Invoking;

namespace Invoice_WPF.ViewModels;

public partial class MainViewModel : ViewModelBase, IDisposable
{
    private readonly INavigation _navigation;
    private readonly IModalNavigation _modalNavigation;
    private InvokerToken _token;
    public MainViewModel(INavigation navigationStore, InvokerToken token, IModalNavigation modalNavigation)
    {
        _navigation = navigationStore;
        _token = token;
        _modalNavigation = modalNavigation;
        _navigation.CurrentViewModelChanged += OnCurrentViewModelChanged;
        _modalNavigation.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        _token.OnRunning += SetIsOnRunning;
        OpenInvoiceSearchCommand = new AsyncRelayCommand(OpenInvoiceSearch);
        OpenInvoiceUploaderCommand = new AsyncRelayCommand(OpenInvoiceUploader);
    }

    public ViewModelBase? CurrentViewModel => _navigation.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigation.CurrentViewModel;
    [ObservableProperty]
    public partial bool IsNotRunning { get; set; } = true;
    public bool IsModalOpen => _modalNavigation.IsOpen;

    public Task OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
        return Task.CompletedTask;
    }

    public Task OnCurrentModalViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOpen));
        return Task.CompletedTask;
    }

    public IAsyncRelayCommand OpenInvoiceSearchCommand { get; }
    public IAsyncRelayCommand OpenInvoiceUploaderCommand { get; }
    public async Task OpenInvoiceSearch()
    {
        await _navigation.NavigateToInvoiceSearchView();
    }
    public async Task OpenInvoiceUploader()
    {
        await _navigation.NavigateToInvoiceUploaderView();
    }

    public Task SetIsOnRunning(bool isRunning)
    {
        IsNotRunning = !isRunning;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _token.OnRunning -= SetIsOnRunning;
    }
}
