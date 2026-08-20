using Invoice_WPF.Services.Invoking;
using Invoice_WPF.ViewModels;

namespace Invoice_WPF.Services;

public interface INavigation
{
    ViewModelBase? CurrentViewModel { get; }
    Func<Task>? CurrentViewModelChanged { get; set; }
    Task NavigateToInvoiceView(InvokerToken token, int headerId);
    Task NavigateToInvoiceSearchView();
    Task NavigateToInvoiceUploaderView();
    void NavigateToMainMenu();
    Task NavigateToMainMenuAsync();
}

public class Navigation : INavigation
{
    private IFactory _factory;
    private ViewModelBase? _currentViewModel;
    private InvokerToken _token;

    public Navigation(IFactory factory, InvokerToken token)
    {
        _factory = factory;
        _token = token;
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

    private async Task OnCurrentViewModelChanged()
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
            navigation: this,
            statusTypeInvoker: _factory.StatusTypeInvoker,
            statusTypeState: _factory.StatusTypeState,
            token: _token);
        await model.LoadData();
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }

    public void NavigateToMainMenu()
    {
        var model = new MainMenuViewModel(this);
        CurrentViewModel = model;
    }

    public async Task NavigateToMainMenuAsync()
    {
        var model = new MainMenuViewModel(this);
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
            invoiceDetailUpdateState: _factory.InvoiceDetailUpdateState,
            navigation: this,
            resultStatusInvoker: _factory.ResultStatusInvoker,
            resultStatusTypeState: _factory.ResultStatusTypeState,
            statusTypeInvoker: _factory.StatusTypeInvoker,
            statusTypeState: _factory.StatusTypeState,
            token: _token
        );
        await model.LoadData(headerId);
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }

    public async Task NavigateToInvoiceUploaderView()
    {
        var model = new InvoiceUploaderViewModel(
            fileDownload: _factory.FileDownload,
            invoiceUploaderInvoker: _factory.InvoiceUploaderInvoker,
            invoiceUploaderState: _factory.InvoiceUploaderState,
            navigation: this,
            statusTypeInvoker: _factory.StatusTypeInvoker,
            statusTypeState: _factory.StatusTypeState,
            modalNavigation: _factory.ModalNavigation,
            token: _token);
        await model.LoadData();
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }
}
