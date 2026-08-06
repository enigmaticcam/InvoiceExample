using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.StatusType;

public class StatusTypeGetCommand
{
    private IServiceWrapper _service;
    private IStatusTypeState _state;

    public StatusTypeGetCommand(IServiceWrapper service, IStatusTypeState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<WPFResult> Perform()
    {
        var result = await _service.StatusType_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }
}
