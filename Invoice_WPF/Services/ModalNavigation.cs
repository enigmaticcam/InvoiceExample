using Invoice_WPF.Services.Invoking;
using Invoice_WPF.ViewModels;

namespace Invoice_WPF.Services;

public interface IModalNavigation
{
    ViewModelBase? CurrentViewModel { get; }
    Func<Task>? CurrentViewModelChanged { get; set; }
    bool IsOpen { get; }
    Task Close();
    Task ShowInvoiceRandomDialog();
}

public class ModalNavigation : IModalNavigation
{
    private IFactory _factory;
    private InvokerToken _token;
    private ViewModelBase? _currentViewModel;

    public ModalNavigation(IFactory factory, InvokerToken token)
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
    public bool IsOpen => CurrentViewModel != null;

    public async Task Close()
    {
        CurrentViewModel = null;
        await OnCurrentViewModelChanged();
    }

    public async Task ShowInvoiceRandomDialog()
    {
        var model = new InvoiceRandomViewModel(this, _factory.InvoiceHeaderInvoker, _token);
        CurrentViewModel = model;
        await OnCurrentViewModelChanged();
    }

    private async Task OnCurrentViewModelChanged()
    {
        if (CurrentViewModelChanged != null)
        {
            await CurrentViewModelChanged();
        }
    }
}
