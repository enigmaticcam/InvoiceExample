using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Services.ServerCommand.StatusType;

public interface IStatusTypeState : IEntityState<int, DTO_StatusType>
{
    string GetText(int id);
}

public class StatusTypeState : EntityState<int, DTO_StatusType>, IStatusTypeState
{
    public StatusTypeState(IClearCollection clearCollection) : base(clearCollection)
    {
    }

    public override string EntityName => "StatusTypeState";

    public string GetText(int id)
    {
        if (Contains(id))
        {
            return Get(id).StatusTypeDesc;
        }
        return "";
    }
}
