namespace Invoice_Logic.Exceptions;

public class InvoiceDetailNotInHeaderException : Exception
{
    public InvoiceDetailNotInHeaderException(string message) : base(message) { }
}
