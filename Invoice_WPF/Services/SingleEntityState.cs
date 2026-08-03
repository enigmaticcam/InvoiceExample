namespace Invoice_BlazorWASM.Services.Entities;

public interface ISingleEntityState<T>
{
    T? Item { get; }
    Task Set(T item);
    Func<Task>? OnChanged { get; set; }
}

public abstract class SingleEntityState<T> : ISingleEntityState<T>
{
    public T? Item { get; private set; }

    public Func<Task>? OnChanged { get; set; }

    public async Task Set(T item)
    {
        Item = item;
        if (OnChanged != null)
        {
            await OnChanged();
        }
    }
}
