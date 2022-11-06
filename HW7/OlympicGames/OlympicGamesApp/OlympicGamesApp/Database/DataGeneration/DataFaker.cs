using Bogus;
using ISO._3166;
using OlympicGamesApp.Database.Entities;

namespace OlympicGamesApp.Database.DataGeneration;

public class DataFaker
{
    private readonly List<string> EventTypes = new List<string>()
    {
        "HDR",
        "KH",
        "GHD",
        "IUL",
        "LXT",
        "MHJ",
        "MFG",
        "DED",
        "NHK",
        "PLD",
        "EKF",
    };

    private readonly List<string> ResultNotedIn = new List<string>()
    {
        "seconds",
        "meters"
    };

    public async Task<DataFakerResult> GenerateFakeData(DataFakerSettings settings)
    {
        ValidateSettings(settings);

        var random = new Random();

        var countryData = CountryCodesResolver.GetList()
            .OrderBy(x => random.Next())
            .Take(settings.CountriesCount)
            .ToList();

        var countryIndex = -1;
        var fakeCountry = new Faker<Country>()
            .RuleFor(c => c.CountryId, f =>
            {
                countryIndex++;
                return countryData[countryIndex].Alpha3;
            })
            .RuleFor(c => c.Name, f =>
            {
                var name = countryData[countryIndex].Name;

                return name.Length > 40 ? name.Substring(0, 40) : name;
            })
            .RuleFor(c => c.AreaSqkm, f => f.Random.Int(settings.MinCountryAreaSqkm, settings.MaxCountryAreaSqkm))
            .RuleFor(c => c.Population, f => f.Random.Int(settings.MinCountryPopulation, settings.MaxCountryPopulation));

        var counties = await Task.Run(() => fakeCountry.Generate(settings.CountriesCount));

        var players = new List<Player>();
        var playerIndex = -1;
        foreach (var country in counties)
        {
            var fakePlayer = new Faker<Player>()
                .RuleFor(c => c.PlayerId, f => $"P-{++playerIndex}")
                .RuleFor(c => c.Name, f =>
                {
                    var name = f.Person.FullName;
                    return name.Length > 40 ? name.Substring(0, 40) : name;
                })
                .RuleFor(c => c.CountryId, f => country.CountryId)
                .RuleFor(c => c.Birthdate, f => f.Date.Between(new DateTime(1960, 1, 1), new DateTime(1980, 1, 1)).ToUniversalTime())
                .RuleFor(c => c.Results, f => new List<Result>());

            var playersToAdd = await Task.Run(
                () => fakePlayer.GenerateBetween(settings.MinPlayersInCountry, settings.MaxPlayersInCountry));

            country.Players = playersToAdd;
            players.AddRange(playersToAdd);
        }

        var olympics = new List<Olympic>();
        var olympicIndex = -1;
        for (var year = 1998; year < 2008; year += 2)
        {
            var month = 1998 % 4 == 0 ? 7 : 1;
            var fakeOlympic = new Faker<Olympic>()
                .RuleFor(c => c.OlympicId, f => $"O-{++olympicIndex}")
                .RuleFor(c => c.CountryId, f => counties.OrderBy(x => random.Next())
                    .First().CountryId)
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.Year, f => year)
                .RuleFor(c => c.StartDate, 
                    f => new DateTime(year, month, 1).ToUniversalTime())
                .RuleFor(c => c.EndDate, 
                    f => new DateTime(year, month, 14).ToUniversalTime())
                .RuleFor(c => c.Events, f => new List<Event>());

            olympics.AddRange(await Task.Run(() => fakeOlympic.Generate(1)));
        }

        var isTeamEvent = false;
        var fakeEventTemplates = new Faker<Event>()

            .RuleFor(c => c.Name, f =>
            {
                var name = f.Lorem.Sentence(3);
                return name.Length > 40 ? name.Substring(0, 40) : name;
            })
            .RuleFor(c => c.EventType, f => EventTypes[f.Random.Int(0, EventTypes.Count - 1)])
            .RuleFor(c => c.IsTeamEvent, f =>
            {
                isTeamEvent = random.Next() % 4 == 0;
                return isTeamEvent;
            })
            .RuleFor(c => c.NumPlayersInTeam,
                f => isTeamEvent ? f.Random.Int(settings.MinPlayersInTeam, settings.MaxPlayersInTeam) : 1)
            .RuleFor(c => c.ResultNotedIn, f => ResultNotedIn[f.Random.Int(0, ResultNotedIn.Count - 1)]);

        var olympicEventTemplates = await Task.Run(() => fakeEventTemplates.Generate(settings.EventCountInOlympic));

        var events = new List<Event>();
        var eventIndex = -1;

        foreach (var olympicEventTemplate in olympicEventTemplates)
        {
            foreach (var olympic in olympics)
            {
                var eventToAdd = new Event()
                {
                    EventId = $"E-{++eventIndex}",
                    Name = olympicEventTemplate.Name,
                    EventType = olympicEventTemplate.EventType,
                    OlympicId = olympic.OlympicId,
                    IsTeamEvent = olympicEventTemplate.IsTeamEvent,
                    NumPlayersInTeam = olympicEventTemplate.NumPlayersInTeam,
                    ResultNotedIn = olympicEventTemplate.ResultNotedIn,
                    Results = new List<Result>()
                };

                olympic.Events.Add(eventToAdd);
                events.Add(eventToAdd);
            }
        }

        var results = new List<Result>();

        foreach (var olympic in olympics)
        {
            foreach (var olimpicIvent in olympic.Events)
            {
                // Calculate draws.
                var goldMedals = random.Next() % 10 == 0 ? 2 : 1;
                var silverMedals = random.Next() % 10 == 0 ? 2 : 1;
                var bronzeMedals = random.Next() % 10 == 0 ? 2 : 1;

                var countOfMedals = goldMedals + silverMedals + bronzeMedals;

                var winningCountries = counties.OrderBy(x => random.Next())
                    .Take(countOfMedals)
                    .ToList();

                var countPlayersInTeam = olimpicIvent.IsTeamEvent ? olimpicIvent.NumPlayersInTeam : 1;

                var score = random.Next(1, 30);
                foreach (var country in winningCountries)
                {
                    var currentResult = score;
                    string medal;

                    if (bronzeMedals > 0)
                    {
                        medal = "BRONZE";
                        --bronzeMedals;

                        if (bronzeMedals == 0)
                        {
                            score += random.Next(1, 30);
                        }
                    }
                    else if (silverMedals > 0)
                    {
                        medal = "SILVER";
                        --silverMedals;

                        if (silverMedals == 0)
                        {
                            score += random.Next(1, 30);
                        }
                    }
                    else
                    {
                        medal = "GOLD";
                        --goldMedals;

                        if (goldMedals == 0)
                        {
                            score += random.Next(1, 30);
                        }
                    }

                    var winners = country.Players.OrderBy(x => random.Next()).Take(countPlayersInTeam);

                    foreach (var winner in winners)
                    {
                        var result = new Result()
                        {
                            EventId = olimpicIvent.EventId,
                            PlayerId = winner.PlayerId,
                            Medal = medal,
                            Score = score
                        };
                        olimpicIvent.Results.Add(result);
                        winner.Results.Add(result);
                        results.Add(result);
                    }
                }
            }
        }

        return new DataFakerResult(counties, players, olympics, events, results);
    }

    private static void ValidateSettings(DataFakerSettings settings)
    {
        if (settings.CountriesCount > CountryCodesResolver.GetList().Count)
        {
            throw new ArgumentException($"There are fewer countries in the world than {settings.CountriesCount}.");
        }

        if (settings.CountriesCount < 6)
        {
            throw new ArgumentException("Countries count must be greater than 6.");
        }

        if (settings.MinPlayersInCountry < 6)
        {
            throw new ArgumentException("Players count must be greater than 6.");
        }

        if (settings.MaxPlayersInTeam > 7)
        {
            throw new ArgumentException("Players count must be less than 7.");
        }

        if (settings.MinPlayersInTeam < 1)
        {
            throw new ArgumentException("Players count must be greater than 1.");
        }

        if (settings.CountriesCount < 1
            || settings.MinCountryAreaSqkm < 1
            || settings.MaxCountryAreaSqkm < 1
            || settings.MinCountryPopulation < 1
            || settings.MaxCountryPopulation < 1
            || settings.MinPlayersInCountry < 1
            || settings.MaxPlayersInCountry < 1
            || settings.MinPlayersInTeam < 1
            || settings.MaxPlayersInTeam < 1
            || settings.EventCountInOlympic < 1)
        {
            throw new ArgumentException("Only positive values in settings.");
        }

        if (settings.MinCountryAreaSqkm > settings.MaxCountryAreaSqkm)
        {
            throw new ArgumentException("MinCountryAreaSqkm greater MaxCountryAreaSqkm.");
        }

        if (settings.MinCountryPopulation > settings.MaxCountryPopulation)
        {
            throw new ArgumentException("MinCountryPopulation greater MaxCountryPopulation.");
        }

        if (settings.MinPlayersInCountry > settings.MaxPlayersInCountry)
        {
            throw new ArgumentException("MinPlayersInCountry greater MaxPlayersInCountry.");
        }

        if (settings.MinPlayersInTeam > settings.MaxPlayersInTeam)
        {
            throw new ArgumentException("MinPlayersInCountry greater MaxPlayersInCountry.");
        }
    }
}