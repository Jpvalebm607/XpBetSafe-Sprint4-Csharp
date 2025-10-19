using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpBetSafe.Api.Data;
using XpBetSafe.Api.Models;

namespace XpBetSafe.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll([FromQuery] string? q)
    {
        var query = db.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(u => u.FullName.Contains(q) || u.Email.Contains(q));

        var list = await query
            .OrderByDescending(u => u.CreatedAt)
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetOne(int id)
    {
        var user = await db.Users
            .Include(u => u.Alerts)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(User input)
    {
        db.Users.Add(input);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOne), new { id = input.Id }, input);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, User input)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return NotFound();

        user.FullName = input.FullName;
        user.Email = input.Email;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return NotFound();

        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
