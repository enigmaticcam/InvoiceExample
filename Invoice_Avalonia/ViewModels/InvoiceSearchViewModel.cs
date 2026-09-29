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
    private List<InvoiceHeaderEntity>? _invoices;
    public List<InvoiceHeaderEntity>? Invoices
    {
        get => _invoices;
        private set => this.RaiseAndSetIfChanged(ref _invoices, value);
    }

    private int _headerId;
    public int HeaderId
    {
        get => _headerId;
        set => this.RaiseAndSetIfChanged(ref _headerId, value);
    }

    private int _customer;
    public int Customer
    {
        get => _customer;
        set => this.RaiseAndSetIfChanged(ref _customer, value);
    }

    private bool _byHeaderId;
    public bool ByHeaderId
    {
        get => _byHeaderId;
        set => this.RaiseAndSetIfChanged(ref _byHeaderId, value);
    }

    private bool _byCustomer;
    public bool ByCustomer
    {
        get => _byCustomer;
        set => this.RaiseAndSetIfChanged(ref _byCustomer, value);
    }

    public ReactiveCommand<Unit, Unit> SearchInvoicesCommand { get; }
    private async Task SearchInvoices()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7206");
        var service = new ServiceWrapper(new Client(client));
        var filter = GetFilter();
        var command = new SearchInvoiceCommand(service, filter);
        var result = await command.Execute();
        if (result.IsSuccess && result.Obj != null)
        {
            Invoices = result.Obj.Invoices;
        }
    }

    private InvoiceFilterDTO GetFilter()
    {
        return new InvoiceFilterDTO()
        {
            ByCustomer = ByCustomer,
            ByHeader = ByHeaderId,
            Customer = Customer,
            HeaderId = HeaderId,
        };
    }
}
