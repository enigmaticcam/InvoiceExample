using Invoice_BlazorWASM.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace Invoice_BlazorWASM.Pages.InvoiceUploader;

public partial class Index
{

    private BroadcastToken _token = new();
    private StandByControls _standBy = new();
    private Controls _controls = new();
    private IBrowserFile? _file;
    private string? _randomInvoice;

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
        if (!_invoiceUploaderState.IsLoaded)
        {
            await _invoiceUploaderInvoker.Get(_token);
        }
    }

    private async Task OnGetRandom()
    {
        var result = await _invoiceUploaderInvoker.GetRandom(_token);
        if (result.IsSuccess && result.Obj != null)
        {
            _randomInvoice = result.Obj;
            await InvokeAsync(StateHasChanged);
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