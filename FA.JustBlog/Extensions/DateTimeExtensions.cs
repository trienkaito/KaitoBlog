namespace FA.JustBlog.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyDate(this DateTime date)
        {
            var now = DateTime.Now;
            var today = now.Date;
            var yesterday = today.AddDays(-1);

            var timeSpan = now.Subtract(date);
            if (timeSpan <= TimeSpan.FromSeconds(60))
                return "just now";
            else if (timeSpan < TimeSpan.FromMinutes(60))
                return $"{timeSpan.Minutes} minutes ago";
            else if (timeSpan < TimeSpan.FromHours(24) && date.Date == today)
                return $"today at {date:hh:mm tt}";
            else if (date.Date == yesterday)
                return $"yesterday at {date:hh:mm tt}";
            else
                return date.ToString("dd/MM/yyyy 'at' hh:mm tt");
        }
    }
}
