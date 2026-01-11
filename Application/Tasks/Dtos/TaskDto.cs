
using Domain.Enums;

namespace Application.Tasks.Dtos;
public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public TaskType Type { get; set; }
}