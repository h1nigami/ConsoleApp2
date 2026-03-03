
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
    public void Log(string message){File.WriteAllText("log.txt", message);}
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
        Console.WriteLine("Процесс обработан");
        _logger.Log("Заказ обработан");
    }
}

//Паттерн Фабричный метод

//интерфейс продукта
public interface IButton
{
    void Render();
    void OnClick();
}

//конкретный продукт1
public class WindowsButton : IButton
{
    public void Render() => Console.WriteLine("Отрисовка в стиле windows");
    public void OnClick() => Console.WriteLine("Вызвано событие клика по кнопке windows");
}

//конкретный продукт2
public class HtmlButton : IButton
{
    public void Render() => Console.WriteLine("<button> Текстовая кнопка </button>");
    public void OnClick() => Console.WriteLine("вызвано событие onclick браузера");
}

public abstract class Dialog
{
    public abstract IButton CreateButton();

    //Высокоуровневая бизнес логика работающая с интерфейсом продукта 
    public void Render()
    {
        Console.WriteLine("начинаем отрисовку диалогового окна...");
        IButton okButton = CreateButton();
        okButton.Render();
    }
}

public class WindowsDialog : Dialog
{
    public override IButton CreateButton()
    {
        return new WindowsButton();
    }
}

public class HtmlDialog : Dialog
{
    public override IButton CreateButton()
    {
        return new HtmlButton();
    }
}

public class Application
{
    private Dialog _dialog;
    public void INitialize(string os)
    {
        if(os == "Windows")
        {
            _dialog = new WindowsDialog();
        }
        else if(os == "Web")
        {
            _dialog = new HtmlDialog();
        }
        else
        {
            throw new Exception("Неизвестная операционная система");
        }
    }
}