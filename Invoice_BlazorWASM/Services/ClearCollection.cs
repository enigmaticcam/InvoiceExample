namespace Invoice_BlazorWASM.Services;

public interface IClear
{
    Task Clear();
}

public interface IClearCollection
{
    void Register(IClear clear);
    void Deregister(IClear clear);
    Task ClearAll();
}

public class ClearCollection : IClearCollection
{
    private Dictionary<int, IClear> _collection = new();

    public async Task ClearAll()
    {
        var list = new List<Task>();
        foreach (var value in _collection.Values)
        {
            list.Add(value.Clear());
        }
        await Task.WhenAll(list);
    }

    public void Deregister(IClear clear)
    {
        _collection.Remove(clear.GetHashCode());
    }

    public void Register(IClear clear)
    {
        _collection[clear.GetHashCode()] = clear;
    }
}


