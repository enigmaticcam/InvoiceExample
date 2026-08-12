using CommunityToolkit.Mvvm.ComponentModel;
using Invoice_WPF.Observables;
using Invoice_WPF.Services.Commands.InvoiceDetail;
using Invoice_WPF.Services.Commands.InvoiceHeader;
using Invoice_WPF.Services.Commands.ResultStatusType;
using Invoice_WPF.Services.Commands.StatusType;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Invoice_WPF.ViewModels;

public partial class InvoiceViewModel : ViewModelBase, IDisposable
{
    private IInvoiceHeaderInvoker _invoiceHeaderInvoker;
    private IInvoiceHeaderState _invoiceHeaderState;
    private IInvoiceDetailInvoker _invoiceDetailInvoker;
    private IInvoiceDetailState _invoiceDetailState;
    private IResultStatusInvoker _resultStatusInvoker;
    private IResultStatusTypeState _resultStatusTypeState;
    private IStatusTypeInvoker _statusTypeInvoker;
    private IStatusTypeState _statusTypeState;
    private InvokerToken _token;
    private ObservableCollection<InvoiceResultObservable> _detail = new();

    public InvoiceViewModel(IInvoiceHeaderInvoker invoiceHeaderInvoker, IInvoiceHeaderState invoiceHeaderState, IInvoiceDetailInvoker invoiceDetailInvoker, IInvoiceDetailState invoiceDetailState, IResultStatusInvoker resultStatusInvoker, IResultStatusTypeState resultStatusTypeState, IStatusTypeInvoker statusTypeInvoker, IStatusTypeState statusTypeState, InvokerToken token)
    {
        _invoiceHeaderInvoker = invoiceHeaderInvoker;
        _invoiceHeaderState = invoiceHeaderState;
        _invoiceDetailInvoker = invoiceDetailInvoker;
        _invoiceDetailState = invoiceDetailState;
        _resultStatusInvoker = resultStatusInvoker;
        _resultStatusTypeState = resultStatusTypeState;
        _statusTypeInvoker = statusTypeInvoker;
        _statusTypeState = statusTypeState;
        _token = token;
    }

    [ObservableProperty]
    public partial InvoiceHeaderObservable? Header { get; set; }
    public InvoiceSummaryObservable Summary { get; set; } = new();
    public ICollectionView? DetailCollectionView { get; private set; }

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
            await _invoiceHeaderInvoker.Get(_token, id);
        }
        if (_invoiceHeaderState.Contains(id))
        {
            Header = new(_invoiceHeaderState.Get(id));
        }
    }

    private async Task LoadDataDetail(int id)
    {
        var result = await _invoiceDetailInvoker.Get(_token, id);
        if (result.IsSuccess)
        {
            foreach (var line in _invoiceDetailState.Items)
            {
                _detail.Add(new InvoiceResultObservable(line));
            }
            DetailCollectionView = CollectionViewSource.GetDefaultView(_detail);
            Summary.Calc(_detail);
        }
    }

    private async Task LoadDataResultStatusType()
    {
        if (!_resultStatusTypeState.IsLoaded)
        {
            await _resultStatusInvoker.Get(_token);
        }
    }

    private async Task LoadDataStatusType()
    {
        if (!_statusTypeState.IsLoaded)
        {
            await _statusTypeInvoker.Get(_token);
        }
    }

    public void Dispose()
    {

    }
}
