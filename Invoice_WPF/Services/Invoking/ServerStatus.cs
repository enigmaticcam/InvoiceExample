using Invoice_WPF.Services.Core;

namespace Invoice_WPF.Services.Invoking;

public interface IServerStatus
{
    Task StartTask(InvokerToken token);
    Task EndTask(InvokerToken token, WPFResult result);
}

public class ServerStatus : IServerStatus
{
    private SemaphoreSlim _semaphore = new(1, 1);
    private int _taskCount;
    private List<WPFResult> _results = new();

    public async Task StartTask(InvokerToken token)
    {
        bool canBroadcast = false;
        await _semaphore.WaitAsync();
        try
        {
            if (_taskCount == 0)
            {
                _results.Clear();
                canBroadcast = true;
            }
            _taskCount++;
        }
        finally
        {
            _semaphore.Release();
        }
        if (canBroadcast && token.OnRunning != null)
        {
            await token.OnRunning(true);
        }
    }

    public async Task EndTask(InvokerToken token, WPFResult result)
    {
        bool canBroadcast = false;
        await _semaphore.WaitAsync();
        try
        {
            _taskCount--;
            _results.Add(result);
            if (_taskCount == 0)
            {
                canBroadcast = true;
            }
        }
        finally
        {
            _semaphore.Release();
        }
        if (canBroadcast)
        {
            if (token.OnComplete != null)
            {
                await token.OnComplete(_results);
            }
            if (token.OnRunning != null)
            {
                await token.OnRunning(false);
            }
        }
    }
}
