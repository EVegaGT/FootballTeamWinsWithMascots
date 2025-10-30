namespace FootballTeamWinsWithMascots.Application.Dtos
{
    public record TeamDto (int Id, 
        int Rank, 
        string Name, 
        string Mascot, 
        DateTime? DateOfLastWin, 
        decimal WinsPercentage, 
        int Wins, 
        int Losses, 
        int Ties, 
        int Games);
}
