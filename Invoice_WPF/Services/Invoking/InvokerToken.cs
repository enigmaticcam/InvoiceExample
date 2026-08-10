using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Services.Invoking;

public class InvokerToken
{
    public Func<List<WPFResult>, Task>? OnComplete { get; set; }
    public Func<bool, Task>? OnRunning { get; set; }
}
