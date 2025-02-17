using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MChannel
    {
        public MChannel()
        {
            FollowUps = new HashSet<FollowUp>();
            InteractionHeaderHistories = new HashSet<InteractionHeaderHistory>();
            InteractionHeaderTakeouts = new HashSet<InteractionHeaderTakeout>();
            InteractionHeaders = new HashSet<InteractionHeader>();
            Journeys = new HashSet<Journey>();
            LogInteractionOutOfSchedules = new HashSet<LogInteractionOutOfSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Group { get; set; }
        public string LinkImage { get; set; }
        public string ComponentBody { get; set; }
        public string ComponentBodyHistory { get; set; }
        public int? Order { get; set; }
        public byte IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte IsHoldOutOfSchedule { get; set; }
        public int? Sla { get; set; }
        public string ComponentBodyV2 { get; set; }
        public string ComponentBodyHistoryV2 { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<FollowUp> FollowUps { get; set; }
        public virtual ICollection<InteractionHeaderHistory> InteractionHeaderHistories { get; set; }
        public virtual ICollection<InteractionHeaderTakeout> InteractionHeaderTakeouts { get; set; }
        public virtual ICollection<InteractionHeader> InteractionHeaders { get; set; }
        public virtual ICollection<Journey> Journeys { get; set; }
        public virtual ICollection<LogInteractionOutOfSchedule> LogInteractionOutOfSchedules { get; set; }
    }
}
