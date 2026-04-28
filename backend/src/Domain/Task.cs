namespace backend.Domain;

public class TaskItem
{
    public int Id { get; set; }
    public int LeadId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Lead? Lead { get; set; }
}

public enum TaskStatus { Todo = 0, Doing = 1, Done = 2 }