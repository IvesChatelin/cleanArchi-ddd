namespace Archi.SharedKernel.Models;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccuredOnUtc { get; set; }
    public DateTime? ProccesedOnUtc { get; set; }
    public int NumberOfRetry { get; set; }
    public string Error { get; set; } = string.Empty;
}