using System.ComponentModel.DataAnnotations;

namespace HomeServer.Infrastructure
{
    public enum PriorityLevel
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3

    }

    public class TodoEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        public bool IsRecurring { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public PriorityLevel Priority { get; set; } = PriorityLevel.Low;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }

        public List<string> Notes { get; set; } = new();
        public Dictionary<string, bool> SubTasks { get; set; } = new();
    }
}
