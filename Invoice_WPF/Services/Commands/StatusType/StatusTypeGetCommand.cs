using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.StatusType;

public class StatusTypeGetCommand : IServerCommand<WPFResult>
{
    private IServiceWrapper _service;
    private IStatusTypeState _state;

    public StatusTypeGetCommand(IServiceWrapper service, IStatusTypeState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<WPFResult> Execute()
    {
        var result = await _service.StatusType_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }

}
