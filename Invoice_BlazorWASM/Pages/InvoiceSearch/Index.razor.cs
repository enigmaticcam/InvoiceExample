using Invoice_BlazorWASM.Services;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Pages.InvoiceSearch;

public partial class Index
{
    private BroadcastToken _token = new();
    private StandByControls _standBy = new();
    private Controls _controls = new();
    private InvoiceSearchDTO? _search;

    private int FilterHeader => _search?.Filter.HeaderId ?? 0;
    private int FilterCustomer => _search?.Filter.Customer ?? 0;

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

    private async Task OnSearch()
    {
        if (_search != null)
        {
            var result = await _invoiceSearchInvoker.Get(_token, _search.Filter);
            if (result.IsSuccess && result.Obj != null)
            {
                _search = result.Obj;
            }
        }
    }

    private void OnHeaderIdChange(int value)
    {
        _search?.Filter.ByHeader = true;
        _search?.Filter.HeaderId = value;
    }

    private void OnFilterCustomerChange(int value)
    {
        _search?.Filter.ByCustomer = true;
        _search?.Filter.Customer = value;
    }

    private record Controls(
        string ControlAll = "ControlAll"
    );

    private bool Disabled()
    {
        return _standBy.Disabled(_controls.ControlAll);
    }
}