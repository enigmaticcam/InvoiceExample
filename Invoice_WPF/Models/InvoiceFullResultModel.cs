using Invoice_WPF.Services;
using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Models;

public class InvoiceFullResultModel : ICopy<InvoiceFullResultModel>
{
    public InvoiceFullResultModel(InvoiceFullResultDTO source)
    {
        InvoiceHeaderId = source.InvoiceHeaderId;
        InvoiceDetailId = source.InvoiceDetailId;
        CustItemCode = source.CustItemCode;
        CustItemDesc = source.CustItemDesc;
        ApprovedRate = source.ApprovedRate;
        Cases = source.Cases;
        OurItemCode = source.OurItemCode;
        CasesRemaining = source.CasesRemaining;
        HasFailedCase = source.HasFailedCase;
        HasFailedRate = source.HasFailedRate;
        ResultStatusTypeId = source.ResultStatusTypeId;
    }

    public InvoiceFullResultModel(int invoiceHeaderId, int invoiceDetailId, string custItemCode, string custItemDesc, decimal customerRate, decimal approvedRate, decimal cases, string ourItemCode, decimal? casesRemaining, bool? hasFailedCase, bool? hasFailedRate, int? resultStatusTypeId)
    {
        InvoiceHeaderId = invoiceHeaderId;
        InvoiceDetailId = invoiceDetailId;
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

    public int InvoiceHeaderId { get; set; }
    public int InvoiceDetailId { get; set; }
    public string CustItemCode { get; set; }
    public string CustItemDesc { get; set; }
    public decimal CustomerRate { get; set; }
    public decimal ApprovedRate { get; set; }
    public decimal Cases { get; set; }
    public string OurItemCode { get; set; }
    public decimal? CasesRemaining { get; set; }
    public bool? HasFailedCase { get; set; }
    public bool? HasFailedRate { get; set; }
    public int? ResultStatusTypeId { get; set; }
    public InvoiceFullResultModel Copy()
    {
        return new InvoiceFullResultModel(
            invoiceHeaderId: InvoiceHeaderId,
            invoiceDetailId: InvoiceDetailId,
            custItemCode: CustItemCode,
            custItemDesc: CustItemDesc,
            customerRate: CustomerRate,
            approvedRate: ApprovedRate,
            cases: Cases,
            ourItemCode: OurItemCode,
            casesRemaining: CasesRemaining,
            hasFailedCase: HasFailedCase,
            hasFailedRate: HasFailedRate,
            resultStatusTypeId: ResultStatusTypeId
        );
    }
}
