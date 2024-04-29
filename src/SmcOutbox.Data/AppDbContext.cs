using Microsoft.EntityFrameworkCore;
using SmcOutbox.Core.Meetings;
using SmcOutbox.Data.Outbox;

namespace SmcOutbox.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SmcOutbox;User=sa;Password=Semicrol_10;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }
    }
    
    
    public DbSet<Meeting> Meetings { get; set; }

    public DbSet<OutboxMessage> Messages { get; set; }
}