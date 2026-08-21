using Invoice_WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Invoice_WPF.Views;
/// <summary>
/// Interaction logic for InvoiceView.xaml
/// </summary>
public partial class InvoiceView : UserControl
{
    private InvoiceViewModel? _viewModel;

    public InvoiceView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
        Unloaded += OnUnloaded;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (_viewModel != null)
        {
            _viewModel.ListViewChanged -= UpdateColumnWidths;
        }
        _viewModel = e.NewValue as InvoiceViewModel;
        if (_viewModel != null)
        {
            _viewModel.ListViewChanged += UpdateColumnWidths;
        }
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

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_viewModel != null)
        {
            _viewModel.ListViewChanged -= UpdateColumnWidths;
        }
    }

    private void UpdateColumnWidths()
    {
        foreach (var column in this.LineGridView.Columns)
        {
            if (double.IsNaN(column.Width))
            {
                column.Width = 0;
                column.Width = double.NaN;
            }
        }
    }
}
