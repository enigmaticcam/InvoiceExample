namespace Invoice_Logic.Exceptions;

public class InvoiceHeaderInvalidActionException : Exception
{

    public InvoiceHeaderInvalidActionException(string message) : base(message)
    {
    }
}
