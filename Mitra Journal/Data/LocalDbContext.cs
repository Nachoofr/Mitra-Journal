using Microsoft.EntityFrameworkCore;
using Mitra_Journal.Entities;

namespace Mitra_Journal.Data;

public class LocalDbContext : DbContext
{
    public DbSet<Journal>  Journals { get; set; }
    
    public DbSet<Tags> Tags { get; set; }
    
    public DbSet<Mood> Moods { get; set; }
    
    public LocalDbContext(DbContextOptions<LocalDbContext> options)
        : base(options) { }
}
