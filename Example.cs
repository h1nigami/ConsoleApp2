// "Толстый" интерфейс, который пытается описать все возможные действия
public interface IMultiFunctionDevice
{
    void Print(string content);
    void Scan(string content);
    void Fax(string content);
}

public interface IPrint
{
    void Pring(string content);
}
public interface IScan
{
    void Scan(string content);
}

public interface IFax
{
    void Fax(string content);
}

//Паттерн "Команда"
class Editor
{
    public string Text = "выделенный текст";
    public void DeleteText() => Console.WriteLine("Удаляю выделенный текст");
}