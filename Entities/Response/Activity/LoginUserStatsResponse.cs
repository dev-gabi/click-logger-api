using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Activity
{
   public class LoginUserStatsResponse:ApiResponse
    {
        public IEnumerable<LoginUserStats> Stats { get; set; }
    }
}
