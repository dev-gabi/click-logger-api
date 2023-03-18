using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DbEntities
{
    public class PageStatsWithUserName:LoginPageStats
    {
        public string UserName { get; set; }
    }
}
