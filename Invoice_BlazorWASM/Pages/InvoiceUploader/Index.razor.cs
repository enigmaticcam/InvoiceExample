using Invoice_BlazorWASM.Services;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Invoice_BlazorWASM.Pages.InvoiceUploader;

public partial class Index
{

    private BroadcastToken _token = new();
    private StandByControls _standBy = new();
    private Controls _controls = new();
    private IBrowserFile? _file;

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

    private void OnSetFile(InputFileChangeEventArgs e)
    {
        _file = e.File;
    }

    private async Task OnGetDATemplate()
    {
        await _fileDownload.Download("api/invoiceuploader/template", "template.xlsx");
    }

    private async Task OnGetRandom()
    {
        var options = new DialogOptions
        {
            BackdropClick = false,
            CloseButton = false,
            CloseOnEscapeKey = false,
            FullWidth = true
        };
        var result = await _dialog.ShowAsync<RandomInvoice>("Generate Random Invoice", options);
        await result.Result;
    }

    private bool Disabled()
    {
        return _standBy.Disabled(_controls.ControlAll);
    }

    private record Controls(
        string ControlAll = "ControlAll"
    );
}