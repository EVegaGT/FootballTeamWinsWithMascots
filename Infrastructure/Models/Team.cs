using System.ComponentModel.DataAnnotations;

namespace FootballTeamWinsWithMascots.Infrastructure.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Rank { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public string Mascot { get; set; }
        public DateTime? DateOfLastWin { get; set; }
        public decimal WinsPercentage { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public int Games { get; set; }
    }
}
