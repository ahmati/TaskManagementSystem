using Domain.Enums;

namespace Application.Tasks.Dtos
{
    public class TaskQueryDto
    {
        public string? Search { get; set; }
        public Status Status { get; set; }
        public TaskType TaskType { get; set; }
        public Priority Priority { get; set; }
    }
}
