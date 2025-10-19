namespace XpBetSafe.Api.Models;

public enum RiskLevel { Low = 1, Medium = 2, High = 3 }

public class Alert
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public RiskLevel Risk { get; set; } = RiskLevel.Low;
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public User? User { get; set; }
}
