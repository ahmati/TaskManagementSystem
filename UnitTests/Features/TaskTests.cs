using Application.Tasks.Dtos;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Features;
public class TaskRepositoryTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateTask()
    {
        // Arrange
        var context = DbContextFactory.Create();
        var taskRepository = new TaskRepository(context);

        var dto = new CreateTaskDto
        {
            Title = "Test Task",
            Description = "Test Description"
        };

        // Act
        var result = await taskRepository.CreateAsync(dto);

        // Assert
        result.Id.Should().BeGreaterThan(0);
        result.Title.Should().Be("Test Task");
        context.Set<TaskItem>().Count().Should().Be(1);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnTasks()
    {
        var context = DbContextFactory.Create();
        var service = new TaskRepository(context);

        await service.CreateAsync(new CreateTaskDto { Title = "Task 1" });
        await service.CreateAsync(new CreateTaskDto { Title = "Task 2" });

        var tasks = await service.GetAllAsync();

        tasks.Should().HaveCount(2);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTask()
    {
        var context = DbContextFactory.Create();
        var service = new TaskRepository(context);

        var task = await service.CreateAsync(new CreateTaskDto { Title = "Delete Me" });

        var deleted = await service.DeleteAsync(task.Id);

        deleted.Should().BeTrue();
        context.Set<TaskItem>().Should().BeEmpty();
    }
}
