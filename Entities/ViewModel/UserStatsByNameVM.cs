using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class UserStatsByNameVM
    {
        [Required]
        [MinLength(2, ErrorMessage ="Name should be at least 2 characters")]
        public string Name { get; set; }
    }
}
