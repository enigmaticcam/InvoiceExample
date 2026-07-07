namespace Invoice_Logic.Repositories;

public interface IAllItemCollections
{
    int RegisterCollection(IClear clear);
    void DeregistrCollection(int id);
    void ClearAll();
}

public class AllItemCollections : IAllItemCollections
{
    private Dictionary<int, IClear> _dictionary = new();
    private int _id = 0;

    public void ClearAll()
    {
        foreach (var c in _dictionary.Values)
        {
            c.Clear();
        }
    }

    public void DeregistrCollection(int id)
    {
        if (_dictionary.ContainsKey(id))
        {
            _dictionary.Remove(id);
        }
    }

    public int RegisterCollection(IClear clear)
    {
        _id++;
        _dictionary.Add(_id, clear);
        return _id;
    }
}
