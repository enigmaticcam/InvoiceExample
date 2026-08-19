using Invoice_WPF.Services;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.ViewModels;
using System.Windows;

namespace Invoice_WPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private INavigation? _navigation;

    protected override void OnStartup(StartupEventArgs e)
    {
        var token = new InvokerToken();
        var factory = new Factory(token);
        _navigation = new Navigation(factory, token);
        _navigation.NavigateToMainMenu();
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigation, token, factory.ModalNavigation)
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}

