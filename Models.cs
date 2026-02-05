public record Client(int Id, string Name, string Email, DateTime CreatedAt, string city, List<Order> Orders);
public record Order(int Id, int ClientId, string Description, decimal amount, DateOnly DueDate);

