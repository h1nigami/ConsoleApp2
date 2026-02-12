
public class Program
{
    public static async Task Main(string[] args)
    {
        CrmService crm = new CrmService();
        Notifier notifier = new Notifier();

        crm.ClientAdded += notifier.OnClientAdded;
        crm.OrderAdded += notifier.OnOrderAdded;
        
        Console.WriteLine("Добавляем клиента");
        ClientRepository clientRepo = new ClientRepository("clients.json");
        var client = await crm.AddClient("Ivan", "email@mail.ru", "Perm", clientRepo);
        Console.WriteLine("Добавляем ему ордер");
        OrderRepository orderRepository = new OrderRepository("orders.json");
        var order = await crm.AddOrder(client.Id,
        "Сделать сайт", 100.0m,
        DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
        orderRepository,
        clientRepo);
        client.Orders.Add(order);
        await clientRepo.SaveAsync();
        await orderRepository.SaveAsync();
        await crm.PopOrder(2, orderRepository);
        await orderRepository.SaveAsync();
    }
}
