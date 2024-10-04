using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
