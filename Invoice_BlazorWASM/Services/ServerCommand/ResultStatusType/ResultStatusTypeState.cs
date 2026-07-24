using Invoice_BlazorWASM.Data;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Services.ServerCommand.ResultStatusType;

public interface IResultStatusTypeState : IEntityState<int, DTO_ResultStatusType>
{
    string GetText(int id);
}

public class ResultStatusTypeState : EntityState<int, DTO_ResultStatusType>, IResultStatusTypeState
{
    public ResultStatusTypeState(IClearCollection clearCollection) : base(clearCollection)
    {
    }

    public override string EntityName => "ResultStatusTypeState";

    public string GetText(int id)
    {
        if (Contains(id))
        {
            return Get(id).ResultStatustypeDesc;
        }
        return "";
    }
}
