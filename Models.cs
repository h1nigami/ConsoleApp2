public record Client(int Id, string Name, string Email, DateTime CreatedAt, string city, List<Order> Orders);
public record Order(int Id, int ClientId, string Description, decimal amount, DateOnly DueDate, bool Succes)
{
    private bool _succes = Succes;
    public bool Succes
    {
        get => _succes;
        set
        {
            _succes = value;
        }
    }
}

