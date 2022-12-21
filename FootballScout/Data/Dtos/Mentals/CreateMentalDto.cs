using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Mentals
{
    public record CreateMentalDto([Required] int Aggression, [Required] int Anticipation, [Required] int Bravery,
        [Required] int Composure, [Required] int Concentration,[Required] int Decisions, [Required] int Determination,
        [Required] int Flair, [Required] int Leadership, [Required] int OffTheBall, [Required] int Positioning, 
        [Required] int Teamwork, [Required] int Vision,[Required] int WorkRate);
}