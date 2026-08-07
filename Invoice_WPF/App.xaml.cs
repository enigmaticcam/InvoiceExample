using Invoice_WPF.Services;
using Invoice_WPF.Services;
using Invoice_WPF.ViewModels;
using System.Windows;

namespace Invoice_WPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private INavigation? _navigationStore;

    protected override void OnStartup(StartupEventArgs e)
    {
        var factory = new Factory();
        _navigationStore = new Navigation(factory);
        _navigationStore.NavigateToMainMenu();
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}

