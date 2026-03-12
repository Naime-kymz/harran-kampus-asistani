using System.ComponentModel.DataAnnotations;

namespace HarranKampusAsistani.API.Models;

public class YemekhaneFeedback
{
    public int Id { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(500)]
    public string? Comment { get; set; }

    public DateTime WeekStart { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}