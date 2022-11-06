using Microsoft.EntityFrameworkCore;
using OlympicGamesApp.Database.Extentions;

namespace OlympicGamesApp.Database;

public class DatabaseStateService
{
    private readonly ApplicationContext _applicationContext;

    public DatabaseStateService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task Migrate()
    {
        await _applicationContext.Database.MigrateAsync();
    }

    public async Task ClearDatabase()
    {
        _applicationContext.Results.Clear();
        _applicationContext.Players.Clear();
        _applicationContext.Events.Clear();
        _applicationContext.Olympics.Clear();
        _applicationContext.Countries.Clear();

        await _applicationContext.SaveChangesAsync();
    }
}
