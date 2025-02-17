using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MSkill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Channel1 { get; set; }
        public byte Channel2 { get; set; }
        public byte Channel3 { get; set; }
        public byte Channel4 { get; set; }
        public byte Channel5 { get; set; }
        public byte Channel6 { get; set; }
        public byte Channel7 { get; set; }
        public byte Channel8 { get; set; }
        public byte Channel9 { get; set; }
        public byte Channel10 { get; set; }
        public byte Channel11 { get; set; }
        public byte Channel12 { get; set; }
        public byte Channel13 { get; set; }
        public byte Channel14 { get; set; }
        public byte Channel15 { get; set; }
        public byte Channel16 { get; set; }
        public byte Channel17 { get; set; }
        public byte Channel18 { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte Channel19 { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
    }
}
