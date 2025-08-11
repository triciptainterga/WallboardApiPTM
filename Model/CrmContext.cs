using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WEBAPI_Bravo.Model
{

    public partial class CrmContext : DbContext
    {
        public CrmContext()
        {

        }
      



        public CrmContext(DbContextOptions<CrmContext> options)
            : base(options)
        {
        }
    }
}
