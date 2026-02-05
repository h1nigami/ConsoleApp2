using Microsoft.VisualBasic;
using Newtonsoft.Json;

public class BaseRepository<T>
{
    protected List<T> _items;
    protected readonly string _filePath;
    protected int _nextId = 1;

    protected BaseRepository(string filePath)
    {
        _filePath = filePath;
        _items = new List<T>();
        Load();
    }
    private void Load()
    {
        if(!File.Exists(_filePath)) return;
        string json = File.ReadAllText(_filePath);
        _items = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
    }

    public virtual async Task SaveAsync()
    {
        string json = JsonConvert.SerializeObject(_items, Newtonsoft.Json.Formatting.Indented);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public List<T> GetAll() => _items;
}

public class ClientRepository : BaseRepository<Client>
{
    public ClientRepository(string filePath) : base(filePath)
    {
        if (_items.Any())
        {
            _nextId = _items.Cast<Client>().Max(c => c.Id) + 1;
        }
    }

    public Client Add(string name, string email, string city, List<Order> orders)
    {
        if (!EmailValidator.IsValidEmail(email))
        {
            throw new Exception("Невалидная почта");
        }
        var Client = new Client(_nextId++, name, email, DateTime.Now, city, orders);
        _items.Add(Client);
        return Client;
    }

    public Client? GetById(int id)
    {
        return _items.Cast<Client>().FirstOrDefault(c => c.Id == id);
    }

    
    
}

public class OrderRepository : BaseRepository<Order>
{
    public OrderRepository(string filePath) : base(filePath)
    {
        if (_items.Any())
        {
            _nextId = _items.Cast<Order>().Max(o => o.Id) + 1;
        }
    }

    public Order Add(int ClientId, string Description, decimal amount, DateOnly DueDate)
    {
        
        var order = new Order(_nextId++, ClientId, Description, amount, DueDate);
        _items.Add(order);
        return order;
    }

    public Order? GetById(int id)
    {
        return _items.Cast<Order>().FirstOrDefault(o => o.Id == id);
    }
}

