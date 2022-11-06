using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlympicGamesApp.Database.Entities;

[Table("Countries")]
public class Country
{
    [Column("name"), Required]
    [MaxLength(40)]
    public string Name { get; set; }

    [Key]
    [Column("country_id"), Required]
    [MaxLength(3)]
    public string CountryId { get; set; }

    [Column("area_sqkm"), Required]
    public int AreaSqkm { get; set; }

    [Column("population"), Required]
    public int Population { get; set; }

    public ICollection<Olympic> Olympics { get; set; }

    public ICollection<Player> Players { get; set; }
}