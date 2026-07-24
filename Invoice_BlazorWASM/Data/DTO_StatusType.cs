using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Data;

public class DTO_StatusType : IEntity<int>
{
    public DTO_StatusType(StatusTypeEntity source)
    {
        StatusTypeId = source.StatusTypeId;
        StatusTypeDesc = source.StatusTypeDesc;
    }

    public DTO_StatusType(int statusTypeId, string statusTypeDesc)
    {
        StatusTypeId = statusTypeId;
        StatusTypeDesc = statusTypeDesc;
    }

    public int StatusTypeId { get; set; }
    public string StatusTypeDesc { get; set; }
    public int Id => StatusTypeId;

    public IEntity<int> Copy()
    {
        return new DTO_StatusType(
            statusTypeId: StatusTypeId,
            statusTypeDesc: StatusTypeDesc
        );
    }
}
