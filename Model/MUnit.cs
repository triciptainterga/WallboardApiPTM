using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MUnit
    {
        public MUnit()
        {
            MCategories = new HashSet<MCategory>();
            TicketHistoryFromUnits = new HashSet<TicketHistory>();
            TicketHistoryUnits = new HashSet<TicketHistory>();
            Tickets = new HashSet<Ticket>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Layer { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? IdOn4 { get; set; }
        public int? MorId { get; set; }
        public string MorName { get; set; }
        public string Regional { get; set; }
        public string Provinsi { get; set; }
        public string Kabkota { get; set; }
        public string Sbm { get; set; }
        public string NoPekerja { get; set; }
        public string SbmName { get; set; }
        public string SbmHp { get; set; }
        public string SbmEmail { get; set; }
        public string Sam { get; set; }
        public string SamName { get; set; }
        public string SamHp { get; set; }
        public string SamEmail { get; set; }
        public string ManregName { get; set; }
        public string ManregHp { get; set; }
        public string ManregEmail { get; set; }
        public string CcEmailRegional { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FunctionName { get; set; }
        public string Area { get; set; }
        public string Hp { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<MCategory> MCategories { get; set; }
        public virtual ICollection<TicketHistory> TicketHistoryFromUnits { get; set; }
        public virtual ICollection<TicketHistory> TicketHistoryUnits { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
