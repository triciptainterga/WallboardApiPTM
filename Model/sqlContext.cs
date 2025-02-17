using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WEBAPI_Bravo.Model
{

    public partial class sqlContext : DbContext
    {
        public sqlContext()
        {
        }
      

       

        public sqlContext(DbContextOptions<sqlContext> options)
            : base(options)
        {
        }
    }
}
