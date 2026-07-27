using Invoice_Logic.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Invoice_Logic.API;

public interface IUserLogging
{
    void AddLog(string message);
    string GetLogsConcat(string delimiter);
    [DoesNotReturn] void ThrowInvoiceDetailNotFoundException(IEnumerable<int> ids);
    [DoesNotReturn] void ThrowInvoiceHeaderInvalidActionException();
    [DoesNotReturn] void ThrowInvoiceHeaderNotFoundException(IEnumerable<int> ids);
    [DoesNotReturn] void ThrowInvoiceHeaderPermissionException(string message);
    [DoesNotReturn] void ThrowResultStatusTypeNotFoundException(IEnumerable<int> ids);
    [DoesNotReturn] void ThrowStatusTypeNotFoundException(int id);
    [DoesNotReturn] void ThrowStatusTypeNotFoundException(IEnumerable<int> ids);
}

public class UserLogging : IUserLogging
{
    private List<string> _logs = new();

    public void AddLog(string message)
    {
        _logs.Add(message);
    }

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
    public void ThrowInvoiceHeaderInvalidActionException()
    {
        var message = "This billback does not support the requested action";
        _logs.Add(message);
        throw new InvoiceHeaderInvalidActionException(message);
    }

    [DoesNotReturn]
    public void ThrowInvoiceHeaderNotFoundException(IEnumerable<int> ids)
    {
        var message = $"The following Invoice Headers(s) were not found: {string.Join(",", ids)}";
        _logs.Add(message);
        throw new InvoiceHeaderNotFoundException(message);
    }

    [DoesNotReturn]
    public void ThrowInvoiceHeaderPermissionException(string message)
    {
        _logs.Add(message);
        throw new InvoiceHeaderPermissionException(message);
    }

    [DoesNotReturn]
    public void ThrowResultStatusTypeNotFoundException(IEnumerable<int> ids)
    {
        var message = $"The following Result Status Type(s) were not found: {string.Join(",", ids)}";
        _logs.Add(message);
        throw new ResultStatusTypeNotFoundException(message);
    }

    [DoesNotReturn]
    public void ThrowStatusTypeNotFoundException(IEnumerable<int> ids)
    {
        var message = $"The following Status Type(s) were not found: {string.Join(",", ids)}";
        _logs.Add(message);
        throw new StatusTypeNotFoundException(message);
    }

    [DoesNotReturn]
    public void ThrowStatusTypeNotFoundException(int id)
    {
        ThrowStatusTypeNotFoundException(new List<int>() { id });
    }
}
