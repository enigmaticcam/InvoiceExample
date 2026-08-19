using Invoice_WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Invoice_WPF.Views;
/// <summary>
/// Interaction logic for InvoiceRandomView.xaml
/// </summary>
public partial class InvoiceRandomView : UserControl
{
    public InvoiceRandomView()
    {
        InitializeComponent();
    }

    private void CopyHeaderButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var context = (InvoiceRandomViewModel)DataContext;
        if (context.InvoiceHeader != null)
        {
            Clipboard.SetText(context.InvoiceHeader);
            MessageBox.Show("Header copied to clipboard");
        }
    }

    private void CopyDetailButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var context = (InvoiceRandomViewModel)DataContext;
        if (context.InvoiceDetail != null)
        {
            Clipboard.SetText(context.InvoiceDetail);
            MessageBox.Show("Detail copied to clipboard");
        }
    }
}
