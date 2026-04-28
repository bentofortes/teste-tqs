using backend.Application.DTOs;
using backend.Application.Interfaces;
using backend.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backend.Application.Services;

public class LeadService(ILeadRepository leadRepository, ITaskRepository taskRepository)
{
    private readonly ILeadRepository _leadRepo = leadRepository;
    private readonly ITaskRepository _taskRepo = taskRepository;

    public async Task<LeadWithTasksDto?> GetLeadByIdAsync(int id)
    {
        var lead = await _leadRepo.GetByIdAsync(id);
        if (lead == null) return null;

        var tasks = await _taskRepo.GetPagedAsync(lead.Id, 1, 10);

        var tasksDtos = tasks.Items.Select(t => new TaskDto(
            Id: t.Id,
            Title: t.Title,
            Status: t.Status,
            LeadId: t.LeadId,
            DueDate: t.DueDate,
            CreatedAt: t.CreatedAt,
            UpdatedAt: t.UpdatedAt
        )).ToList();

        var results = new LeadWithTasksDto(
            Id: lead.Id,
            Name: lead.Name,
            Email: lead.Email,
            Status: lead.Status,
            CreatedAt: lead.CreatedAt,
            UpdatedAt: lead.UpdatedAt,
            Tasks: tasksDtos
        );

        return results;
    }

    public async Task<PagedResult<LeadDto>> GetLeadsAsync(string? search, LeadStatus? status, int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize > 10) pageSize = 10;

        var (leads, totalCount) = await _leadRepo.GetPagedAsync(search, status, page, pageSize);

        var leadDtos = leads.Select(l => new LeadDto(
            Id: l.Id,
            Name: l.Name,
            Email: l.Email,
            Status: l.Status,
            CreatedAt: l.CreatedAt,
            UpdatedAt: l.UpdatedAt,
            TasksCount: l.Tasks.Count
        )).ToList();

        return new PagedResult<LeadDto>(
            Items: leadDtos,
            TotalCount: totalCount,
            Page: page,
            PageSize: pageSize
        );
    }

    public async Task CreateLeadAsync(LeadCreateDto dto)
    {
        var lead = new Lead
        {
            Name = dto.Name,
            Email = dto.Email
        };

        await _leadRepo.AddAsync(lead);
    }

    public async Task UpdateLeadAsync(int id, LeadUpdateDto dto)
    {
        var lead = await _leadRepo.GetByIdAsync(id);
        if (lead == null) throw new Exception("Lead not found");

        lead.Name = dto.Name;
        lead.Email = dto.Email;
        lead.Status = dto.Status;

        await _leadRepo.UpdateAsync(lead);
    }

    public async Task DeleteLeadAsync(int id)
    {
        await _leadRepo.DeleteByIdAsync(id);
    }
}