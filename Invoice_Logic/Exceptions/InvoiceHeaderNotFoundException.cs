namespace Invoice_Logic.Exceptions;

public class InvoiceHeaderNotFoundException : Exception
{
    public InvoiceHeaderNotFoundException(string message) : base(message) { }
}
