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