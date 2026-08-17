using Invoice_WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Invoice_WPF.Views;
/// <summary>
/// Interaction logic for InvoiceView.xaml
/// </summary>
public partial class InvoiceView : UserControl
{
    public InvoiceView()
    {
        InitializeComponent();
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            var context = (InvoiceViewModel)DataContext;
            await context.Delete();
        }
    }
}
