using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.States;

namespace Invoice_WPF.Services.Commands.ResultStatusType;

public class ResultStatusTypeGetCommand
{
    private IServiceWrapper _service;
    private IResultStatusTypeState _state;

    public ResultStatusTypeGetCommand(IServiceWrapper service, IResultStatusTypeState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<WPFResult> Perform()
    {
        var result = await _service.ResultStatusType_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj);
        }
        return result;
    }
}
