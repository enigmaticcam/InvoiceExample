using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.ServerCommand.InvoiceDetail;

namespace Invoice_BlazorWASM.Services.ServerCommand.InvoiceHeader;

public interface IInvoiceHeaderInvoker
{
    Task<BlazorResult> Get(BroadcastToken token, int headerId);
    Task<BlazorResult<InvoicePermissionsDTO>> GetPermissions(BroadcastToken token, int headerId);
    Task<BlazorResult> RefreshResults(BroadcastToken token, int headerId);
}

public class InvoiceHeaderInvoker : IInvoiceHeaderInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IInvoiceHeaderState _headerState;
    private IInvoiceDetailState _detailState;

    public InvoiceHeaderInvoker(IServerInvoker invoker, IServiceWrapper service, IInvoiceHeaderState state, IInvoiceDetailState detailState)
    {
        _invoker = invoker;
        _service = service;
        _headerState = state;
        _detailState = detailState;
    }

    public Task<BlazorResult> Get(BroadcastToken token, int headerId)
    {
        var command = new InvoiceHeaderGet(_service, _headerState, headerId);
        return _invoker.Perform(command, token);
    }

    public Task<BlazorResult<InvoicePermissionsDTO>> GetPermissions(BroadcastToken token, int headerId)
    {
        var command = new InvoiceHeaderGetPermissions(_service, headerId);
        return _invoker.Perform(command, token);
    }

    public Task<BlazorResult> RefreshResults(BroadcastToken token, int headerId)
    {
        var command = new InvoiceHeaderRefreshResults(_service, _detailState, headerId);
        return _invoker.Perform(command, token);
    }
}
