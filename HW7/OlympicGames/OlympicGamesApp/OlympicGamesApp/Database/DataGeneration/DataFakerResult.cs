using OlympicGamesApp.Database.Entities;

namespace OlympicGamesApp.Database.DataGeneration;

public class DataFakerResult
{
    public DataFakerResult(List<Country> counties, List<Player> players, List<Olympic> olympics,
        List<Event> events, List<Result> results)
    {
        Counties = counties;
        Players = players;
        Olympics = olympics;
        Events = events;
        Results = results;
    }

    public List<Country> Counties { get; set; }

    public List<Player> Players { get; set; }

    public List<Olympic> Olympics { get; set; }

    public List<Event> Events { get; set; }

    public List<Result> Results { get; set; }
}
