
/// <summary>
/// Как делать не надо
/// </summary>
//Низкоуровневый модуль
public class FileLoger
{
    public void Log(string message)
    {
        File.WriteAllText("log.txt", message);
    }
}

//Выосокоуровневый модуль
public class OrderPreProcessor
{
    //Жесткая зависимость от конкретной реализации
    //Внедрение зависимости через свойство
    private readonly FileLoger _logger = new FileLoger();

    public void Process()
    {
        /// ...Какая то бизнес логика
        _logger.Log("Заказ обработан");
    }
}

//абстрация
public interface ILogger
{
    void Log(string message);
}

//низкоуровневый модуль зависимый от абстракции
public class FileLogger : ILogger
{
    public void Log(string message){/*........*/}
}

//высокоуровневый модуль зависимый от абстракции
public class OrderProcessor
{
    private readonly ILogger _logger;
    public OrderProcessor(ILogger logger) //зависимость приходит извне
    {
        _logger = logger;
    }

    public void Process()
    {
        /// ...Какая то бизнес логика
        _logger.Log("Заказ обработан");
    }
}