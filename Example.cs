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
    public string Text {get; set;} = "";
    public void GetSelection() => Console.WriteLine("выделенный текст");
    public void DeleteSelectionText() => Console.WriteLine("Удаляю выделенный текст");
    public void ReplaceSelectionText(string text) => Console.WriteLine($"Вставляю: {text}");
}

