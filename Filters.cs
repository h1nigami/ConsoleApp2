public interface IFind
{
    List<Client> Find(ClientRepository clientRepository);
}

public class FindByCity : IFind
{
    private readonly string _city;
    public FindByCity(string city)
    {
        _city = city;
    }
    public List<Client> Find(ClientRepository clientRepository)
    {
        var clients = clientRepository.GetAll().Where(c=>c.city == _city);
        return clients.ToList();
    }
}

public class FindByName : IFind
{
    private readonly string _name;
    public FindByName(string name)
    {
        _name = name;
    }
    public List<Client> Find(ClientRepository clientRepository)
    {
        return clientRepository.GetAll().Where(c => c.Name == _name).ToList();
    }
}

public class FindContext
{
    private IFind _strategy;
    public FindContext(IFind strategy)
    {
        _strategy = strategy;
    }
    public void SetStrategy(IFind strategy)
    {
        _strategy = strategy;
    }
    public List<Client> ExecuteFind(ClientRepository clientRepository)
    {
        var result = _strategy.Find(clientRepository);
        return result;
    }
}