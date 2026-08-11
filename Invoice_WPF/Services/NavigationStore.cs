using Invoice_WPF.Services.Invoking;
using Invoice_WPF.ViewModels;

namespace Invoice_WPF.Services;

public interface INavigation
{
    ViewModelBase? CurrentViewModel { get; }
    Func<Task>? CurrentViewModelChanged { get; set; }
    Task NavigateToInvoiceView(InvokerToken token, int headerId);
    Task NavigateToInvoiceSearchView();
    void NavigateToMainMenu();
    Task NavigateToMainMenuAsync();
}

public class Navigation : INavigation
{
    private IFactory _factory;
    private ViewModelBase? _currentViewModel;

    public Navigation(IFactory factory)
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
        var model = new InvoiceSearchViewModel(
            invoiceSearchInvoker: _factory.InvoiceSearchInvoker,
            invoiceSearchState: _factory.InvoiceSearchState,
            navigation: this);
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

    public async Task NavigateToInvoiceView(InvokerToken token, int headerId)
    {
        var model = new InvoiceViewModel(
            invoiceHeaderInvoker: _factory.InvoiceHeaderInvoker,
            invoiceHeaderState: _factory.InvoiceHeaderState,
            invoiceDetailInvoker: _factory.InvoiceDetailInvoker,
            invoiceDetailState: _factory.InvoiceDetailState,
            resultStatusInvoker: _factory.ResultStatusInvoker,
            resultStatusTypeState: _factory.ResultStatusTypeState,
            statusTypeInvoker: _factory.StatusTypeInvoker,
            statusTypeState: _factory.StatusTypeState
        );
        await model.LoadData(headerId, token);
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }
}
