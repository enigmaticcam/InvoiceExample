using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Invoice_WPF.Models;
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
    private INavigation _navigation;
    private IResultStatusInvoker _resultStatusInvoker;
    private IResultStatusTypeState _resultStatusTypeState;
    private IStatusTypeInvoker _statusTypeInvoker;
    private IStatusTypeState _statusTypeState;
    private InvokerToken _token;

    public InvoiceViewModel(IInvoiceHeaderInvoker invoiceHeaderInvoker, IInvoiceHeaderState invoiceHeaderState, IInvoiceDetailInvoker invoiceDetailInvoker, IInvoiceDetailState invoiceDetailState, INavigation navigation, IResultStatusInvoker resultStatusInvoker, IResultStatusTypeState resultStatusTypeState, IStatusTypeInvoker statusTypeInvoker, IStatusTypeState statusTypeState, InvokerToken token)
    {
        _invoiceHeaderInvoker = invoiceHeaderInvoker;
        _invoiceHeaderState = invoiceHeaderState;
        _invoiceDetailInvoker = invoiceDetailInvoker;
        _invoiceDetailState = invoiceDetailState;
        _navigation = navigation;
        _resultStatusInvoker = resultStatusInvoker;
        _resultStatusTypeState = resultStatusTypeState;
        _statusTypeInvoker = statusTypeInvoker;
        _statusTypeState = statusTypeState;
        _token = token;
        SaveHeaderCommand = new AsyncRelayCommand(SaveHeader);
        SaveDetailCommand = new AsyncRelayCommand(SaveDetail);
        CancelSaveCommand = new AsyncRelayCommand(CancelSaveDetail);
        ToggleEditHeaderCommand = new AsyncRelayCommand(ToggleEditHeader);
        ToggleEditDetailCommand = new AsyncRelayCommand(ToggleEditDetail);
        _token.OnRunning += SetIsRunning;
    }
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
            IsEditingHeader = false;
        }
    }
    public bool CanEdit => _permissions?.CanEdit ?? false;
    public bool CanDelete => _permissions?.CanDelete ?? false;
    public bool CanChangeStatus => _permissions?.StatusChanges.Count > 0;
    public bool CanSaveDetail => _detail.Any(x => x.IsChanged) && IsEditingDetail;
    public Action? ListViewChanged { get; set; }
    public IAsyncRelayCommand SaveDetailCommand { get; set; }
    public IAsyncRelayCommand SaveHeaderCommand { get; set; }
    public IAsyncRelayCommand CancelSaveCommand { get; set; }
    public IAsyncRelayCommand ToggleEditHeaderCommand { get; set; }
    public IAsyncRelayCommand ToggleEditDetailCommand { get; set; }

    private bool _isRunning;
    public bool IsRunning
    {
        get => _isRunning;
        set
        {
            _isRunning = value;
            OnPropertyChanged(nameof(IsRunning));
            OnPropertyChanged(nameof(IsNotRunning));
            OnPropertyChanged(nameof(CanSaveDetail));
        }
    }

    private bool _isEditingHeader;
    public bool IsEditingHeader
    {
        get => _isEditingHeader;
        set
        {
            _isEditingHeader = value;
            OnPropertyChanged(nameof(IsEditingHeader));
            OnPropertyChanged(nameof(IsNotEditingHeader));
            OnPropertyChanged(nameof(CanEnableHeaderEditing));
            OnPropertyChanged(nameof(CanDisableHeaderEditing));
        }
    }

    private bool _isEditingDetail;
    public bool IsEditingDetail
    {
        get => _isEditingDetail;
        set
        {
            _isEditingDetail = value;
            OnPropertyChanged(nameof(IsEditingDetail));
            OnPropertyChanged(nameof(IsNotEditingDetail));
            OnPropertyChanged(nameof(CanSaveDetail));
            OnPropertyChanged(nameof(CanEnableDetailEditing));
            OnPropertyChanged(nameof(CanDisableDetailEditing));
        }
    }

    public bool IsNotEditingHeader => !_isEditingHeader;
    public bool IsNotEditingDetail => !_isEditingDetail;
    public bool IsNotRunning => !IsRunning;
    public bool CanEnableHeaderEditing => !IsEditingHeader && CanEdit;
    public bool CanDisableHeaderEditing => IsEditingHeader;
    public bool CanEnableDetailEditing => !IsEditingDetail && CanEdit;
    public bool CanDisableDetailEditing => IsEditingDetail;

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

    private Task SetIsRunning(bool isRunning)
    {
        IsRunning = isRunning;
        return Task.CompletedTask;
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
            LoadDataDetail(_invoiceDetailState.Items);
        }
    }

    private void LoadDataDetail(IEnumerable<InvoiceFullResultModel> items)
    {
        _detail.Clear();
        foreach (var line in items)
        {
            var add = new InvoiceResultObservable(line);
            add.ChangedEvent += LineChanged;
            _detail.Add(add);
        }
        ListViewChanged?.Invoke();
        DetailCollectionView = CollectionViewSource.GetDefaultView(_detail);
        Summary.Calc(_detail);
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

    private void LineChanged()
    {
        OnPropertyChanged(nameof(CanSaveDetail));
        OnPropertyChanged(nameof(CanEnableHeaderEditing));
        ListViewChanged?.Invoke();
    }

    private async Task SaveDetail()
    {
        var updates = _detail
            .Where(x => x.IsChanged)
            .Select(x => new InvoiceDetailUpdateDTO()
            {
                ApprovedRate = x.ApprovedRate,
                InvoiceDetailId = x.InvoiceDetailId
            });
        if (updates.Count() > 0)
        {
            var result = await _invoiceDetailInvoker.Update(_token, _headerId, updates);
            if (result.IsSuccess)
            {
                LoadDataDetail(_invoiceDetailState.Items);
                IsEditingDetail = false;
            }
        }
    }

    private async Task SaveHeader()
    {
        if (Header != null)
        {
            var result = await _invoiceHeaderInvoker.Update(_token, _headerId, new InvoiceHeaderUpdateDTO()
            {
                Description = Header.Description,
                HeaderId = _headerId
            });
            if (result.IsSuccess)
            {
                await LoadDataHeader(_headerId);
                IsEditingHeader = false;
            }
        }
    }

    private Task CancelSaveDetail()
    {
        foreach (var item in _detail)
        {
            if (item.IsChanged)
            {
                _invoiceDetailState.Reset(item.InvoiceDetailId);
            }
        }
        LoadDataDetail(_invoiceDetailState.Items);
        IsEditingDetail = false;
        return Task.CompletedTask;
    }

    private async Task CancelSaveHeader()
    {
        _invoiceHeaderState.Reset(_headerId);
        await LoadDataHeader(_headerId);
        IsEditingHeader = false;
    }

    private async Task ToggleEditDetail()
    {
        if (IsEditingDetail)
        {
            await CancelSaveDetail();
        }
        else
        {
            IsEditingDetail = true;
        }
    }

    private async Task ToggleEditHeader()
    {
        if (IsEditingHeader)
        {
            await CancelSaveHeader();
        }
        else
        {
            IsEditingHeader = true;
        }
    }

    public void Dispose()
    {
        _token.OnRunning -= SetIsRunning;
    }
}



