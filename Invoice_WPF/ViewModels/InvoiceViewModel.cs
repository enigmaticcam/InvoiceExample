using Invoice_WPF.Observables;
using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;
using System.Collections.ObjectModel;

namespace Invoice_WPF.ViewModels;

public class InvoiceViewModel : ViewModelBase
{
    private IServiceWrapper _serviceWrapper;
    private IInvoiceHeaderState _invoiceHeaderState;
    private IResultStatusTypeState _resultStatusTypeState;
    private IStatusTypeState _statusTypeState;
    private ObservableCollection<InvoiceResultObservable> _detail = new();

    public InvoiceViewModel(IServiceWrapper serviceWrapper, IInvoiceHeaderState invoiceHeaderState, IResultStatusTypeState resultStatusTypeState, IStatusTypeState statusTypeState)
    {
        _serviceWrapper = serviceWrapper;
        _invoiceHeaderState = invoiceHeaderState;
        _resultStatusTypeState = resultStatusTypeState;
        _statusTypeState = statusTypeState;
    }

    public InvoiceHeaderObservable? Header { get; private set; }

    public async Task LoadData(int id)
    {
        await Task.WhenAll(
            LoadDataHeader(id),
            LoadDataDetail(id),
            LoadDataResultStatusType(),
            LoadDataStatusType());
    }

    private async Task LoadDataHeader(int id)
    {
        if (!_invoiceHeaderState.IsLoaded || !_invoiceHeaderState.Contains(id))
        {
            await _serviceWrapper.InvoiceHeader_Get(id);
            if (_invoiceHeaderState.IsLoaded && _invoiceHeaderState.Contains(id))
            {
                Header = new(_invoiceHeaderState.Get(id));
            }
        }
        else
        {
            Header = new(_invoiceHeaderState.Get(id));
        }
    }

    private async Task LoadDataDetail(int id)
    {
        var result = await _serviceWrapper.InvoiceHeader_GetResults(id);
        if (result.IsSuccess && result.Obj != null)
        {
            foreach (var line in result.Obj)
            {
                _detail.Add(new InvoiceResultObservable(line));
            }
        }
    }

    private async Task LoadDataResultStatusType()
    {
        if (!_resultStatusTypeState.IsLoaded)
        {
            var result = await _serviceWrapper.ResultStatusType_Get();
            if (result.IsSuccess && result.Obj != null)
            {
                await _resultStatusTypeState.Set(result.Obj);
            }
        }
    }

    private async Task LoadDataStatusType()
    {
        if (!_statusTypeState.IsLoaded)
        {
            var result = await _serviceWrapper.StatusType_Get();
            if (result.IsSuccess && result.Obj != null)
            {
                await _statusTypeState.Set(result.Obj);
            }
        }
    }
}
