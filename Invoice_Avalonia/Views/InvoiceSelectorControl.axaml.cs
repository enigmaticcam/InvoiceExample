using Avalonia;
using Avalonia.Controls;
using Invoice_Avalonia.Services.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace Invoice_Avalonia.Views;

public partial class InvoiceSelectorControl : UserControl
{
    public InvoiceSelectorControl()
    {
        InitializeComponent();
        //Invoices = new List<InvoiceHeaderEntity>()
        //{
        //    new InvoiceHeaderEntity() { InvoiceHeaderId = 15 }
        //};
    }

    private List<InvoiceHeaderEntity> _invoices = new List<InvoiceHeaderEntity>() { new InvoiceHeaderEntity() { InvoiceHeaderId = 15 } };
    public List<InvoiceHeaderEntity> Invoices
    {
        get => _invoices;
        set => SetAndRaise(InvoicesProperty, ref _invoices, value);
    }

    //public List<InvoiceHeaderEntity> Invoices
    //{
    //    get => GetValue(InvoicesProperty);
    //    set => SetValue(InvoicesProperty, value);
    //}

    private InvoiceHeaderEntity? _selectedInvoice;
    public InvoiceHeaderEntity? SelectedInvoice
    {
        get => _selectedInvoice;
        set => SetAndRaise(SelectedInvoiceProperty, ref _selectedInvoice, value);
    }

    public ICommand? OpenCommand
    {
        get => GetValue(OpenCommandProperty);
        set => SetValue(OpenCommandProperty, value);
    }

    public static readonly DirectProperty<InvoiceSelectorControl, List<InvoiceHeaderEntity>> InvoicesProperty =
        AvaloniaProperty.RegisterDirect<InvoiceSelectorControl, List<InvoiceHeaderEntity>>(
            nameof(Invoices),
            o => o.Invoices,
            (o, v) => o.Invoices = v);
    //public static readonly StyledProperty<List<InvoiceHeaderEntity>> InvoicesProperty =
    //    AvaloniaProperty.Register<InvoiceSelectorControl, List<InvoiceHeaderEntity>>(nameof(Invoices));

    public static readonly DirectProperty<InvoiceSelectorControl, InvoiceHeaderEntity?> SelectedInvoiceProperty =
        AvaloniaProperty.RegisterDirect<InvoiceSelectorControl, InvoiceHeaderEntity?>(
            nameof(SelectedInvoice),
            o => o.SelectedInvoice,
            (o, v) => o.SelectedInvoice = v);

    public static readonly StyledProperty<ICommand?> OpenCommandProperty =
        AvaloniaProperty.Register<InvoiceSelectorControl, ICommand?>(nameof(OpenCommand));
}