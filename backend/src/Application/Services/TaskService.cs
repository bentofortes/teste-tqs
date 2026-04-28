using backend.Application.DTOs;
using backend.Application.Interfaces;
using backend.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backend.Application.Services;

public class TaskService(ITaskRepository taskRepository, ILeadRepository leadRepository)
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly ILeadRepository _leadRepository = leadRepository;

    public async Task<PagedResult<TaskDto>> GetTasksAsync(int leadId, int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize > 10) pageSize = 10;

        var (tasks, totalCount) = await _taskRepository.GetPagedAsync(leadId, page, pageSize);

        var taskDtos = tasks.Select(t => new TaskDto(
            Id: t.Id,
            Title: t.Title,
            Status: t.Status,
            LeadId: t.LeadId,
            DueDate: t.DueDate,
            CreatedAt: t.CreatedAt,
            UpdatedAt: t.UpdatedAt
        )).ToList();

        return new PagedResult<TaskDto>(
            Items: taskDtos,
            TotalCount: totalCount,
            Page: page,
            PageSize: pageSize
        );
    }

    public async Task CreateTaskAsync(int leadId, TaskCreateDto dto)
    {
        var lead = await _leadRepository.GetByIdAsync(leadId);
        if (lead == null) throw new Exception("Lead not found");

        var task = new TaskItem
        {
            LeadId = leadId,
            Title = dto.Title,
            DueDate = dto.DueDate,
            Status = dto.Status ?? Domain.TaskStatus.Todo
        };

        await _taskRepository.AddAsync(task);
    }

    public async Task UpdateTaskAsync(int id, TaskUpdateDto dto)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)  throw new Exception("Task not found");

        task.Title = dto.Title;
        task.DueDate = dto.DueDate;
        task.Status = dto.Status;

        await _taskRepository.UpdateAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteByIdAsync(id);
    }
}