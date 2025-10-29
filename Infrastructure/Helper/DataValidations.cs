namespace FootballTeamWinsWithMascots.Infrastructure.Helper
{
    public static class DataValidations
    {
        public static bool IsValidDate(string dateString)
        {
            DateTime parsedDate;
            return DateTime.TryParse(dateString, out parsedDate);
        }

        public static int ParseIntOrDefault(string value)
        {
            int parsedInt;
            return int.TryParse(value, out parsedInt) ? parsedInt : 0;
        }

        public static decimal ParseDecimalOrDefault(string value)
        {
            return decimal.TryParse(value, out var result) ? result : 0m;
        }
    }
}
