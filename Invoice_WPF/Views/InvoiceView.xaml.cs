using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Invoice_WPF.Views;
/// <summary>
/// Interaction logic for InvoiceView.xaml
/// </summary>
public partial class InvoiceView : UserControl, INotifyPropertyChanged
{
    public InvoiceView()
    {
        InitializeComponent();
    }

    private bool _isEditing;
    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            _isEditing = value;
            OnPropertyChanged(nameof(IsEditing));
            OnPropertyChanged(nameof(IsNotEditing));
        }
    }

    public bool IsNotEditing => !IsEditing;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
