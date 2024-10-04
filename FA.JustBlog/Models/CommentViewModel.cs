using FA.JustBlog.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
        }

        public CommentViewModel(int id, string name, string email, int postId, string commentHeader, string commentText, DateTime commentTime)
        {
            Id = id;
            Name = name;
            Email = email;
            PostId = postId;
            CommentHeader = commentHeader;
            CommentText = commentText;
            CommentTime = commentTime;
        }
        

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string Email { get; set; }
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]

        [StringLength(255)]
        [Required(ErrorMessage = "Comment header is required.")]
        [Display(Name = "Title")]
        public string CommentHeader { get; set; }

        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        public DateTime CommentTime { get; set; }
    }
}
