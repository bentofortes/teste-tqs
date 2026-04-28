using backend.Domain;

namespace backend.Application.DTOs;

public record TaskCreateDto(
    string Title,
    DateTime? DueDate,
    Domain.TaskStatus? Status
);

public record TaskUpdateDto(
    string Title,
    DateTime? DueDate,
    Domain.TaskStatus Status
);

public record TaskDto(
    int Id,
    int LeadId,
    string Title,
    DateTime? DueDate,
    Domain.TaskStatus Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);