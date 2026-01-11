using Application.Tasks.Dtos;
using Domain.Enums;
namespace Application.Interfaces;
public interface ITaskRepository
{
    Task<List<TaskDto>> GetAllAsync();
    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateAsync(CreateTaskDto dto);
    Task<bool> UpdateAsync(int id, UpdateTaskDto dto);
    Task<bool> UpdateStatusAsync(int id, Status status);
    Task<bool> DeleteAsync(int id);
}
