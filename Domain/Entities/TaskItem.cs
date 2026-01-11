using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class TaskItem : BaseEntity, IAuditableEntity
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public TaskType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
