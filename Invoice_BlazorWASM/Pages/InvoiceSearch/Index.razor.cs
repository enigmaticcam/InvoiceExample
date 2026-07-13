using Invoice_BlazorWASM.Services;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Pages.InvoiceSearch;

public partial class Index
{
    private BroadcastToken _token = new();
    private StandByControls _standBy = new();
    private Controls _controls = new();
    private InvoiceSearchDTO? _search;

    protected override void OnInitialized()
    {
        _standBy.RegisterControl(_controls.ControlAll, _token);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
        }
    }

    private async Task LoadData()
    {
        var result = await _invoiceSearchInvoker.Get(_token);
        if (result.IsSuccess && result.Obj != null)
        {
            _search = result.Obj;
            await InvokeAsync(StateHasChanged);
        }
    }

    private record Controls(
        string ControlAll = "ControlAll"
    );

    private bool Disabled()
    {
        return _standBy.Disabled(_controls.ControlAll);
    }
}