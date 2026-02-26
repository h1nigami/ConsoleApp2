

public class Program
{
    public static async Task Main(string[] args)
    {
        var calc = new AreaCalculator();
        var rec =  new Rectangle();
        rec.Height=10;
        rec.Width=5;
        var square = new Square();
        square.Side = 5;
        calc.TestRectangleArea(rec);
        calc.TestRectangleArea(square);
        
    }
}
