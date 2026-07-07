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
}
