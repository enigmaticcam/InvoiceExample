using Invoice_Avalonia.Command.InvoiceSearch;
using Invoice_Avalonia.Services.Core;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;

namespace Invoice_Avalonia.ViewModels;

public class InvoiceSearchViewModel : ViewModelBase
{
    public InvoiceSearchViewModel()
    {
        SearchInvoicesCommand = ReactiveCommand.CreateFromTask(SearchInvoices);
    }
    //private List<InvoiceHeaderEntity> _invoices = new();
    //public List<InvoiceHeaderEntity> Invoices
    //{
    //    get => _invoices;
    //    set => this.RaiseAndSetIfChanged(ref _invoices, _invoices);
    //}

    public List<InvoiceHeaderEntity>? Invoices { get; private set; }

    public ReactiveCommand<Unit, Unit> SearchInvoicesCommand { get; }
    private async Task SearchInvoices()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7206");
        var service = new ServiceWrapper(new Client(client));
        var command = new SearchInvoiceCommand(service, new());
        var result = await command.Execute();
        if (result.IsSuccess && result.Obj != null)
        {
            Invoices = result.Obj.Invoices;
            //foreach (var i in result.Obj.Invoices)
            //{
            //    Invoices?.Add(i);
            //}
        }
    }
}
