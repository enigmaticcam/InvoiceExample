namespace Invoice_Logic.Exceptions;

public class StatusTypeNotFoundException : Exception
{
    public StatusTypeNotFoundException(string message) : base(message) { }
}
