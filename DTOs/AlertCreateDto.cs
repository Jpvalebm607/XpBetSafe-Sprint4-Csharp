using XpBetSafe.Api.Models;

namespace XpBetSafe.Api.DTOs;

public record AlertCreateDto(
    string Title,
    string Description,
    RiskLevel Risk,
    int UserId,
    DateTime? OccurredAt
);
