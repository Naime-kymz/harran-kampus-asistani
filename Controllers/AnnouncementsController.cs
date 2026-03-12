using HarranKampusAsistani.API.Data;
using HarranKampusAsistani.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarranKampusAsistani.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AnnouncementsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Announcement>>> GetAll()
    {
        var items = await _db.Announcements
            .OrderByDescending(x => x.PublishedAt)
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Announcement>> GetById(int id)
    {
        var item = await _db.Announcements.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }
    [HttpPut("{id:int}")]
public async Task<ActionResult<Announcement>> Update(int id, [FromBody] Announcement updated)
{
    var existing = await _db.Announcements.FindAsync(id);
    if (existing == null)
        return NotFound();

    existing.Title = updated.Title;
    existing.Content = updated.Content;
    existing.SourceUrl = updated.SourceUrl;
    existing.PublishedAt = updated.PublishedAt;

    await _db.SaveChangesAsync();

    return Ok(existing);
}

    [HttpPost]
    public async Task<ActionResult<Announcement>> Create([FromBody] Announcement announcement)
    {
        announcement.Id = 0;
        announcement.CreatedAt = DateTime.UtcNow;

        _db.Announcements.Add(announcement);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = announcement.Id }, announcement);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Announcements.FindAsync(id);
        if (item == null) return NotFound();

        _db.Announcements.Remove(item);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}