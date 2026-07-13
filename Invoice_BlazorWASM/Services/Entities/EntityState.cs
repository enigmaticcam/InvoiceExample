namespace Invoice_BlazorWASM.Services.Entities;

public interface IEntityState
{
    Func<Task>? OnChange { get; set; }
}

public interface IEntityState<TId, TObject> : IEntityState, IClear
{
    IEnumerable<TObject> Items { get; }
    bool IsLoaded { get; }
    Task Append(TObject item);
    bool Contains(TId id);
    TObject Get(TId id);
    Task Merge(TObject item);
    Task Merge(IEnumerable<TObject> items);
    Task Remove(IEnumerable<TObject> items);
    Task Remove(TId item);
    Task Remove(IEnumerable<TId> items);
    Task Set(TObject item);
    Task Set(IEnumerable<TObject> items);
    void Reset();
}

public abstract class EntityState<TId, TObject> : IEntityState<TId, TObject>, IDisposable where TId : notnull where TObject : IEntity<TId>
{
    private Dictionary<TId, TObject> _items = new();
    private Dictionary<TId, TObject> _oldValues = new();
    private IClearCollection _clearCollection;

    protected EntityState(IClearCollection clearCollection)
    {
        _clearCollection = clearCollection;
        clearCollection.Register(this);
    }

    public IEnumerable<TObject> Items => Alter(_items.Values);
    public bool IsLoaded { get; private set; }
    public Func<Task>? OnChange { get; set; }
    public abstract string EntityName { get; }

    public virtual IEnumerable<TObject> Alter(IEnumerable<TObject> items)
    {
        return items;
    }

    public async Task Append(TObject item)
    {
        Reset();
        _items.Add(item.Id, item);
        _oldValues.Add(item.Id, (TObject)item.Copy());
        if (OnChange != null)
            await OnChange();
    }

    public async Task Clear()
    {
        _items.Clear();
        _oldValues.Clear();
        IsLoaded = false;
        if (OnChange != null)
            await OnChange();
    }

    public bool Contains(TId id)
    {
        return _items.ContainsKey(id);
    }

    public TObject Get(TId id)
    {
        if (!_items.ContainsKey(id))
        {
            throw new Exception($"Entity set {EntityName} does not contain item {id}");
        }
        return _items[id];
    }

    public async Task Merge(TObject item)
    {
        await Merge(new List<TObject>() { item });
    }

    public async Task Merge(IEnumerable<TObject> items)
    {
        Reset();
        foreach (var item in items)
        {
            _items[item.Id] = item;
            _oldValues[item.Id] = (TObject)item.Copy();
        }
        if (OnChange != null)
        {
            await OnChange();
        }
    }

    public async Task Remove(IEnumerable<TObject> items)
    {
        Reset();
        foreach (var item in items)
        {
            if (_items.ContainsKey(item.Id))
            {
                _items.Remove(item.Id);
            }
            if (_oldValues.ContainsKey(item.Id))
            {
                _oldValues.Remove(item.Id);
            }
        }
        if (OnChange != null)
        {
            await OnChange();
        }
    }

    public Task Remove(TId item)
    {
        return Remove(new List<TId>() { item });
    }

    public async Task Remove(IEnumerable<TId> items)
    {
        Reset();
        foreach (var item in items)
        {
            if (_items.ContainsKey(item))
            {
                _items.Remove(item);
            }
            if (_oldValues.ContainsKey(item))
            {
                _oldValues.Remove(item);
            }
        }
        if (OnChange != null)
            await OnChange();
    }

    public void Reset()
    {
        foreach (var item in _items.Keys.ToList())
        {
            if (_oldValues.ContainsKey(item))
            {
                _items[item] = (TObject)_oldValues[item].Copy();
            }
        }
    }

    public Task Set(TObject item)
    {
        return Set(new List<TObject>() { item });
    }

    public async Task Set(IEnumerable<TObject> items)
    {
        _items = items.ToDictionary(x => x.Id, x => x);
        _oldValues = items
            .Select(x => (TObject)x.Copy())
            .ToDictionary(x => x.Id, x => x);
        IsLoaded = true;
        if (OnChange != null)
            await OnChange();
    }

    public void Dispose()
    {
        _clearCollection.Deregister(this);
    }
}
