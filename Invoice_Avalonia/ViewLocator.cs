using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Invoice_Avalonia.ViewModels;
using Invoice_Avalonia.Views;
using System.Diagnostics.CodeAnalysis;

namespace Invoice_Avalonia;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        return param switch
        {
            MainViewModel => new MainWindow(),
            InvoiceSearchViewModel => new InvoiceSearchView(),
            _ => new TextBlock { Text = $"No view for {param?.GetType().Name}" }
        };
    }

    public bool Match(object? data) => data is ViewModelBase;
}
