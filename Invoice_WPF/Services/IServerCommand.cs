namespace Invoice_WPF.Services;

public interface IServerCommand
{
    Task Execute();
}

public interface IServerCommand<T>
{
    Task<T> Execute();
}
