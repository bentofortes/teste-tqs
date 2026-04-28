using Microsoft.EntityFrameworkCore;
using backend.Domain;
using backend.Application.Interfaces;

namespace backend.Infra.Repositories;

public class TaskRepository(AppDbContext context): ITaskRepository
{
    private readonly AppDbContext _context = context;

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(int leadId, int page, int pageSize)
    {
        var query = _context.Tasks.AsQueryable();

        var totalCount = await query.CountAsync();

        var tasks = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (tasks, totalCount);
    }

    public async Task AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        var oldTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id);
        if (oldTask == null) return;

        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}