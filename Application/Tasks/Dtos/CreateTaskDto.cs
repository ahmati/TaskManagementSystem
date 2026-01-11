using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Tasks.Dtos; 

public class CreateTaskDto
{
    [Required]
    [StringLength(20, ErrorMessage = "Title cannot exceed 20 characters")]
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Status Status { get; set; } = Status.ToDo;
    public Priority Priority { get; set; } = Priority.Medium;
    public TaskType Type { get; set; } = TaskType.Feature;
}
