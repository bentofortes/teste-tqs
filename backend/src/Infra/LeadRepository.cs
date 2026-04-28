using Microsoft.EntityFrameworkCore;
using backend.Domain;
using backend.Application.Interfaces;

namespace backend.Infra.Repositories;

public class LeadRepository(AppDbContext context): ILeadRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Lead?> GetByIdAsync(int id)
    {
        return await _context.Leads
            .Include(l => l.Tasks)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<(List<Lead> Items, int TotalCount)> GetPagedAsync(string? search, LeadStatus? status, int page, int pageSize)
    {
        var query = _context.Leads.Include(l => l.Tasks).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(l => l.Name.Contains(search) || l.Email.Contains(search));
        }

        if (status.HasValue)
        {
            query = query.Where(l => l.Status == status.Value);
        }

        var totalCount = await query.CountAsync();

        var leads = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (leads, totalCount);
    }

    public async Task AddAsync(Lead lead)
    {
        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Lead lead)
    {
        var oldLead = await GetByIdAsync(lead.Id);
        if (oldLead == null) return;

        _context.Leads.Update(lead);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var lead = await GetByIdAsync(id);
        if (lead == null) return;
        
        _context.Leads.Remove(lead);
        await _context.SaveChangesAsync();
    }
}