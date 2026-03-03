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
public class Editor
{
    public string Text {get; set;} = "";
    public void GetSelection() => Console.WriteLine("выделенный текст");
    public void DeleteSelectionText() => Console.WriteLine("Удаляю выделенный текст");
    public void ReplaceSelectionText(string text) => Console.WriteLine($"Вставляю: {text}");
}

public abstract class Command
{
    protected Editor _editor;
    private string _backup;

    protected Command(Editor editor)
    {
        _editor = editor;
    }

    protected void SaveBAckup()
    {
        _backup = _editor.Text;
    }

    public void Undo()
    {
        _editor.Text = _backup;
        Console.WriteLine("Отмена операции");
    }

    public abstract bool Execute();
}

public class CutCommand : Command
{
    public CutCommand(Editor editor):base(editor){}

    public override bool Execute()
    {
        SaveBAckup();
        _editor.DeleteSelectionText();
        return true;
    }
}
