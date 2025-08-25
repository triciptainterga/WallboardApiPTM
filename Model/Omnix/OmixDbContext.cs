using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Bravo.Model.Omnix
{
    public class OmixDbContext: DbContext
    {
        public OmixDbContext(DbContextOptions<OmixDbContext> options)
            : base(options)
        {
        }

        public DbSet<MCategory> MCategorys { get; set; }
        public DbSet<MCategoryMain> MCategoryMains { get; set; }
        public DbSet<MCustomer> MCustomers { get; set; }
        public DbSet<MCustomerContact> MCustomerContacts { get; set; }
        public DbSet<MCustomerContactDetail> MCustomerContactDetails { get; set; }
        public DbSet<MCustomerSuggestionMerge> MCustomerSuggestionMerges { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistorys { get; set; }
        public DbSet<User> Users { get; set; }
        // Tambahkan DbSet lain sesuai kebutuhan

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API di sini (jika perlu)
        }
    }
}
