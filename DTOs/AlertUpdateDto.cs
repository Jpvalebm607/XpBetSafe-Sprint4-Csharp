using XpBetSafe.Api.Models;

namespace XpBetSafe.Api.DTOs;

public record AlertUpdateDto(
    string? Title,
    string? Description,
    RiskLevel? Risk,
    DateTime? OccurredAt
);
