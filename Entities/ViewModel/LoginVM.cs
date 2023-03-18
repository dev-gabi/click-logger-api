
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModel
{
    public class LoginVM
    {
        [Required]
        [EmailAddress, MinLength(4, ErrorMessage ="Email should be at least 4 characthers")]
        [MaxLength(100, ErrorMessage = "Email shold not exceed 100 characters")]
        public string Email { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Password shold not exceed 20 characters")]
        [MinLength(4, ErrorMessage = "Password should be at least 4 characthers")]
        public string Password { get; set; }

        [Required]
        public int TimeOnPageInSeconds { get; set; }
    }
}
