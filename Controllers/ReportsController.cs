using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpBetSafe.Api.Data;
using XpBetSafe.Api.Models;

namespace XpBetSafe.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController(AppDbContext db) : ControllerBase
{
    // GET /api/reports/alerts-by-user?min=1
    [HttpGet("alerts-by-user")]
    public async Task<ActionResult<IEnumerable<object>>> AlertsByUser([FromQuery] int min = 1)
    {
        var data = await db.Alerts
            .Include(a => a.User)
            .GroupBy(a => new { a.UserId, a.User!.FullName })
            .Select(g => new {
                g.Key.UserId,
                g.Key.FullName,
                TotalAlerts = g.Count(),
                HighRisk = g.Count(a => a.Risk == RiskLevel.High)
            })
            .Where(x => x.TotalAlerts >= min)
            .OrderByDescending(x => x.HighRisk)
            .ThenByDescending(x => x.TotalAlerts)
            .ToListAsync();

        return Ok(data);
    }
}
