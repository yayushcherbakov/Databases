using Microsoft.EntityFrameworkCore;
using OlympicGamesApp.Database.Entities;

namespace OlympicGamesApp.Database;

public class ApplicationContext: DbContext
{
    public DbSet<Country> Countries { get; set; }
    
    public DbSet<Event> Events { get; set; }
    
    public DbSet<Olympic> Olympics { get; set; }
    
    public DbSet<Player> Players { get; set; }
    
    public DbSet<Result> Results { get; set; }
    
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;" +
                                 "Port=5432;" +
                                 "Username=Shcherbakow_205;" +
                                 "Password=Shcherbakow_205;" +
                                 "Database=Shcherbakow_205_Olympic_games");
    }
}