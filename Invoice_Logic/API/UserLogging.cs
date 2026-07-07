using Invoice_Logic.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Invoice_Logic.API;

public interface IUserLogging
{
    string GetLogsConcat(string delimiter);
    [DoesNotReturn] void ThrowInvoiceDetailNotFoundException(IEnumerable<int> ids);
    [DoesNotReturn] void ThrowInvoiceHeaderNotFoundException(IEnumerable<int> ids);
}

public class UserLogging : IUserLogging
{
    private List<string> _logs = new();

    public string GetLogsConcat(string delimiter)
    {
        return string.Join(delimiter, _logs);
    }

    [DoesNotReturn]
    public void ThrowInvoiceDetailNotFoundException(IEnumerable<int> ids)
    {
        var message = $"The following Invoice Detail(s) were not found: {string.Join(",", ids)}";
        _logs.Add(message);
        throw new InvoiceDetailNotFoundException(message);
    }

    [DoesNotReturn]
    public void ThrowInvoiceHeaderNotFoundException(IEnumerable<int> ids)
    {
        var message = $"The following Invoice Headers(s) were not found: {string.Join(",", ids)}";
        _logs.Add(message);
        throw new InvoiceHeaderNotFoundException(message);
    }
}
