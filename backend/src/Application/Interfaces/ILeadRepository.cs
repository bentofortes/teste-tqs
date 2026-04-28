using backend.Application.DTOs;
using backend.Domain;

namespace backend.Application.Interfaces;

public interface ILeadRepository
{
    Task<Lead?> GetByIdAsync(int id);

    Task<(List<Lead> Items, int TotalCount)> GetPagedAsync(string? search, LeadStatus? status, int page, int pageSize);
   
    Task AddAsync(Lead lead);

    Task UpdateAsync(Lead lead);

    Task DeleteByIdAsync(int id);
}