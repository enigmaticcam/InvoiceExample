namespace Invoice_Logic.Exceptions;

public class InvoiceHeaderPermissionException : Exception
{
    public InvoiceHeaderPermissionException(string message) : base(message) { }
}
