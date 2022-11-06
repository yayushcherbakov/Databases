using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlympicGamesApp.Database.Entities;

[Table("Events")]
public class Event
{
    [Key]
    [Column("event_id"), Required]
    [MaxLength(7)]
    public string EventId { get; set; }

    [Column("name"), Required]
    [MaxLength(40)]
    public string Name { get; set; }

    [Column("eventtype"), Required]
    [MaxLength(20)]
    public string EventType { get; set; }

    [ForeignKey(nameof(Olympic))]
    [Column("olympic_id"), Required]
    [MaxLength(7)]
    public string OlympicId { get; set; }

    public Olympic Olympic { get; set; }

    [Column("is_team_event"), Required]
    public bool IsTeamEvent { get; set; }

    [Column("num_players_in_team"), Required]
    public int NumPlayersInTeam { get; set; }

    [Column("result_noted_in"), Required]
    [MaxLength(100)]
    public string ResultNotedIn { get; set; }

    public ICollection<Result> Results { get; set; }
}