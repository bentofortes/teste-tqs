using backend.Application.DTOs;
using backend.Domain;

namespace backend.Application.Interfaces;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id);
    
    Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(int leadId, int page, int pageSize);
   
    Task AddAsync(TaskItem task);

    Task UpdateAsync(TaskItem task);

    Task DeleteByIdAsync(int id);
}