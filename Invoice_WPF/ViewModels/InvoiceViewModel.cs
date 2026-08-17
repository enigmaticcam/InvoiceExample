using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Observables;
using Invoice_WPF.Services;
using Invoice_WPF.Services.Commands.InvoiceDetail;
using Invoice_WPF.Services.Commands.InvoiceHeader;
using Invoice_WPF.Services.Commands.ResultStatusType;
using Invoice_WPF.Services.Commands.StatusType;
using Invoice_WPF.Services.Core;
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
    private IInvoiceDetailUpdateState _invoiceDetailUpdateState;
    private INavigation _navigation;
    private IResultStatusInvoker _resultStatusInvoker;
    private IResultStatusTypeState _resultStatusTypeState;
    private IStatusTypeInvoker _statusTypeInvoker;
    private IStatusTypeState _statusTypeState;
    private InvokerToken _token;
    private ObservableCollection<InvoiceResultObservable> _detail = new();
    private int _headerId;

    [ObservableProperty]
    public partial InvoiceHeaderObservable? Header { get; set; }
    public InvoiceSummaryObservable Summary { get; set; } = new();
    public ICollectionView? DetailCollectionView { get; private set; }
    private InvoicePermissionsDTO? _permissions;
    public InvoicePermissionsDTO? Permissions
    {
        get => _permissions;
        set
        {
            _permissions = value;
            OnPropertyChanged(nameof(StatusChanges));
            OnPropertyChanged(nameof(CanEdit));
            OnPropertyChanged(nameof(CanDelete));
            IsEditing = false;
        }
    }
    public bool CanEdit => _permissions?.CanEdit ?? false;
    public bool CanDelete => _permissions?.CanDelete ?? false;
    public bool CanChangeStatus => _permissions?.StatusChanges.Count > 0;
    private bool _isEditing;

    public InvoiceViewModel(IInvoiceHeaderInvoker invoiceHeaderInvoker, IInvoiceHeaderState invoiceHeaderState, IInvoiceDetailInvoker invoiceDetailInvoker, IInvoiceDetailState invoiceDetailState, IInvoiceDetailUpdateState invoiceDetailUpdateState, INavigation navigation, IResultStatusInvoker resultStatusInvoker, IResultStatusTypeState resultStatusTypeState, IStatusTypeInvoker statusTypeInvoker, IStatusTypeState statusTypeState, InvokerToken token)
    {
        _invoiceHeaderInvoker = invoiceHeaderInvoker;
        _invoiceHeaderState = invoiceHeaderState;
        _invoiceDetailInvoker = invoiceDetailInvoker;
        _invoiceDetailState = invoiceDetailState;
        _invoiceDetailUpdateState = invoiceDetailUpdateState;
        _navigation = navigation;
        _resultStatusInvoker = resultStatusInvoker;
        _resultStatusTypeState = resultStatusTypeState;
        _statusTypeInvoker = statusTypeInvoker;
        _statusTypeState = statusTypeState;
        _token = token;
    }

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

    public ObservableCollection<DynamicButton> StatusChanges
    {
        get
        {
            var list = new ObservableCollection<DynamicButton>();
            if (_permissions?.StatusChanges != null)
            {
                foreach (var status in _permissions.StatusChanges)
                {
                    if (_statusTypeState.Contains(status))
                    {
                        list.Add(new DynamicButton(_statusTypeState.GetText(status), new AsyncRelayCommand(() => ChangeStatus(status))));
                    }
                }
            }
            return list;
        }
    }

    public string? StatusTypeText
    {
        get => _statusTypeState.GetText(Header?.StatusTypeId);
    }

    public async Task LoadData(int id)
    {
        _headerId = id;
        await Task.WhenAll(
            LoadDataHeader(id),
            LoadDataDetail(id),
            LoadDataResultStatusType(),
            LoadDataStatusType(),
            LoadDataPermissions(id)
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

    private async Task LoadDataPermissions(int id)
    {
        var result = await _invoiceHeaderInvoker.GetPermissions(_token, id);
        if (result.IsSuccess)
        {
            Permissions = result.Obj;
        }
    }

    private async Task ChangeStatus(int statusId)
    {
        var result = await _invoiceHeaderInvoker.Update(_token, _headerId, statusId);
        if (result.IsSuccess)
        {
            Header = new(_invoiceHeaderState.Get(_headerId));
            if (result.Obj != null && result.Obj.Permissions != null)
            {
                Permissions = result.Obj.Permissions;
            }
        }
    }

    public async Task Delete()
    {
        var result = await _invoiceHeaderInvoker.Delete(_token, _headerId);
        if (result.IsSuccess)
        {
            await _navigation.NavigateToInvoiceSearchView();
        }
    }

    public void Dispose()
    {

    }
}



