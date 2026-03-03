public abstract class BaseReporter
{
    protected readonly IClientReader _clientReader;
    protected readonly IOrderReader _orderReader;
    protected BaseReporter(IClientReader clientReader, IOrderReader orderReader)
    {
        _clientReader = clientReader;
        _orderReader = orderReader;
    }
    public void Generate()
    {
        GenerateHeader();
        GenerateBody();
        GenerateFooter();
    }
    protected virtual void GenerateHeader()
    {
        Console.WriteLine("======================================");
        Console.WriteLine("         ОТЧЕТ ПО СИСТЕМЕ CRM         ");
        Console.WriteLine("======================================");
    }
    protected virtual void GenerateFooter()
    {
        Console.WriteLine("___________________________________");
        Console.WriteLine($"Отчет сгенерирован: {DateTime.Now}");
        Console.WriteLine("===================================");
    }
    protected abstract Task GenerateBody();
}

public class ClientListReport : BaseReporter
{
    public ClientListReport(IClientReader clientReader, IOrderReader orderReader) : base(clientReader, orderReader){}

    protected override async Task GenerateBody()
    {
        Console.WriteLine("\n--- Список всех клиентов ---");
        var clients = await _clientReader.GetAllClients();
        foreach(var client in clients)
        {
            Console.WriteLine($"ID: {client.Id}, Имя: {client.Name}, Email: {client.Email}");
        }
    }
}

public class OrderListReport : BaseReporter
{
    public OrderListReport(IClientReader clientReader, IOrderReader orderReader) : base(clientReader, orderReader){}

    protected override async Task GenerateBody()
    {
        Console.WriteLine("\n--- Список всех ордеров ---");
        var orders = await _orderReader.GetAllOrders();
        foreach(var order in orders)
        {
            Console.WriteLine($"ID: {order.Id}, Описание: {order.Description}, Стоимость: {order.amount}, Дедлайн: {order.DueDate}");
        }
        Console.WriteLine("\n--- Список выполненных ордеров ---");
        foreach(var order in orders.Where(o => o.Succes).ToList())
        {
            Console.WriteLine($"ID: {order.Id}, Описание: {order.Description}, Стоимость: {order.amount}");
        }
    }
}

public abstract class ReportGeneratorFactory
{
    public abstract BaseReporter CreateGenerator(IClientReader clientReader, IOrderReader orderReader);
}

public class ClientListReportFactory : ReportGeneratorFactory
{
    public override BaseReporter CreateGenerator(IClientReader clientReader, IOrderReader orderReader)
    {
        return new ClientListReport(clientReader, orderReader);
    }
}

public class OrderListReportFactory : ReportGeneratorFactory
{
    public override BaseReporter CreateGenerator(IClientReader clientReader, IOrderReader orderReader)
    {
        return new OrderListReport(clientReader, orderReader);
    }
}