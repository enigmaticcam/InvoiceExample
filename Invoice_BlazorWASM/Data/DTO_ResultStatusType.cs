using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Data;

public class DTO_ResultStatusType : IEntity<int>
{
    public DTO_ResultStatusType(ResultStatusTypeEntity source)
    {
        ResultStatustypeId = source.ResultStatusTypeId;
        ResultStatustypeDesc = source.ResultStatusTypeDesc;
    }

    public DTO_ResultStatusType(int resultStatustypeId, string resultStatustypeDesc)
    {
        ResultStatustypeId = resultStatustypeId;
        ResultStatustypeDesc = resultStatustypeDesc;
    }

    public int ResultStatustypeId { get; set; }
    public string ResultStatustypeDesc { get; set; }
    public int Id => ResultStatustypeId;

    public IEntity<int> Copy()
    {
        return new DTO_ResultStatusType(
            resultStatustypeId: ResultStatustypeId,
            resultStatustypeDesc: ResultStatustypeDesc
        );
    }
}
