namespace Invoice_Logic.Repositories;

public interface IClear
{
    void Clear();
}

public interface IItemCollection<T> : IClear
{
    int AddItem(T item);
    T GetItem(int id);
}

public abstract class ItemCollection<T> : IItemCollection<T>, IDisposable
{
    private IAllItemCollections _allItemCollections;
    private Dictionary<int, T> _items = new();
    private int _thisId;
    private int _autoIncrementId = 0;

    protected ItemCollection(IAllItemCollections allItemCollections)
    {
        _allItemCollections = allItemCollections;
    }

    public int AddItem(T item)
    {
        _autoIncrementId--;
        _items.Add(_autoIncrementId, item);
        return _autoIncrementId;
    }

    public void Clear()
    {
        _items.Clear();
    }

    public void Dispose()
    {
        _allItemCollections.DeregistrCollection(_thisId);
    }

    public T GetItem(int id)
    {
        return _items[id];
    }
}
