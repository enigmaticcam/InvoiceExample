namespace Invoice_Logic.Repositories;

public interface ILateLoaderCollection
{
    LateLoader<T> Add<T>(Func<Task<T>> func);
    LateLoader<TId, TObject> Add<TId, TObject>(Func<Task<TObject>> func, TId tempId);
    Task Commit();
}

public class LateLoaderCollection : ILateLoaderCollection
{
    private List<LateLoader> _loaders = new();
    public LateLoader<T> Add<T>(Func<Task<T>> func)
    {
        var added = new LateLoader<T>(func);
        _loaders.Add(added);
        return added;
    }

    public LateLoader<TId, TObject> Add<TId, TObject>(Func<Task<TObject>> func, TId tempId)
    {
        var added = new LateLoader<TId, TObject>(func, tempId);
        _loaders.Add(added);
        return added;
    }

    public async Task Commit()
    {
        foreach (var l in _loaders)
        {
            await l.Load();
        }
        _loaders.Clear();
    }
}
