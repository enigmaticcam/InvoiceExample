using CommunityToolkit.Mvvm.ComponentModel;
using Invoice_WPF.Observables;
using Invoice_WPF.Services.Commands.InvoiceDetail;
using Invoice_WPF.Services.Commands.InvoiceHeader;
using Invoice_WPF.Services.Commands.ResultStatusType;
using Invoice_WPF.Services.Commands.StatusType;
using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;
using System.Collections.ObjectModel;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceViewModel : ViewModelBase
{
    private IServiceWrapper _serviceWrapper;
    private IInvoiceHeaderState _invoiceHeaderState;
    private IInvoiceDetailState _invoiceDetailState;
    private IResultStatusTypeState _resultStatusTypeState;
    private IStatusTypeState _statusTypeState;
    private ObservableCollection<InvoiceResultObservable> _detail = new();

    public InvoiceViewModel(IServiceWrapper serviceWrapper, IInvoiceHeaderState invoiceHeaderState, IInvoiceDetailState invoiceDetailState, IResultStatusTypeState resultStatusTypeState, IStatusTypeState statusTypeState)
    {
        _serviceWrapper = serviceWrapper;
        _invoiceHeaderState = invoiceHeaderState;
        _invoiceDetailState = invoiceDetailState;
        _resultStatusTypeState = resultStatusTypeState;
        _statusTypeState = statusTypeState;
    }

    [ObservableProperty]
    public partial InvoiceHeaderObservable? Header { get; set; }

    public string? StatusTypeText
    {
        get => _statusTypeState.GetText(Header?.StatusTypeId);
    }

    public async Task LoadData(int id)
    {
        await Task.WhenAll(
            LoadDataHeader(id),
            LoadDataDetail(id),
            LoadDataResultStatusType(),
            LoadDataStatusType()
        );
    }

    private async Task LoadDataHeader(int id)
    {
        if (!_invoiceHeaderState.IsLoaded || !_invoiceHeaderState.Contains(id))
        {
            var command = new InvoiceHeaderGetCommand(_serviceWrapper, _invoiceHeaderState);
            await command.Perform(id);
        }
        if (_invoiceHeaderState.Contains(id))
        {
            Header = new(_invoiceHeaderState.Get(id));
        }
    }

    private async Task LoadDataDetail(int id)
    {
        var command = new InvoiceDetailGetCommand(_serviceWrapper, _invoiceDetailState);
        var result = await command.Perform(id);
        if (result.IsSuccess)
        {
            foreach (var line in _invoiceDetailState.Items)
            {
                _detail.Add(new InvoiceResultObservable(line));
            }
        }
    }

    private async Task LoadDataResultStatusType()
    {
        if (!_resultStatusTypeState.IsLoaded)
        {
            var command = new ResultStatusTypeGetCommand(_serviceWrapper, _resultStatusTypeState);
            await command.Perform();
        }
    }

    private async Task LoadDataStatusType()
    {
        if (!_statusTypeState.IsLoaded)
        {
            var command = new StatusTypeGetCommand(_serviceWrapper, _statusTypeState);
            await command.Perform();
        }
    }
}
