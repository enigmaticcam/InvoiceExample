using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services;
using Invoice_BlazorWASM.Services.Core;
using Microsoft.AspNetCore.Components;

namespace Invoice_BlazorWASM.Pages.Invoice;

public partial class Index
{
    [Parameter] public int id { get; set; }
    private BroadcastToken _token = new();
    private StandByControls _standBy = new();
    private Controls _controls = new();
    private ResponseMessage? _message;
    private DTO_InvoiceHeader? _invoiceHeader;
    private DTO_InvoiceSummary _summary = new();
    private InvoicePermissionsDTO? _permissions;

    public bool CanEdit => _permissions?.CanEdit ?? false;
    public bool CanDelete => _permissions?.CanDelete ?? false;
    public bool CanChangeStatus => _permissions?.StatusChanges.Count > 0;
    public List<int> StatusChanges => _permissions?.StatusChanges ?? new List<int>();
    public string StatusText => _statusTypeState.GetText(_invoiceHeader?.StatusTypeId ?? -1);

    protected override void OnInitialized()
    {
        _standBy.RegisterControl(_controls.ControlAll, _token);
    }

    private async Task LoadData()
    {
        await Task.WhenAll(
            LoadDataHeader(),
            LoadDataDetail(),
            LoadDataPermissions(),
            LoadDataResultStatusType(),
            LoadDataStatustype()
        );
        await InvokeAsync(StateHasChanged);
    }

    private async Task LoadDataHeader()
    {
        if (_invoiceHeader == null)
        {
            if (!_invoiceHeaderState.Contains(id))
            {
                await _invoiceHeaderInvoker.Get(_token, id);
            }
            if (_invoiceHeaderState.Contains(id))
            {
                _invoiceHeader = _invoiceHeaderState.Get(id);
            }
        }
    }

    private async Task LoadDataDetail()
    {
        var result = await _invoiceDetailInvoker.GetResults(_token, id);
        if (result.IsSuccess)
        {
            _summary.Calc(_invoiceDetailState.Items);
        }
    }

    private async Task LoadDataPermissions()
    {
        var result = await _invoiceHeaderInvoker.GetPermissions(_token, id);
        if (result.IsSuccess)
        {
            _permissions = result.Obj;
        }
    }

    private async Task LoadDataResultStatusType()
    {
        if (!_resultStatusTypeState.IsLoaded)
        {
            await _resultStatusTypeInvoker.Get(_token);
        }
    }

    private async Task LoadDataStatustype()
    {
        if (!_statusTypeState.IsLoaded)
        {
            await _statusTypeInvoker.Get(_token);
        }
    }

    private async Task OnRefreshResults()
    {
        var result = await _invoiceHeaderInvoker.RefreshResults(_token, id);
        if (result.IsSuccess)
        {
            _summary.Calc(_invoiceDetailState.Items);
        }
    }

    private async Task OnDelete()
    {
        var result = await _invoiceHeaderInvoker.Delete(_token, id);
        if (result.IsSuccess)
        {
            _navigation.NavigateTo("/");
        }
    }

    private async Task OnChangeStatus(int statusId)
    {
        var status = _statusTypeState.Get(statusId);
        bool? request = await _dialog.ShowMessageBoxAsync(
            title: "Warning",
            message: $"Are you sure you want to change status to {status.StatusTypeDesc}?",
            yesText: "Yes",
            cancelText: "No");
        if (request.HasValue && request.Value)
        {
            var result = await _invoiceHeaderInvoker.Update(_token, id, statusId);
            if (result.IsSuccess && result.Obj != null)
            {
                if (_message != null)
                {
                    _message.SendMessage($"Invoice status successfully changed to {status.StatusTypeDesc}", MudBlazor.Severity.Normal);
                }
                _invoiceHeader = new DTO_InvoiceHeader(result.Obj);
                _permissions = null;
                await LoadDataPermissions();
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    private bool Disabled()
    {
        return _standBy.Disabled(_controls.ControlAll);
    }

    private Func<DTO_InvoiceDetail, int, string> RowStyle => (line, i) =>
    {
        return LineColor(line.HasFailedRate, line.HasFailedCase);
    };

    private string LineColor(bool? isFailedRate, bool? isFailedCase)
    {
        bool caseFail = isFailedCase ?? false;
        bool rateFail = isFailedRate ?? false;
        if (rateFail && caseFail)
        {
            return "background-color:lightpink";
        }
        else if (caseFail)
        {
            return "background-color:lightyellow";
        }
        else if (rateFail)
        {
            return "background-color:lightcyan";
        }
        else
        {
            return "";
        }
    }

    private record Controls(
        string ControlAll = "ControlAll"
    );
}