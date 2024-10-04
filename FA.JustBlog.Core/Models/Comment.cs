using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class Comment
    {
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
        public Post Post { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Comment header is required.")]
        [Display(Name = "Title")]
        public string CommentHeader { get; set; }

        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        public DateTime CommentTime { get; set; }
    }
}
