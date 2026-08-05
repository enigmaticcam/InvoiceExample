using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IStatusTypeState : IEntityState<int, StatusTypeEntity> { }

public class StatusTypeState : EntityState<int, StatusTypeEntity>, IStatusTypeState
{
    protected override int GetId(StatusTypeEntity obj)
    {
        return obj.StatusTypeId;
    }
}
