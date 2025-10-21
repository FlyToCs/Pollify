namespace Pollify.Domain.Common;

public class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public void UpdateTimestamp() => UpdatedAt = DateTime.Now;


}