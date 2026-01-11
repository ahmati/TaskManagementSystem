using Application.Interfaces;
using Application.Tasks.Dtos;
using Application.Tasks.Services;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMediator _mediator;

    public TasksController(ITaskRepository taskService, IMediator mediator)
    {
        _taskRepository = taskService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetTasksQuery query)
    {
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var task = await _taskRepository.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var updated = await _taskRepository.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/todo")]
    public async Task<IActionResult> UpdateStatusToDo(int id)
    {
        var updated = await _taskRepository.UpdateStatusAsync(id, Status.ToDo);
        return updated ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/inprogress")]
    public async Task<IActionResult> UpdateStatusInProgress(int id)
    {
        var updated = await _taskRepository.UpdateStatusAsync(id, Status.InProgress);
        return updated ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/done")]
    public async Task<IActionResult> UpdateStatusDone(int id)
    {
        var updated = await _taskRepository.UpdateStatusAsync(id, Status.Done);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _taskRepository.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
