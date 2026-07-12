namespace Invoice_Logic.API.Pipelines;

public class ExceptionPipeline : PipelineDecorator
{
    private IExceptionHandler _exceptionHandler;
    public ExceptionPipeline(IPipeline pipeline, IExceptionHandler exceptionHandler) : base(pipeline)
    {
        _exceptionHandler = exceptionHandler;
    }

    public override async Task<APIResult> Perform(Func<Task> action, string actionName)
    {
        try
        {
            var response = await base.Perform(action, actionName);
            return response;
        }
        catch (Exception ex)
        {
            await _exceptionHandler.LogException(ex);
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
        catch (Exception ex)
        {
            await _exceptionHandler.LogException(ex);
            return APIResult<T>.Failure("An error occurred");
        }
    }
}
