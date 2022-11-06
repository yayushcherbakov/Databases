namespace OlympicGamesApp.Database.DataGeneration;

public class DataFakerSettings
{
    public int CountriesCount { get; set; } = 6;

    public int MinCountryAreaSqkm { get; set; } = 500;

    public int MaxCountryAreaSqkm { get; set; } = 99999999;

    public int MinCountryPopulation { get; set; } = 5000;

    public int MaxCountryPopulation { get; set; } = 99999999;

    public int MinPlayersInCountry { get; set; } = 6;

    public int MaxPlayersInCountry { get; set; } = 20;

    public int MinPlayersInTeam { get; set; } = 2;

    public int MaxPlayersInTeam { get; set; } = 6;

    public int EventCountInOlympic { get; set; } = 20;
}
