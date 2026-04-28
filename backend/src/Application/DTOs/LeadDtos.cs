using backend.Domain;

namespace backend.Application.DTOs;


public record LeadCreateDto(
    string Name,
    string Email,
    LeadStatus? Status
);

public record LeadUpdateDto(
    string Name,
    string Email,
    LeadStatus Status
);

public record LeadDto(
    int Id,
    string Name,
    string Email,
    LeadStatus Status,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    int TasksCount
);

public record LeadWithTasksDto(
    int Id,
    string Name,
    string Email,
    LeadStatus Status,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<TaskDto> Tasks
);