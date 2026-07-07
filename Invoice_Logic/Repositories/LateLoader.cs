namespace Invoice_Logic.Repositories;

public abstract class LateLoader
{
    public abstract Task Load();
}

public class LateLoader<T> : LateLoader
{
    private Func<Task<T>> _func;

    public LateLoader(Func<Task<T>> func)
    {
        _func = func;
    }

    public T? LoadObject { get; private set; }
    public override async Task Load()
    {
        LoadObject = await _func();
    }
}

public class LateLoader<TId, TObject> : LateLoader<TObject>
{
    public LateLoader(Func<Task<TObject>> func) : base(func)
    {
    }

    public LateLoader(Func<Task<TObject>> func, TId tempId) : base(func)
    {
        TempId = tempId;
    }

    public TId? TempId { get; private set; }
}
