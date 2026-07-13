namespace Invoice_BlazorWASM.Services.Entities.ServerCommand;

public interface IServerCommand
{
    Task Execute();
}

public interface IServerCommand<T>
{
    Task<T> Execute();
}