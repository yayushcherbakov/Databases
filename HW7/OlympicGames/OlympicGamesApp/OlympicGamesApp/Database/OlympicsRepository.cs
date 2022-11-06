using Microsoft.EntityFrameworkCore;
using OlympicGamesApp.Database.Models;

namespace OlympicGamesApp.Database;

public class OlympicsRepository
{
    private readonly ApplicationContext _applicationContext;

    public OlympicsRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    /*  Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, 
     *  количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, 
     *  родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, 
     *  и количество золотых медалей, завоеванных игроками,родившимися в этом году.
     */
    public async Task<List<GoldMedalStatistic>> GetGoldMedalStatisticByYear()
    {
        var query = _applicationContext.Olympics
            .Where(olimpic => olimpic.Year == 2004)
            .SelectMany(olimpic => olimpic.Events)
            .SelectMany(olompicEvent => olompicEvent.Results)
            .Select(result => new
            {
                Player = result.Player,
                Result = result
            })
            .GroupBy(
                player => player.Player.Birthdate.Year,
                (year, pr) => new GoldMedalStatistic()
                {
                    Year = year,
                    WinnersCount = pr.Where(x => x.Result.Medal == "GOLD")
                        .Select(x => x.Player.PlayerId)
                        .Distinct()
                        .Count(),
                    GoldMedalsCount = pr
                        .Count(x => x.Result.Medal == "GOLD")
                });

        var queryString = query.ToQueryString();

        return await query.ToListAsync();
    }

    /*  Перечислите все индивидуальные(не групповые) соревнования, в которых была ничья в счете,
     *  и два или более игрока выиграли золотую медаль.
     */
    public async Task<List<string>> GetEventIdsWithDraw()
    {
        var query = _applicationContext.Results
            .Select(result => new { Event = result.Event, Result = result })
            .Where(x => !x.Event.IsTeamEvent && x.Result.Medal == "GOLD")
            .GroupBy(x => x.Event.EventId)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key);

        var queryString = query.ToQueryString();

        return await query.ToListAsync();
    }

    /* Найдите всех игроков, которые выиграли хотя бы одну медаль(GOLD, SILVER и BRONZE) на одной Олимпиаде. 
     * (player-name, olympic-id).
     */
    public async Task<List<PlayerWithMedal>> GetPlayersWithMedals()
    {
        var query = _applicationContext.Results
            .GroupBy(x => new { x.PlayerId, x.Player.Name, x.Event.OlympicId })
            .Select(x => new { x.Key.PlayerId, x.Key.Name, x.Key.OlympicId })
            .Distinct()
            .Select(x => new PlayerWithMedal()
            {
                Name = x.Name,
                OlympicId = x.OlympicId
            });

        var queryString = query.ToQueryString();

        return await query.ToListAsync();
    }

    /* Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых 
     * медалей к численности населения.
     */
    public async Task<List<string>> GetTop5CountriesByRule()
    {
        var query = _applicationContext.Olympics
            .Where(olimpic => olimpic.Year == 2000)
            .SelectMany(olimpic => olimpic.Events)
            .Where(x => x.IsTeamEvent)
            .SelectMany(olompicEvent => olompicEvent.Results)
            .Select(result => result.Player)
            .Select(player => player.Country)
            .GroupBy(x => new { CountryId = x.CountryId, Population = x.Population })
            .OrderBy(x => x.Count() / x.Key.Population)
            .Select(x => x.Key.CountryId)
            .Take(5);

        var queryString = query.ToQueryString();

        return await query.ToListAsync();
    }
}