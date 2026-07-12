using Invoice_BlazorWASM.Services.Core;

namespace Invoice_BlazorWASM.Services;

public interface IServerStatus
{
    Task StartTask(BroadcastToken token);
    Task EndTask(BroadcastToken token, BlazorResult result);
}

public class ServerStatus : IServerStatus
{
    private static object _lock = new object();
    private int _taskCount;
    private List<BlazorResult> _results = new List<BlazorResult>();

    public async Task StartTask(BroadcastToken token)
    {
        bool canBroadCast = false;
        lock (_lock)
        {
            if (_taskCount == 0)
            {
                _results.Clear();
                canBroadCast = true;
            }
            _taskCount++;
        }
        if (canBroadCast)
        {
            if (token.OnRunning != null) await token.OnRunning(true);
        }
    }

    public async Task EndTask(BroadcastToken token, BlazorResult result)
    {
        bool canBraodCast = false;
        lock (_lock)
        {
            _taskCount--;
            _results.Add(result);
            if (_taskCount == 0) canBraodCast = true;
        }
        if (canBraodCast)
        {
            if (token.OnRunning != null) await token.OnRunning(false);
            if (token.OnComplete != null) await token.OnComplete(_results);
        }
    }
}

public class BroadcastToken
{
    public Func<bool, Task>? OnRunning { get; set; }
    public Func<List<BlazorResult>, Task>? OnComplete { get; set; }
}

