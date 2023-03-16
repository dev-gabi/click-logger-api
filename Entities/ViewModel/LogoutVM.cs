using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModel
{
    public class LogoutVM
    {
        [Required]
        public int LoginUserStatsId { get; set; }
    }
}
