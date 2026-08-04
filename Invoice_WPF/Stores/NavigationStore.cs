using Invoice_WPF.ViewModels;

namespace Invoice_WPF.Stores;

public class NavigationStore
{
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        private set => _currentViewModel = value;
    }
    public Func<Task>? CurrentViewModelChanged { get; set; }

    public async Task NavigateToAsync(ViewModelBase viewModel)
    {
        await viewModel.LoadData();
        CurrentViewModel = viewModel;
        await OnCurrentViewModelChanged();
    }

    public void NavigateTo(ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
    }

    public async Task OnCurrentViewModelChanged()
    {
        if (CurrentViewModelChanged != null)
        {
            await CurrentViewModelChanged();
        }
    }
}
