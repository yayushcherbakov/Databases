using OlympicGamesApp.Database;
using OlympicGamesApp.Database.DataGeneration;

namespace OlympicGamesApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var isNeedClearDatabase = false;

        using var context = new ApplicationContext();

        var databaseStateService = new DatabaseStateService(context);

        Console.WriteLine("Applies any pending migrations for the context to the database");
        await databaseStateService.Migrate();


        if (isNeedClearDatabase)
        {
            Console.WriteLine("Clear database");
            await databaseStateService.ClearDatabase();
        }

        // Generate data if empty.
        if (!context.Countries.Any()
            && !context.Players.Any()
            && !context.Olympics.Any()
            && !context.Events.Any()
            && !context.Results.Any())
        {
            Console.WriteLine("Generate fake data and save to database");
            var dataFaker = new DataFaker();
            var fakeData = await dataFaker.GenerateFakeData(new());

            context.Countries.AddRange(fakeData.Counties);
            context.Players.AddRange(fakeData.Players);
            context.Olympics.AddRange(fakeData.Olympics);
            context.Events.AddRange(fakeData.Events);
            context.Results.AddRange(fakeData.Results);

            await context.SaveChangesAsync();
        }

        var olympicsRepository = new OlympicsRepository(context);

        // Task 1.
        var res1 = await olympicsRepository.GetGoldMedalStatisticByYear();
        Console.WriteLine("Task 1 - complete!");

        // Task 2.
        var res2 = await olympicsRepository.GetEventIdsWithDraw();
        Console.WriteLine("Task 2 - complete!");

        // Task 3.
        var res3 = await olympicsRepository.GetPlayersWithMedals();
        Console.WriteLine("Task 3 - complete!");

        // Task 4.
        var res4 = await olympicsRepository.GetTop5CountriesByRule();
        Console.WriteLine("Task 4 - complete!");

        Console.ReadKey();
    }
}