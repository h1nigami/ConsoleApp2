

public class Program
{
    public static async Task Main(string[] args)
    {
        ClientRepository clientRepository = new ClientRepository("clients.json");
        OrderRepository orderRepository = new OrderRepository("orders.json");
        CrmService crm = new CrmService(clientRepository, orderRepository); 
        BaseReporter ClientReporter = new ClientListReport(crm);
        ClientReporter.Generate();
    }
}
