using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Data.EF;

namespace Invoice_Logic.Repositories.DbEntities.Objects;

public static class Mapper
{
    public static InvoiceDetail ToEf(InvoiceDetailCreateDTO source)
    {
        return new InvoiceDetail()
        {
            ApprovedRate = source.ApprovedRate,
            Cases = source.Cases,
            CustItemCode = source.CustItemCode,
            CustItemDesc = source.CustItemDesc,
            CustomerRate = source.CustomerRate
        };
    }

    public static InvoiceHeader ToEf(InvoiceHeaderCreateDTO source)
    {
        return new InvoiceHeader()
        {
            Customer = source.Customer,
            Description = source.Description,
            InvoiceDate = source.InvoiceDate,
            StatusTypeId = source.StatusTypeId
        };
    }

    public static InvoiceDetailEntity FromEf(InvoiceDetail source)
    {
        return new InvoiceDetailEntity(
            InvoiceDetailId: source.InvoiceDetailId,
            InvoiceHeaderId: source.InvoiceHeaderId,
            CustItemCode: source.CustItemCode,
            CustItemDesc: source.CustItemDesc,
            CustomerRate: source.CustomerRate,
            ApprovedRate: source.ApprovedRate,
            Cases: source.Cases
        );
    }

    public static InvoiceHeaderEntity FromEf(InvoiceHeader source)
    {
        return new InvoiceHeaderEntity(
            InvoiceHeaderId: source.InvoiceHeaderId,
            Customer: source.Customer,
            InvoiceDate: source.InvoiceDate,
            StatusTypeId: source.StatusTypeId,
            Description: source.Description
        );
    }

    public static InvoiceResultEntity FromEf(InvoiceResult source)
    {
        return new InvoiceResultEntity(
            InvoiceResultId: source.InvoiceResultId,
            InvoiceDetailId: source.InvoiceDetailId,
            InvoiceHeaderId: source.InvoiceHeaderId,
            OurItemCode: source.OurItemCode,
            CasesRemaining: source.CasesRemaining,
            HasFailedCase: source.HasFailedCase,
            HasFailedRate: source.HasFailedRate,
            ResultStatusTypeId: source.ResultStatusTypeId
        );
    }

    public static List<InvoiceResultEntity> FromEf(IEnumerable<InvoiceResult> source)
    {
        return source
            .Select(FromEf)
            .ToList();
    }
}
