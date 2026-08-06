using Invoice_WPF.Services;
using Invoice_WPF.ViewModels;

namespace Invoice_WPF.Stores;

public interface INavigationStore
{
    ViewModelBase? CurrentViewModel { get; }
    Func<Task>? CurrentViewModelChanged { get; set; }
    Task NavigateToInvoiceView(int headerId);
    Task NavigateToInvoiceSearchView();
    void NavigateToMainMenu();
    Task NavigateToMainMenuAsync();
}

public class NavigationStore : INavigationStore
{
    private IFactory _factory;
    private ViewModelBase? _currentViewModel;

    public NavigationStore(IFactory factory)
    {
        _factory = factory;
    }

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        private set => _currentViewModel = value;
    }
    public Func<Task>? CurrentViewModelChanged { get; set; }

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

    public async Task NavigateToInvoiceSearchView()
    {
        var model = new InvoiceSearchViewModel(_factory, this);
        await model.LoadData();
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }

    public void NavigateToMainMenu()
    {
        var model = new MainMenuViewModel(this, _factory);
        CurrentViewModel = model;
    }

    public async Task NavigateToMainMenuAsync()
    {
        var model = new MainMenuViewModel(this, _factory);
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }

    public async Task NavigateToInvoiceView(int headerId)
    {
        var model = new InvoiceViewModel(
            serviceWrapper: _factory.ServiceWrapper,
            invoiceHeaderState: _factory.InvoiceHeaderState,
            invoiceDetailState: _factory.InvoiceDetailState,
            resultStatusTypeState: _factory.ResultStatusTypeState,
            statusTypeState: _factory.StatusTypeState
        );
        await model.LoadData(headerId);
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }
}
