namespace HarranKampusAsistani.API.Models;

using System.ComponentModel.DataAnnotations;

public class Announcement
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık zorunludur.")]
    [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir.")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "İçerik zorunludur.")]
    public string Content { get; set; } = "";

    [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
    [StringLength(500)]
    public string? SourceUrl { get; set; }

    [Required(ErrorMessage = "Yayın tarihi zorunludur.")]
    public DateTime PublishedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}