// D - Dependicies Inversion Principles


public class Program
{
    public static async Task Main(string[] args)
    {
        ClientRepository clientRepository = new ClientRepository("clients.json");
        OrderRepository orderRepository = new OrderRepository("orders.json");
        var crm = new CrmService(clientRepository, orderRepository);
        ReportGeneratorFactory clientReportFactory = new ClientListReportFactory();
        ReportGeneratorFactory orderReportFactory = new OrderListReportFactory();
        BaseReporter clientReporter = clientReportFactory.CreateGenerator(crm, crm);
        BaseReporter orderReporter = orderReportFactory.CreateGenerator(crm, crm);
        clientReporter.Generate();
        orderReporter.Generate();
    }
}
