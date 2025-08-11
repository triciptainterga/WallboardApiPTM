using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Bravo.Model
{
         public partial class WallboardCard
    
      {
        public int Id { get; set; }
        public string Files { get; set; }
        public string Title { get; set; }
        public string Skill { get; set; }
        public string Channel { get; set; }
        public string Users { get; set; }
    }

}
