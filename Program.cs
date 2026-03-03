// D - Dependicies Inversion Principles


public class Program
{
    public static async Task Main(string[] args)
    {
        ILogger logger = new FileLogger();
        var processor = new OrderProcessor(logger);

        processor.Process();
    }
}
