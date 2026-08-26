using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.InvoiceHeader;

public interface IInvoiceHeaderInvoker
{
    Task<WPFResult> Delete(InvokerToken token, int headerId);
    Task<WPFResult> Get(InvokerToken token, int headerId);
    Task<WPFResult<InvoicePermissionsDTO>> GetPermissions(InvokerToken token, int headerId);
    Task<WPFResult<RandomInvoiceDTO>> GetRandom(InvokerToken token);
    Task<WPFResult<InvoiceUpdateResultDTO>> Update(InvokerToken token, int headerId, int statusTypeId);
    Task<WPFResult> Update(InvokerToken token, int headerId, InvoiceHeaderUpdateDTO update);
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

    public Task<WPFResult> Delete(InvokerToken token, int headerId)
    {
        var command = new InvoiceHeaderDeleteCommand(_service, _state, headerId);
        return _invoker.Perform(token, command);
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

    public Task<WPFResult<RandomInvoiceDTO>> GetRandom(InvokerToken token)
    {
        var command = new InvoiceHeaderGetRandomCommand(_service);
        return _invoker.Perform(token, command);
    }

    public Task<WPFResult<InvoiceUpdateResultDTO>> Update(InvokerToken token, int headerId, int statusTypeId)
    {
        var command = new InvoiceHeaderChangeStatusCommand(_service, _state, headerId, statusTypeId);
        return _invoker.Perform(token, command);
    }

    public Task<WPFResult> Update(InvokerToken token, int headerId, InvoiceHeaderUpdateDTO update)
    {
        var command = new InvoiceHeaderUpdateCommand(_service, _state, headerId, update);
        return _invoker.Perform(token, command);
    }
}
