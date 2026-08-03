using Invoice_WPF.ViewModels;

namespace Invoice_WPF.Stores;

public class NavigationStore
{
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set => _currentViewModel = value;
    }
}
