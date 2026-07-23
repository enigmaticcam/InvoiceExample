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
    private DTO_InvoiceHeader? _invoiceHeader;
    private DTO_InvoiceSummary _summary = new();
    private InvoicePermissionsDTO? _permissions;

    public bool CanEdit => _permissions?.CanEdit ?? false;
    public bool CanDelete => _permissions?.CanDelete ?? false;

    protected override void OnInitialized()
    {
        _standBy.RegisterControl(_controls.ControlAll, _token);
    }

    private async Task LoadData()
    {
        await Task.WhenAll(
            LoadDataHeader(),
            LoadDataDetail(),
            LoadDataPermissions()
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
        await _invoiceDetailInvoker.GetResults(_token, id);
        _summary.Calc(_invoiceDetailState.Items);
    }

    private async Task LoadDataPermissions()
    {
        var result = await _invoiceHeaderInvoker.GetPermissions(_token, id);
        if (result.IsSuccess)
        {
            _permissions = result.Obj;
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

    private bool Disabled()
    {
        return _standBy.Disabled(_controls.ControlAll);
    }

    private record Controls(
        string ControlAll = "ControlAll"
    );
}