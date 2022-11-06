using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OlympicGamesApp.Database.Entities;

[Table("Players")]
public class Player
{
    [Column("name"), Required]
    [MaxLength(40)]
    public string Name { get; set; }

    [Key]
    [Column("player_id"), Required]
    [MaxLength(10)]
    public string PlayerId { get; set; }

    [ForeignKey(nameof(Country)), Required]
    [Column("country_id")]
    [MaxLength(3)]
    public string CountryId { get; set; }

    public Country Country { get; set; }

    [Column("birthdate"), Required]
    public DateTime Birthdate { get; set; }

    public ICollection<Result> Results { get; set; }
}