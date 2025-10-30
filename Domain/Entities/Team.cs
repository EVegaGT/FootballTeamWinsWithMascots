using System.ComponentModel.DataAnnotations;
namespace FootballTeamWinsWithMascots.Domain.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Mascot { get; set; }
        public DateTime? DateOfLastWin { get; set; }
        public decimal WinsPercentage { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public int Games { get; set; }
    }
}
