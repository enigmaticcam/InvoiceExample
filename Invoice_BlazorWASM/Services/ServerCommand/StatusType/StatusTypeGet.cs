using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.StatusType;

public class StatusTypeGet : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IStatusTypeState _state;

    public StatusTypeGet(IServiceWrapper service, IStatusTypeState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.StatusType_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj.Select(x => new DTO_StatusType(x)));
        }
        return result;
    }
}
