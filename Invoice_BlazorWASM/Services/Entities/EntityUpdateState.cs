namespace Invoice_BlazorWASM.Services.Entities;

public interface IEntityUpdateState<TId, TObject> : IClear
{
    IEnumerable<TObject> Updates { get; }
    void Add(TId id, TObject update);
    bool Contains(TId id);
    TObject Get(TId id);
}

public class EntityUpdateState<TUpdate, TId, TObject> : IEntityUpdateState<TId, TUpdate>, IDisposable where TId : notnull
{
    private IEntityState _state;
    private Dictionary<TId, TUpdate> _updates = new();

    public EntityUpdateState(IEntityState state)
    {
        _state = state;
        _state.OnChange += OnChange;
    }

    public void Dispose()
    {
        _state.OnChange -= OnChange;
    }

    public IEnumerable<TUpdate> Updates => _updates.Values;

    public void Add(TId id, TUpdate update)
    {
        _updates[id] = update;
    }

    public Task Clear()
    {
        _updates.Clear();
        return Task.CompletedTask;
    }

    private Task OnChange()
    {
        _updates.Clear();
        return Task.CompletedTask;
    }

    public bool Contains(TId id)
    {
        return _updates.ContainsKey(id);
    }

    public TUpdate Get(TId id)
    {
        return _updates[id];
    }
}

