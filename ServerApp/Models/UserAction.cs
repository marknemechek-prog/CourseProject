namespace ServerApp.Models;

public class UserAction
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public string ActionType { get; set; } = null!; // 'Create' або 'Delete'

    public string? ActionDetails { get; set; }

    public string Status { get; set; } = "Pending"; // 'Pending', 'Approved', 'Rejected'

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
