using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IResultStatusTypeState : IEntityState<int, ResultStatusTypeEntity> { }

public class ResultStatusTypeState : EntityState<int, ResultStatusTypeEntity>, IResultStatusTypeState
{
    protected override int GetId(ResultStatusTypeEntity obj)
    {
        return obj.ResultStatusTypeId;
    }
}
