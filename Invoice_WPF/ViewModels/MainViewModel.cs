namespace Invoice_WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        CurrentViewModel = new InvoiceSearchViewModel();
    }

    public ViewModelBase CurrentViewModel { get; }
}
