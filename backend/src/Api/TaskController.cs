using Microsoft.AspNetCore.Mvc;
using backend.Application.DTOs;
using backend.Application.Services;

namespace backend.Controllers;

[ApiController]
[Route("leads/{leadId}/tasks")]
public class TasksController(TaskService taskService): ControllerBase
{
    private readonly TaskService _taskService = taskService;
    
    [HttpGet]
    public async Task<IActionResult> GetTasks(int leadId, int page = 1, int pageSize = 10)
    {
        var result = await _taskService.GetTasksAsync(leadId, page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(int leadId, [FromBody] TaskCreateDto dto)
    {
        await _taskService.CreateTaskAsync(leadId, dto);
        return Created();
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(int leadId, int taskId, [FromBody] TaskUpdateDto dto)
    {
        await _taskService.UpdateTaskAsync(taskId, dto);
        return NoContent();
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(int leadId, int taskId)
    {
        await _taskService.DeleteTaskAsync(taskId);
        return NoContent();
    }
}