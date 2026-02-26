public static class EmailValidator
{
    private static int _validationCount = 0;
    
    public static bool IsValidEmail(string email)
    {
        _validationCount++;
        return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
    }

    public static int GetValidationsCount()
    {
        return _validationCount;
    }
}

public sealed class AppSettings
{
    private static readonly AppSettings _instance = new AppSettings();

    public string AppVersion {get; set;}
    public string  ThemeColor{get;set;}

    private AppSettings()
    {
        //здесь загружаются настройки из файла
        Console.WriteLine("Единственный экземпляр класса AppSettings создан");
        AppVersion = "0.0.1";
        ThemeColor = "Dark";
    }

    public static AppSettings Instance{
        get
        {
            return _instance;
        }
    }
}

//Потокобезопасная версия настроек
public sealed class AppSettingsSafe
{
    private static readonly Lazy<AppSettingsSafe> lazy = new Lazy<AppSettingsSafe>(()=>new AppSettingsSafe());

    public static AppSettingsSafe Instance => lazy.Value;
    public string AppVersion {get;set;}
    private AppSettingsSafe(){
        Console.WriteLine("Ленивая загрузка потокобезопасного экземпляра AppSettings");
        AppVersion = "0.0.2";
    }
}

public class CrmService
{
    public event Action<Client> ClientAdded;
    public event Action<Order, Client> OrderAdded;
    private static readonly CrmService _instance = new CrmService();
    public List<Client> FindClients(Predicate<Client> filter, ClientRepository clientRepository)
    {
        var result = new List<Client>();
        foreach(var client in clientRepository.GetAll())
        {
            if (filter(client))
            {
                result.Add(client);
            }
        }
        return result;
    }

    public static CrmService Instance
    {
        get
        {
            return _instance;
        }
    }

    public async Task<Client> AddClient(string name, string email, string city, ClientRepository _clientRepository)
    {
        var client = _clientRepository.Add(name, email, city, new List<Order>());
        await _clientRepository.SaveAsync();

        // 2. Генерируем (вызываем) событие, уведомляя всех подписчиков.
        // Передаем в качестве аргумента только что созданного клиента.
        ClientAdded?.Invoke(client);

        return client;
    }

    public async Task<Order> AddOrder(int clientId, string Description, decimal amount, DateOnly DueDate, OrderRepository orderRepository, ClientRepository clientRepository)
    {
        var order = orderRepository.Add(clientId, Description, amount, DueDate);
        var client = clientRepository.GetById(clientId);
        OrderAdded?.Invoke(order, client);

        return order;
    }

    public async Task<Order> PopOrder(int orderID, OrderRepository orderRepository)
    {
        var order = orderRepository.Pop(orderID);
        return order;
    }

    public async Task<List<Client>> FindClient(FindContext findStrategy, ClientRepository clientRepository)
    {
        return findStrategy.ExecuteFind(clientRepository);
    }
}

    // Класс-подписчик, который реагирует на события из CrmService.
public class Notifier
{
        // Метод-обработчик события добавления нового клиента.
    public void OnClientAdded(Client client)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Уведомление]: Добавлен новый клиент '{client.Name}' с Email: {client.Email}");
        Console.ResetColor();
    }

    public void OnOrderAdded(Order order, Client? client)
    {
        if (client != null){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[Уведомление]: Добавлен новый заказ '{order.Description}' стоимость: {order.amount}, Заказчик: {client.Name} из {client.city}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[Уведомление]: Добавлен новый заказ '{order.Description}' стоимость: {order.amount}, без заказчика");
            Console.ResetColor();
        }
        
    }
}