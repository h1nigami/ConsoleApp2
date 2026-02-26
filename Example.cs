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

public class Rectangle
{
    // Неявный инвариант: Width и Height независимы.
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    // Постусловие метода GetArea: он вернет произведение Width и Height.
    public int GetArea()
    {
        return Width * Height;
    }
}

public class Square : Rectangle
{
    public override int Width
    {
        set { base.Width = base.Height = value; }
    }

    public override int Height
    {
        set { base.Width = base.Height = value; }
    }
}

public class AreaCalculator
{
    public void TestRectangleArea(Rectangle rect)
    {
        // Клиентский код ожидает, что установка ширины
        // не должна влиять на высоту (доверяет инварианту Rectangle).
        rect.Width = 10;
        rect.Height = 5;

        // Ожидаемый результат: 10 * 5 = 50
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