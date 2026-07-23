namespace Invoice_BlazorWASM.Services.ServerCommand;

public interface IServerCommand
{
    Task Execute();
}

public interface IServerCommand<T>
{
    Task<T> Execute();
}