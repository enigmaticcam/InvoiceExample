using Invoice_WPF.Stores;

namespace Invoice_WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly INavigationStore _navigationStore;
    public MainViewModel(INavigationStore navigationStore)
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
