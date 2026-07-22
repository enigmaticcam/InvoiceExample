using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services;
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

    protected override void OnInitialized()
    {
        _standBy.RegisterControl(_controls.ControlAll, _token);
    }

    private async Task LoadData()
    {
        await Task.WhenAll(
            LoadDataHeader(),
            LoadDataDetail()
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

    private bool Disabled()
    {
        return _standBy.Disabled(_controls.ControlAll);
    }

    private record Controls(
        string ControlAll = "ControlAll"
    );
}