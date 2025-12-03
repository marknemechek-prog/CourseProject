namespace ServerApp.Models;

public class Item
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Semester { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public List<UserAction> Actions { get; set; } = new();
}
