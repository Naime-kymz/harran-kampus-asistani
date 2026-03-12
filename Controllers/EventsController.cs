using HarranKampusAsistani.API.Data;
using HarranKampusAsistani.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarranKampusAsistani.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _db;

    public EventsController(AppDbContext db)
    {
        _db = db;
    }

    // TÜM ETKİNLİKLERİ GETİR
    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetAll()
    {
        var items = await _db.Events
            .OrderBy(x => x.StartAt)
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Event>> GetById(int id)
    {
        var item = await _db.Events.FindAsync(id);
        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Event>> Create([FromBody] Event request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("Title zorunlu.");

        if (request.StartAt == default)
            return BadRequest("StartAt zorunlu.");

        request.Id = 0;
        request.CreatedAt = DateTime.UtcNow;

        _db.Events.Add(request);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Event>> Update(int id, [FromBody] Event request)
    {
        var item = await _db.Events.FindAsync(id);

        if (item == null)
            return NotFound();

        item.Title = request.Title;
        item.Description = request.Description;
        item.Location = request.Location;
        item.SourceUrl = request.SourceUrl;
        item.StartAt = request.StartAt;
        item.EndAt = request.EndAt;

        await _db.SaveChangesAsync();

        return Ok(item);
    }

    // ETKİNLİK SİL
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Events.FindAsync(id);

        if (item == null)
            return NotFound();

        _db.Events.Remove(item);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}