using System.Diagnostics.CodeAnalysis;

namespace Invoice_Logic.API;

public interface IUserLogging
{
    [DoesNotReturn] void ThrowInvoiceDetailNotFoundException(IEnumerable<int> ids);
    [DoesNotReturn] void ThrowInvoiceHeaderNotFoundException(IEnumerable<int> ids);
}
