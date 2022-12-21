using System.ComponentModel.DataAnnotations;

namespace FootballScout.Data.Dtos.Technicals
{
    public record CreateTechnicalDto([Required]  int Corners, [Required] int Crossing, [Required] int Dribbling,
        [Required] int Finishing, [Required] int FirstTouch, [Required] int FreeKickTaking, [Required] int Heading, 
        [Required] int LongShots, [Required] int LongThrows, [Required] int Marking, [Required] int Passing, 
        [Required] int PenaltyTaking, [Required] int Tackling, [Required] int Technique);
}