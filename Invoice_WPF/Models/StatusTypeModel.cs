using Invoice_WPF.Services;
using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Models;

public class StatusTypeModel : ICopy<StatusTypeModel>
{
    public StatusTypeModel(int statusTypeId, string statusTypeDesc)
    {
        StatusTypeId = statusTypeId;
        StatusTypeDesc = statusTypeDesc;
    }

    public StatusTypeModel(StatusTypeEntity source)
    {
        StatusTypeId = source.StatusTypeId;
        StatusTypeDesc = source.StatusTypeDesc;
    }

    public int StatusTypeId { get; set; }
    public string StatusTypeDesc { get; set; }

    public StatusTypeModel Copy()
    {
        return new StatusTypeModel(
            statusTypeId: StatusTypeId,
            statusTypeDesc: StatusTypeDesc
        );
    }
}
