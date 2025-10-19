using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpBetSafe.Api.Data;
using XpBetSafe.Api.DTOs;
using XpBetSafe.Api.Models;

namespace XpBetSafe.Api.Controllers;

[ApiController]
[Route("api/alerts")]
public class AlertsController(AppDbContext db) : ControllerBase
{
    // GET /api/alerts?risk=High&from=2025-09-01&to=2025-10-31&search=palavra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAll(
        [FromQuery] RiskLevel? risk,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] string? search)
    {
        var q = db.Alerts.Include(a => a.User).AsQueryable();

        if (risk.HasValue) q = q.Where(a => a.Risk == risk.Value);
        if (from.HasValue) q = q.Where(a => a.OccurredAt >= from.Value);
        if (to.HasValue) q = q.Where(a => a.OccurredAt <= to.Value);
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(a => a.Title.Contains(search) || a.Description.Contains(search));

        var result = await q
            .OrderByDescending(a => a.OccurredAt)
            .Select(a => new {
                a.Id,
                a.Title,
                a.Risk,
                a.OccurredAt,
                User = new { a.UserId, a.User!.FullName, a.User.Email }
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Alert>> GetOne(int id)
    {
        var alert = await db.Alerts.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        return alert is null ? NotFound() : Ok(alert);
    }

    [HttpPost]
    public async Task<ActionResult<Alert>> Create(AlertCreateDto dto)
    {
        var alert = new Alert
        {
            Title = dto.Title,
            Description = dto.Description,
            Risk = dto.Risk,
            UserId = dto.UserId,
            OccurredAt = dto.OccurredAt ?? DateTime.UtcNow
        };

        db.Alerts.Add(alert);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOne), new { id = alert.Id }, alert);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AlertUpdateDto dto)
    {
        var a = await db.Alerts.FindAsync(id);
        if (a is null) return NotFound();

        if (dto.Title is not null) a.Title = dto.Title;
        if (dto.Description is not null) a.Description = dto.Description;
        if (dto.Risk.HasValue) a.Risk = dto.Risk.Value;
        if (dto.OccurredAt.HasValue) a.OccurredAt = dto.OccurredAt.Value;

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var a = await db.Alerts.FindAsync(id);
        if (a is null) return NotFound();

        db.Alerts.Remove(a);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
