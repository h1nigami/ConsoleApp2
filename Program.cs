using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var crm = new CrmService();
        var notifier = new Notifier();

        crm.ClientAdded += notifier.OnClientAdded;
        crm.OrderAdded += notifier.OnOrderAdded;
        
        Console.WriteLine("Добавляем клиента");
        var clientRepo = new ClientRepository("clients.json");
        var client = await crm.AddClient("Ivan", "email@mail.ru", "Perm", clientRepo);
        Console.WriteLine("Добавляем ему ордер");
        var orderRepository = new OrderRepository("orders.json");
        var order = await crm.AddOrder(client.Id,
        "Сделать сайт", 100.0m,
        DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
        orderRepository,
        clientRepo);

    }
}
