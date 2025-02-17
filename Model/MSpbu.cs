using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MSpbu
    {
        public int Id { get; set; }
        public string NoSpbu { get; set; }
        public string JenisSpbu { get; set; }
        public string ShipToParty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Regional { get; set; }
        public string Provinsi { get; set; }
        public string Kabkota { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
    }
}
