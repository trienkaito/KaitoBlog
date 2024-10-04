
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Areas.Admin.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
