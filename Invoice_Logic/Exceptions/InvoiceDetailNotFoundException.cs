namespace Invoice_Logic.Exceptions;

public class InvoiceDetailNotFoundException : Exception
{
    public InvoiceDetailNotFoundException(string message) : base(message) { }
}
