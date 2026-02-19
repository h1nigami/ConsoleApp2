public class DataFetcher
{
    private DateTime startDate;
    private DateTime endDate;
    public DataFetcher(DateTime startdate, DateTime enddate)
    {
        startDate = startdate;
        endDate = enddate;
    }
    public List<Order> FetchOrders(OrderRepository orderRepository)
    {
        var orders = orderRepository.GetAll().Where(
        o => o.Succes == true 
        && o.DueDate >= DateOnly.FromDateTime(startDate) 
        && o.DueDate <= DateOnly.FromDateTime(endDate)).ToList();
        return orders;
    }
}

public class ReportFormater
{
    public string Format(List<Order> orders, ClientRepository clientRepository)
    {
        var reportContent = $"<html><body></body>\n</html>";
        decimal totalCost = 0;
        var added = reportContent.Split("<body>");
        added[0]+="\n<body>";
        foreach(var order in orders)
        {
            totalCost += order.amount;
            added[0] += $"\nЗаказ под номером {order.Id} стоимостью {order.amount}: {order.Description} от {clientRepository.GetById(order.Id).Name}";
        }
        added[0]+="\n";
        return added[0] + added[1];
    }
}

public class SalesReport
{
    private readonly DataFetcher _fetcher;
    private readonly ReportFormater _formater;

    public SalesReport(DataFetcher fetcher, ReportFormater formater)
    {
        _fetcher = fetcher;
        _formater = formater;
    }

    public void GenerateReport(OrderRepository orderRepository, ClientRepository clientRepository)
    {
        //1. Получение заказов из файла
        Console.WriteLine("Подключаюсь к файлу");
        var data = _fetcher.FetchOrders(orderRepository);
        Console.WriteLine("Данные получены");
        
        //2. Форматирвоание данных
        Console.WriteLine("Форматирую отчет");
        var formated_report = _formater.Format(data, clientRepository);
        
        //3. Сохранение отчета
        Console.WriteLine("Сохраняю отчет");
        File.WriteAllText("report.html", formated_report);
        Console.WriteLine("Отчет сохранен");
    }
}