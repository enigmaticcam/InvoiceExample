namespace Invoice_WPF.Services.Entities;

public interface IEntityState
{
    Func<Task>? OnChanged { get; set; }
}

public interface IEntityState<TId, TObject> : IEntityState
{
    IEnumerable<TObject> Items { get; }
    bool IsLoaded { get; }
    bool Contains(TId id);
    TObject Get(TId id);
    Task Merge(TObject item);
    Task Merge(IEnumerable<TObject> items);
    Task Remove(IEnumerable<TObject> items);
    Task Remove(TId item);
    Task Remove(IEnumerable<TId> items);
    void Reset();
    Task Set(TObject item);
    Task Set(IEnumerable<TObject> items);
}

public abstract class EntityState<TId, TObject> : IEntityState<TId, TObject> where TId : notnull where TObject : ICopy<TObject>
{
    private Dictionary<TId, TObject> _items = new();
    private Dictionary<TId, TObject> _oldValues = new();

    public IEnumerable<TObject> Items => _items.Values;
    public bool IsLoaded { get; private set; }
    public Func<Task>? OnChanged { get; set; }

    public bool Contains(TId id)
    {
        return _items.ContainsKey(id);
    }

    public TObject Get(TId id)
    {
        if (!_items.ContainsKey(id))
        {
            throw new Exception($"Entity set does not contain {id}");
        }
        return _items[id];
    }

    public async Task Merge(TObject item)
    {
        await Merge(new List<TObject>() { item });
    }

    public async Task Merge(IEnumerable<TObject> items)
    {
        foreach (var item in items)
        {
            var id = GetId(item);
            _items[id] = item;
            _oldValues[id] = (TObject)item.Copy();
        }
        if (OnChanged != null)
        {
            await OnChanged();
        }
    }

    public Task Remove(TId item)
    {
        return Remove(new List<TId>() { item });
    }

    public async Task Remove(IEnumerable<TId> ids)
    {
        foreach (var id in ids)
        {
            if (_items.ContainsKey(id))
            {
                _items.Remove(id);
            }
            if (_oldValues.ContainsKey(id))
            {
                _oldValues.Remove(id);
            }
        }
        if (OnChanged != null)
        {
            await OnChanged();
        }
    }

    public async Task Remove(IEnumerable<TObject> items)
    {
        foreach (var item in items)
        {
            var id = GetId(item);
            if (_items.ContainsKey(id))
            {
                _items.Remove(id);
            }
            if (_oldValues.ContainsKey(id))
            {
                _items.Remove(id);
            }
        }
        if (OnChanged != null)
        {
            await OnChanged();
        }
    }

    public void Reset()
    {
        foreach (var item in _oldValues.Values)
        {
            var id = GetId(item);
            if (_items.ContainsKey(id))
            {
                _items[id] = (TObject)_oldValues[id].Copy();
            }
        }
    }

    public Task Set(TObject item)
    {
        return Set(new List<TObject>() { item });
    }

    public async Task Set(IEnumerable<TObject> items)
    {
        _items.Clear();
        foreach (var i in items)
        {
            var id = GetId(i);
            _items.Add(id, i);
        }
        IsLoaded = true;
        if (OnChanged != null)
        {
            await OnChanged();
        }
    }

    protected abstract TId GetId(TObject obj);
}
