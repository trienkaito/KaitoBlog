using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FA.JustBlog.HelperCustom
{
    [HtmlTargetElement("friendly-date")]
    public class FriendlyDateTagHelper : TagHelper
    {
        public DateTime Date { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";  // Replaces <friendly-date> with <span>
            output.Content.SetContent(FormatDate(Date));
        }

        private string FormatDate(DateTime date)
        {
            var timeSpan = DateTime.Now.Subtract(date);
            if (timeSpan <= TimeSpan.FromSeconds(60))
                return $"{timeSpan.Seconds} seconds ago";
            else if (timeSpan <= TimeSpan.FromMinutes(60))
                return $"{timeSpan.Minutes} minutes ago";
            else if (timeSpan <= TimeSpan.FromHours(24))
                return $"{timeSpan.Hours} hours ago";
            else if (timeSpan <= TimeSpan.FromDays(1))
                return "yesterday";
            else if (timeSpan <= TimeSpan.FromDays(7))
                return $"{timeSpan.Days} days ago";
            else
                return date.ToString("MMMM dd, yyyy 'at' hh:mm tt");
        }
    }
}
