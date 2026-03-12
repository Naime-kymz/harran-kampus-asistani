using HarranKampusAsistani.API.Data;
using HarranKampusAsistani.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarranKampusAsistani.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class YemekhaneFeedbackController : ControllerBase
{
    private readonly AppDbContext _db;

    public YemekhaneFeedbackController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeekly()
    {
        var weekStart = GetWeekStart(DateTime.UtcNow);

        var items = await _db.YemekhaneFeedbacks
            .Where(x => x.WeekStart == weekStart)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var total = items.Count;
        var average = total == 0 ? 0 : Math.Round(items.Average(x => x.Rating), 2);

        return Ok(new
        {
            weekStart,
            total,
            average,
            comments = items.Select(x => new
            {
                x.Rating,
                x.Comment,
                x.CreatedAt
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] YemekhaneFeedback request)
    {
        if (request.Rating < 1 || request.Rating > 5)
            return BadRequest("Puan 1 ile 5 arasında olmalıdır.");

        var item = new YemekhaneFeedback
        {
            Rating = request.Rating,
            Comment = request.Comment,
            WeekStart = GetWeekStart(DateTime.UtcNow),
            CreatedAt = DateTime.UtcNow
        };

        _db.YemekhaneFeedbacks.Add(item);
        await _db.SaveChangesAsync();

        return Ok(item);
    }

    private static DateTime GetWeekStart(DateTime date)
    {
        int diff = (7 + ((int)date.DayOfWeek - (int)DayOfWeek.Monday)) % 7;
        return date.Date.AddDays(-1 * diff);
    }
}