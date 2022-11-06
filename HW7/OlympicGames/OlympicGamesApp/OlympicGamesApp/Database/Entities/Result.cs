using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OlympicGamesApp.Database.Entities;

[Table("Results")]
public class Result
{
    [Key]
    [Column("result_id"), Required]
    public int ResultId { get; set; }

    [ForeignKey(nameof(Event))]
    [Column("event_id"), Required]
    [MaxLength(7)]
    public string EventId { get; set; }

    public Event Event { get; set; }

    [ForeignKey(nameof(Player))]
    [Column("player_id"), Required]
    [MaxLength(10)]
    public string PlayerId { get; set; }

    public Player Player { get; set; }

    [Column("medal"), Required]
    [MaxLength(7)]
    public string Medal { get; set; }

    [Column("result"), Required]
    public float Score { get; set; }
}