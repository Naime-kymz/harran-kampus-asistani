using HarranKampusAsistani.API.Models;

namespace HarranKampusAsistani.API.Services;

public static class EventsStore
{
    private static readonly List<Event> _items = new()
    {
        new Event
        {
            Id = 1,
            Title = "Örnek Etkinlik",
            Description = "Kampüs Asistanı demo etkinliği.",
            Location = "Osmanbey Kampüsü",
            StartAt = DateTime.UtcNow.AddDays(3),
            EndAt = DateTime.UtcNow.AddDays(3).AddHours(2),
            SourceUrl = "https://harran.edu.tr",
            CreatedAt = DateTime.UtcNow
        }
    };

    public static List<Event> GetAll() => _items;

    public static Event? GetById(int id) =>
        _items.FirstOrDefault(x => x.Id == id);

    public static Event Add(Event e)
    {
        var nextId = _items.Count == 0 ? 1 : _items.Max(x => x.Id) + 1;
        e.Id = nextId;
        e.CreatedAt = DateTime.UtcNow;
        _items.Add(e);
        return e;
    }

    public static bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return false;
        _items.Remove(existing);
        return true;
    }
}