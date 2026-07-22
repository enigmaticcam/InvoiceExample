using Invoice_Logic.Core.Objects;
using Invoice_Logic.Core.Objects.InvoiceHeaderTypes;
using Invoice_Logic.Enums;

namespace Invoice_Logic.Factories;

public static class FactoryInvoiceHeaderType
{
    public static InvoiceHeaderType Create(enumStatusType statusType)
        => statusType switch
        {
            enumStatusType.Approved => new InvoiceHeaderTypeApproved(),
            enumStatusType.Draft => new InvoiceHeaderTypeDraft(),
            enumStatusType.Finished => new InvoiceHeaderTypeFinished(),
            _ => throw new NotImplementedException($"{statusType} not implemented")
        };
}
