

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("---Ход орков---");
        GameAI orcs = new Orcs();
        orcs.Turn();

        Console.WriteLine("---Ход монстров---");
        GameAI monsters = new MonstersAI();
        monsters.Turn();
    }
}
