namespace Invoice_Logic.API;

public interface IPipeline
{
    Task<APIResult> Perform(Func<Task> action, string actionName);
    Task<APIResult<T>> Perform<T>(Func<Task<T>> action, string actionName);
}

public class Pipeline : IPipeline
{
    public async Task<APIResult> Perform(Func<Task> action, string actionName)
    {
        await action();
        return APIResult.Successful();
    }

    public async Task<APIResult<T>> Perform<T>(Func<Task<T>> action, string actionName)
    {
        var result = await action();
        return APIResult<T>.Successful(result);
    }
}

public class PipelineDecorator : IPipeline
{
    private IPipeline _pipeline;

    public PipelineDecorator(IPipeline pipeline)
    {
        _pipeline = pipeline;
    }

    public virtual Task<APIResult> Perform(Func<Task> action, string actionName)
    {
        return _pipeline.Perform(action, actionName);
    }

    public virtual Task<APIResult<T>> Perform<T>(Func<Task<T>> action, string actionName)
    {
        return _pipeline.Perform(action, actionName);
    }
}
