using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Core;
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
        _token.OnComplete += SetOnComplete;
        OpenInvoiceSearchCommand = new AsyncRelayCommand(OpenInvoiceSearch);
        OpenInvoiceUploaderCommand = new AsyncRelayCommand(OpenInvoiceUploader);
    }

    public ViewModelBase? CurrentViewModel => _navigation.CurrentViewModel;
    public ViewModelBase? CurrentModalViewModel => _modalNavigation.CurrentViewModel;
    private bool _isNotRunning = true;
    public bool IsNotRunning
    {
        get => _isNotRunning;
        set
        {
            _isNotRunning = value;
            OnPropertyChanged(nameof(IsNotRunning));
            OnPropertyChanged(nameof(LastResultSetHasError));
        }
    }
    public bool IsModalOpen => _modalNavigation.IsOpen;
    private List<WPFResult>? _lastResultSet;
    public List<WPFResult>? LastResultSet
    {
        get => _lastResultSet;
        set
        {
            _lastResultSet = value;
            OnPropertyChanged(nameof(LastResultSet));
            OnPropertyChanged(nameof(LastResultSetHasError));
        }
    }
    public bool LastResultSetHasError => (LastResultSet?.Count ?? 0) > 0 && IsNotRunning;

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

    public Task SetOnComplete(List<WPFResult> results)
    {
        LastResultSet = results
            .Where(x => !x.IsSuccess)
            .ToList();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _token.OnRunning -= SetIsOnRunning;
    }
}
