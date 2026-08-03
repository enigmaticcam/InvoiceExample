using Invoice_WPF.Services;
using Invoice_WPF.Stores;
using Invoice_WPF.ViewModels;
using System.Windows;

namespace Invoice_WPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _navigationStore = new();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var factory = new Factory();
        _navigationStore.CurrentViewModel = new InvoiceSearchViewModel(factory);
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}

