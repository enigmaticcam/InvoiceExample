using Avalonia.Controls;
using Invoice_Avalonia.ViewModels;
using System;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace Invoice_Avalonia.Views;

public partial class InvoiceSearchView : UserControl, IDisposable
{
    private readonly CompositeDisposable _disposables = new();
    public InvoiceSearchView()
    {
        InitializeComponent();

        Observable
            .FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                x => InvoiceIdText.TextChanged += x,
                x => InvoiceIdText.TextChanged -= x
            )
            .Subscribe(x => (DataContext as InvoiceSearchViewModel)?.ByHeaderId = true)
            .DisposeWith(_disposables);

        Observable
            .FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                x => CustomerText.TextChanged += x,
                x => CustomerText.TextChanged -= x
            )
            .Subscribe(x => (DataContext as InvoiceSearchViewModel)?.ByCustomer = true)
            .DisposeWith(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}