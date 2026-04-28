using backend.Domain;

namespace backend.Application.DTOs;

public record PagedResult<T>(
    List<T> Items,
    int TotalCount,
    int Page,
    int PageSize
);