using HarranKampusAsistani.API.Models;

namespace HarranKampusAsistani.API.Services;

public static class AnnouncementsStore
{
    private static readonly List<Announcement> _items = new()
    {
        new Announcement
        {
            Id = 1,
            Title = "Hoş geldiniz",
            Content = "Kampüs Asistanı demo duyurusu.",
            SourceUrl = "https://harran.edu.tr",
            PublishedAt = DateTime.UtcNow.AddDays(-1),
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        }
    };

    public static List<Announcement> GetAll() => _items;

    public static Announcement? GetById(int id) =>
        _items.FirstOrDefault(x => x.Id == id);

    public static Announcement Add(Announcement a)
    {
        var nextId = _items.Count == 0 ? 1 : _items.Max(x => x.Id) + 1;
        a.Id = nextId;
        a.CreatedAt = DateTime.UtcNow;
        _items.Add(a);
        return a;
    }

    public static bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return false;
        _items.Remove(existing);
        return true;
    }
}