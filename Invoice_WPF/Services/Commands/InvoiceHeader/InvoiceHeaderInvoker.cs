using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public interface IInvoiceHeaderInvoker
{
    Task<WPFResult> Get(InvokerToken token, int headerId);
    Task<WPFResult<InvoicePermissionsDTO>> GetPermissions(InvokerToken token, int headerId);
}

public class InvoiceHeaderInvoker : IInvoiceHeaderInvoker
{
    private IServerInvoker _invoker;
    private IServiceWrapper _service;
    private IInvoiceHeaderState _state;

    public InvoiceHeaderInvoker(IServerInvoker invoker, IServiceWrapper service, IInvoiceHeaderState state)
    {
        _invoker = invoker;
        _service = service;
        _state = state;
    }

    public Task<WPFResult> Get(InvokerToken token, int headerId)
    {
        var command = new InvoiceHeaderGetCommand(_service, _state, headerId);
        return _invoker.Perform(token, command);
    }

    public Task<WPFResult<InvoicePermissionsDTO>> GetPermissions(InvokerToken token, int headerId)
    {
        var command = new InvoiceHeaderGetPermissionsCommand(_service, headerId);
        return _invoker.Perform(token, command);
    }
}
