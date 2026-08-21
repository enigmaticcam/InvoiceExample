using Invoice_WPF.Models;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IStatusTypeState : IEntityState<int, StatusTypeModel>
{
    string GetText(int? statusTypeId);
}

public class StatusTypeState : EntityState<int, StatusTypeModel>, IStatusTypeState
{
    public string GetText(int? statusTypeId)
    {
        if (statusTypeId != null && Contains(statusTypeId.Value))
        {
            return Get(statusTypeId.Value).StatusTypeDesc;
        }
        return "";
    }

    protected override int GetId(StatusTypeModel obj)
    {
        return obj.StatusTypeId;
    }
}
