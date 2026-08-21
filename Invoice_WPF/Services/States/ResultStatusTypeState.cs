using Invoice_WPF.Models;
using Invoice_WPF.Services.Entities;

namespace Invoice_WPF.Services.States;

public interface IResultStatusTypeState : IEntityState<int, ResultStatusTypeModel> { }

public class ResultStatusTypeState : EntityState<int, ResultStatusTypeModel>, IResultStatusTypeState
{
    protected override int GetId(ResultStatusTypeModel obj)
    {
        return obj.ResultStatusTypeId;
    }
}
