using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModel
{
    public class DeleteVM
    {
        [Required]
        public int Id { get; set; }
    }
}
