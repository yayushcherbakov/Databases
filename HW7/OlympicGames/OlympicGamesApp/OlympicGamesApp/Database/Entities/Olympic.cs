using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OlympicGamesApp.Database.Entities;

[Table("Olympics")]
public class Olympic
{
    [Key]
    [Column("olympic_id"), Required]
    [MaxLength(7)]
    public string OlympicId { get; set; }

    [ForeignKey(nameof(Country)), Required]
    [Column("country_id")]
    [MaxLength(3)]
    public string CountryId { get; set; }

    public Country Country { get; set; }

    [Column("city"), Required]
    [MaxLength(50)]
    public string City { get; set; }

    [Column("year"), Required]
    public int Year { get; set; }

    [Column("startdate"), Required]
    public DateTime StartDate { get; set; }

    [Column("enddate"), Required]
    public DateTime EndDate { get; set; }

    public ICollection<Event> Events { get; set; }
}