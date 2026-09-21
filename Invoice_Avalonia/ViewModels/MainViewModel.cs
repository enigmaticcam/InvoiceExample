using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace Invoice_Avalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        OpenInvoiceSearchCommand = ReactiveCommand.CreateFromTask(OpenInvoiceSearch);
    }
    private string _greeting = "Welcome to Avalonia!";

    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }
    public ReactiveCommand<Unit, Unit> OpenInvoiceSearchCommand { get; }

    public string Greeting
    {
        get => _greeting;
        set => this.RaiseAndSetIfChanged(ref _greeting, value);
    }

    public Task OpenInvoiceSearch()
    {
        CurrentViewModel = new InvoiceSearchViewModel();
        return Task.CompletedTask;
    }
}
