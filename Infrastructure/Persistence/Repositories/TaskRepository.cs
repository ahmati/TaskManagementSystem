using Application.Abstractions;
using Application.Interfaces;
using Application.Tasks.Dtos;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IApplicationDbContext _context;

    public TaskRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskDto>> GetAllAsync()
    {
        return await _context.Set<TaskItem>()
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                Type = t.Type
            })
            .ToListAsync();

    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var task = await _context.Set<TaskItem>().FindAsync(id);
        if (task == null) return null;

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            Type = task.Type
        };
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            Type = dto.Type
        };

        _context.Set<TaskItem>().Add(task);
        await _context.SaveChangesAsync();

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            Type = task.Type
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = await _context.Set<TaskItem>().FindAsync(id);
        if (task == null) return false;

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = task.Status;
        task.Priority = dto.Priority;
        task.Type = dto.Type;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStatusAsync(int id, Status status)
    {
        var task = await _context.Set<TaskItem>().FindAsync(id);
        if (task == null) return false;
        task.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Set<TaskItem>().FindAsync(id);
        if (task == null) return false;

        _context.Set<TaskItem>().Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> SoftDeleteAsync(int id) 
    {
        var task = await _context.Set<TaskItem>().FindAsync(id);
        if (task == null) return false;

        _context.Set<TaskItem>().Remove(task);
        await _context.SoftDeleteAsync(task);
        return true;
    }
}
