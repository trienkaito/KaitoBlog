using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Areas.Admin.Models
{
    public class PostAddModel
    {
        public PostAddModel()
        {

        }

        public PostAddModel(int id, string title, string shortDescription, string postContent, string meta, string urlSlug, bool published, DateTime postedOn, DateTime? modified, int categoryId, int? viewCount, int? rateCount, int? totalRate, string tagValues)
        {
            Id = id;
            Title = title;
            ShortDescription = shortDescription;
            PostContent = postContent;
            Meta = meta;
            UrlSlug = urlSlug;
            Published = published;
            PostedOn = postedOn;
            Modified = modified;
            CategoryId = categoryId;
            ViewCount = viewCount;
            RateCount = rateCount;
            TotalRate = totalRate;
            TagValues = tagValues;
        }

        public PostAddModel(int id, string title, string shortDescription, string postContent, string meta, string urlSlug, bool published, DateTime postedOn, DateTime? modified, int? viewCount, int? rateCount, int? totalRate, string tagValues, int categoryId)
        {
            Id = id;
            Title = title;
            ShortDescription = shortDescription;
            PostContent = postContent;
            Meta = meta;
            UrlSlug = urlSlug;
            Published = published;
            PostedOn = postedOn;
            Modified = modified;
            ViewCount = viewCount;
            RateCount = rateCount;
            TotalRate = totalRate;
            TagValues = tagValues;
            CategoryId = categoryId;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title name is required.")]
        [StringLength(255)]
        [Display(Name = "Post title")]
        public string Title { get; set; }

        [Column("Short Description")]
        [Required(ErrorMessage = "Short Description is required.")]
        [StringLength(1024)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Content")]
        public string PostContent { get; set; }

        [Display(Name = "Meta")]
        public string Meta { get; set; }

        [StringLength(255)]
        [Display(Name = "Url")]
        public string UrlSlug { get; set; }

        [Display(Name = "Is Published")]
        public bool Published { get; set; }

        [Column("Posted On")]
        [Display(Name = "Posted On")]
        public DateTime PostedOn { get; set; }
        public DateTime? Modified { get; set; }
        public int CategoryId { get; set; }
        public int? ViewCount { get; set; }

        [NotMapped]
        public decimal Rate
        {
            get
            {
                if (RateCount == null || RateCount == 0)
                {
                    return 0;
                }

                return Math.Round((decimal)TotalRate.Value / RateCount.Value, 2);
            }
        }
        public int? RateCount { get; set; }
        public int? TotalRate { get; set; }    
        public string TagValues { get; set; }
    }
}
