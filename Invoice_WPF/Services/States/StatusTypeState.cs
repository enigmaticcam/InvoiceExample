using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IStatusTypeState : IEntityState<int, StatusTypeEntity>
{
    string GetText(int? statusTypeId);
}

public class StatusTypeState : EntityState<int, StatusTypeEntity>, IStatusTypeState
{
    public string GetText(int? statusTypeId)
    {
        if (statusTypeId != null && Contains(statusTypeId.Value))
        {
            return Get(statusTypeId.Value).StatusTypeDesc;
        }
        return "";
    }

    protected override int GetId(StatusTypeEntity obj)
    {
        return obj.StatusTypeId;
    }
}
