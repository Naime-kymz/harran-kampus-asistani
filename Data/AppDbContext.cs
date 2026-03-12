using HarranKampusAsistani.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HarranKampusAsistani.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<Event> Events => Set<Event>();

    public DbSet<YemekhaneFeedback> YemekhaneFeedbacks => Set<YemekhaneFeedback>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Announcement>(e =>
        {
            e.ToTable("announcements");
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.Content).HasColumnType("text").IsRequired();
            e.Property(x => x.SourceUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<Event>(e =>
        {
            e.ToTable("events");
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.Description).HasColumnType("text");
            e.Property(x => x.Location).HasMaxLength(200);
            e.Property(x => x.SourceUrl).HasMaxLength(500);
        });
    }
}