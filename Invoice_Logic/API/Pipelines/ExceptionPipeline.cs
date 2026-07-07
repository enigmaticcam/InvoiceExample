namespace Invoice_Logic.API.Pipelines;

public class ExceptionPipeline : PipelineDecorator
{
    public ExceptionPipeline(IPipeline pipeline) : base(pipeline)
    {
    }

    public override async Task<APIResult> Perform(Func<Task> action, string actionName)
    {
        try
        {
            var response = await base.Perform(action, actionName);
            return response;
        }
        catch
        {
            return APIResult.Failure("An error occurred");
        }
    }

    public override async Task<APIResult<T>> Perform<T>(Func<Task<T>> action, string actionName)
    {
        try
        {
            var response = await base.Perform(action, actionName);
            return response;
        }
        catch
        {
            return APIResult<T>.Failure("An error occurred");
        }
    }
}
