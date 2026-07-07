namespace Invoice_Logic.API.Pipelines;

public class UserLoggingPipeline : PipelineDecorator
{
    private IUserLogging _userLogging;
    public UserLoggingPipeline(IPipeline pipeline, IUserLogging userLogging) : base(pipeline)
    {
        _userLogging = userLogging;
    }

    public override async Task<APIResult> Perform(Func<Task> action, string actionName)
    {
        var result = await base.Perform(action, actionName);
        result.Message = GetMessage(result.Message);
        return result;
    }

    public override async Task<APIResult<T>> Perform<T>(Func<Task<T>> action, string actionName)
    {
        var result = await base.Perform(action, actionName);
        result.Message = GetMessage(result.Message);
        return result;
    }

    private string GetMessage(string message)
    {
        var logs = _userLogging.GetLogsConcat("\r\n");
        if (!string.IsNullOrWhiteSpace(logs))
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return logs;
            }
            else
            {
                return message += $"\r\n{logs}";
            }
        }
        return message;
    }
}
