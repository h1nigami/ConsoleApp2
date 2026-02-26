public abstract class BaseReporter
{
    protected readonly CrmService _crmService;

    protected BaseReporter(CrmService crm)
    {
        _crmService = crm;
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
    public ClientListReport(CrmService crm) : base(crm){}

    protected override async Task GenerateBody()
    {
        Console.WriteLine("\n--- Список всех клиентов ---");
        var clients = await _crmService.GetAllClients();
        foreach(var client in clients)
        {
            Console.WriteLine($"ID: {client.Id}, Имя: {client.Name}, Email: {client.Email}");
        }
    }
}