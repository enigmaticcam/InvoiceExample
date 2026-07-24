using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services.ServerCommand.ResultStatusType;

public class ResultStatusTypeGet : IServerCommand<BlazorResult>
{
    private IServiceWrapper _service;
    private IResultStatusTypeState _state;

    public ResultStatusTypeGet(IServiceWrapper service, IResultStatusTypeState state)
    {
        _service = service;
        _state = state;
    }

    public async Task<BlazorResult> Execute()
    {
        var result = await _service.ResultStatusType_Get();
        if (result.IsSuccess && result.Obj != null)
        {
            await _state.Set(result.Obj.Select(x => new DTO_ResultStatusType(x)));
        }
        return result;
    }
}
