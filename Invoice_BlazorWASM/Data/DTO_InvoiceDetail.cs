using Invoice_BlazorWASM.Services.Core;
using Invoice_BlazorWASM.Services.Entities;

namespace Invoice_BlazorWASM.Data;

public class DTO_InvoiceDetail : IEntity<int>
{
    public DTO_InvoiceDetail(InvoiceFullResultDTO source)
    {
        InvoiceDetailId = source.InvoiceDetailId;
        InvoiceHeaderId = source.InvoiceHeaderId;
        CustItemCode = source.CustItemCode;
        CustItemDesc = source.CustItemDesc;
        CustomerRate = source.CustomerRate;
        ApprovedRate = source.ApprovedRate;
        Cases = source.Cases;
        OurItemCode = source.OurItemCode;
        CasesRemaining = source.CasesRemaining;
        HasFailedCase = source.HasFailedCase;
        HasFailedRate = source.HasFailedRate;
        ResultStatusTypeId = source.ResultStatusTypeId;
    }

    public DTO_InvoiceDetail(int invoiceDetailId, int invoiceHeaderId, string custItemCode, string custItemDesc, decimal customerRate, decimal approvedRate, decimal cases, string? ourItemCode, decimal? casesRemaining, bool? hasFailedCase, bool? hasFailedRate, int? resultStatusTypeId)
    {
        InvoiceDetailId = invoiceDetailId;
        InvoiceHeaderId = invoiceHeaderId;
        CustItemCode = custItemCode;
        CustItemDesc = custItemDesc;
        CustomerRate = customerRate;
        ApprovedRate = approvedRate;
        Cases = cases;
        OurItemCode = ourItemCode;
        CasesRemaining = casesRemaining;
        HasFailedCase = hasFailedCase;
        HasFailedRate = hasFailedRate;
        ResultStatusTypeId = resultStatusTypeId;
    }

    public int InvoiceDetailId { get; set; }
    public int InvoiceHeaderId { get; set; }
    public string CustItemCode { get; set; }
    public string CustItemDesc { get; set; }
    public decimal CustomerRate { get; set; }
    public decimal ApprovedRate { get; set; }
    public decimal Cases { get; set; }
    public string? OurItemCode { get; set; }
    public decimal? CasesRemaining { get; set; }
    public bool? HasFailedCase { get; set; }
    public bool? HasFailedRate { get; set; }
    public int? ResultStatusTypeId { get; set; }
    public int Id => InvoiceDetailId;

    public IEntity<int> Copy()
    {
        return new DTO_InvoiceDetail(
            invoiceDetailId: InvoiceDetailId,
            invoiceHeaderId: InvoiceHeaderId,
            custItemCode: CustItemCode,
            custItemDesc: CustItemDesc,
            customerRate: CustomerRate,
            approvedRate: ApprovedRate,
            cases: Cases,
            ourItemCode: OurItemCode,
            casesRemaining: CasesRemaining,
            hasFailedCase: HasFailedCase,
            hasFailedRate: HasFailedRate,
            resultStatusTypeId: ResultStatusTypeId);
    }
}
