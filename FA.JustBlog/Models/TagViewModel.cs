using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models
{
    public class TagViewModel
    {
        public TagViewModel(string name)
        {
            Name = name;
        }

        public TagViewModel(int id, string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tag name is required.")]
        [StringLength(255)]
        public  string Name { get; set; }

        public int Count { get; set; }

        [StringLength(255)]
        public  string UrlSlug { get; set; }

        [StringLength(1024)]
        public  string Description { get; set; }
    }
}
