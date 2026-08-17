using Invoice_WPF.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace Invoice_WPF.Views;
/// <summary>
/// Interaction logic for InvoiceUploaderView.xaml
/// </summary>
public partial class InvoiceUploaderView : UserControl
{
    public InvoiceUploaderView()
    {
        InitializeComponent();
    }

    private async void DownloadButton_Click(object sender, RoutedEventArgs e)
    {
        var save = new SaveFileDialog();
        if (save.ShowDialog() == true)
        {
            var vm = (InvoiceUploaderViewModel)DataContext;
            await vm.Download(save.FileName);
        }
    }

    private async void UploadButton_Click(object sender, RoutedEventArgs e)
    {
        var open = new OpenFileDialog();
        if (open.ShowDialog() == true)
        {
            var vm = (InvoiceUploaderViewModel)DataContext;
            await vm.Upload(open.FileName);
        }
    }
}
