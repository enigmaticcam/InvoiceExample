using Invoice_WPF.Services;

namespace Invoice_WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly INavigation _navigationStore;
    public MainViewModel(INavigation navigationStore)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;

    public Task OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
        return Task.CompletedTask;
    }
}
