using Invoice_WPF.Services;
using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Models;

public class ResultStatusTypeModel : ICopy<ResultStatusTypeModel>
{
    public ResultStatusTypeModel(int resultStatusTypeId, string resultStatusTypeDesc)
    {
        ResultStatusTypeId = resultStatusTypeId;
        ResultStatusTypeDesc = resultStatusTypeDesc;
    }

    public ResultStatusTypeModel(ResultStatusTypeEntity source)
    {
        ResultStatusTypeId = source.ResultStatusTypeId;
        ResultStatusTypeDesc = source.ResultStatusTypeDesc;
    }

    public int ResultStatusTypeId { get; set; }
    public string ResultStatusTypeDesc { get; set; }

    public ResultStatusTypeModel Copy()
    {
        return new ResultStatusTypeModel(
            resultStatusTypeId: ResultStatusTypeId,
            resultStatusTypeDesc: ResultStatusTypeDesc
        );
    }
}
