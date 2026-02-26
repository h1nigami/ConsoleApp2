//Эт наш usb-port
public interface IOperationStrategy
{
    int Execute(int a, int b);
}

public class AddStrategy : IOperationStrategy
{
    public int Execute(int a, int b)
    {
        return a + b;
    }
}
public class SubstractStrategy : IOperationStrategy
{
    public int Execute(int a, int b)
    {
        return a - b;
    }
}
public class MultiplyStrategy : IOperationStrategy
{
    public int Execute(int a, int b)
    {
        return a * b;
    }
}
public class DividedStrategy : IOperationStrategy
{
    public int Execute(int a, int b)
    {
        return a/b;
    }
}

public class CalculatorContext
{
    private IOperationStrategy _strategy;

    public CalculatorContext(IOperationStrategy operationStrategy)
    {
        _strategy = operationStrategy;
    }
    public void SetStrategy(IOperationStrategy operationStrategy)
    {
        _strategy = operationStrategy;
    }
    public int ExecuteStrategy(int a, int b)
    {
        return _strategy.Execute(a, b);
    }
}

public abstract class Shape
{
    public abstract int GetArea();
}

public class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public override int GetArea() => Width * Height;
}

public class Square : Shape
{
    public int Side { get; set; }

    public override int GetArea() => Side * Side;
}

public class AreaCalculator
{
    public void TestRectangleArea(Shape rect)
    {
        int expectedArea = 50;
        int actualArea = rect.GetArea();

        if (actualArea == expectedArea)
        {
            Console.WriteLine("Тест пройден! Ожидаемая площадь: 50, Фактическая: " + actualArea);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Тест провален! Ожидаемая площадь: 50, Фактическая: " + actualArea);
            Console.ResetColor();
        }
    }
}

public abstract class GameAI
{
    public void Turn()
    {
        CollectResources();
        BuildStructures();
        BuildUnits();
        Attack();
    }

    protected virtual void CollectResources()
    {
        Console.WriteLine("Собираем золото и древесину");
    }
    protected abstract void BuildStructures();
    protected abstract void BuildUnits();

    protected virtual void Attack()
    {
        Console.WriteLine("Атакуем противника стандартными силами.");
    }
}

public class Orcs : GameAI
{
    protected override void BuildStructures()
    {
        Console.WriteLine("Строим бараки");
    }
    protected override void BuildUnits()
    {
        Console.WriteLine("Создаём пехотинцев");
    }
}

public class MonstersAI : GameAI
{
    protected override void CollectResources()
    {
        //Пусто. Монстры не занимаются экономикой
    }
    protected override void BuildStructures()
    {
        //Пусто. Монстры не умеют строить
    }
    protected override void BuildUnits()
    {
        Console.WriteLine("Порождаем монстров");
    }
    protected override void Attack()
    {
        Console.WriteLine("Атакуем всем роем");
    }
}