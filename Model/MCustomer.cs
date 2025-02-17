using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class MCustomer
    {
        public MCustomer()
        {
            FollowUpChats = new HashSet<FollowUpChat>();
            FollowUps = new HashSet<FollowUp>();
            InteractionEmails = new HashSet<InteractionEmail>();
            InteractionFacebookComments = new HashSet<InteractionFacebookComment>();
            InteractionFacebookMessageHistories = new HashSet<InteractionFacebookMessageHistory>();
            InteractionFacebookMessages = new HashSet<InteractionFacebookMessage>();
            InteractionHeaderHistories = new HashSet<InteractionHeaderHistory>();
            InteractionHeaderTakeouts = new HashSet<InteractionHeaderTakeout>();
            InteractionHeaders = new HashSet<InteractionHeader>();
            InteractionInstagramComments = new HashSet<InteractionInstagramComment>();
            InteractionInstagramMessageHistories = new HashSet<InteractionInstagramMessageHistory>();
            InteractionInstagramMessages = new HashSet<InteractionInstagramMessage>();
            InteractionManuals = new HashSet<InteractionManual>();
            InteractionVideoCalls = new HashSet<InteractionVideoCall>();
            InteractionVoices = new HashSet<InteractionVoice>();
            Journeys = new HashSet<Journey>();
            MCustomerContactCustIdOldNavigations = new HashSet<MCustomerContact>();
            MCustomerContactCusts = new HashSet<MCustomerContact>();
            MCustomerSuggestionMergeCustIdSuggestionNavigations = new HashSet<MCustomerSuggestionMerge>();
            MCustomerSuggestionMergeCusts = new HashSet<MCustomerSuggestionMerge>();
            TicketCusts = new HashSet<Ticket>();
            TicketInformants = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Hp { get; set; }
        public string Email { get; set; }
        public string TelegramId { get; set; }
        public string TelegramUsername { get; set; }
        public string TelegramName { get; set; }
        public string TwitterId { get; set; }
        public string TwitterUsername { get; set; }
        public int? TwitterFollowers { get; set; }
        public int? TwitterFollowing { get; set; }
        public string TwitterPicture { get; set; }
        public string TwitterName { get; set; }
        public string FacebookId { get; set; }
        public string FacebookName { get; set; }
        public string FacebookPicture { get; set; }
        public string InstagramId { get; set; }
        public string InstagramId2 { get; set; }
        public string Application { get; set; }
        public string InstagramName { get; set; }
        public string InstagramPicture { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string LineId { get; set; }
        public string Gender { get; set; }
        public int? IdOn4 { get; set; }
        public string Other { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual AdditionalFieldCustomer AdditionalFieldCustomer { get; set; }
        public virtual ICollection<FollowUpChat> FollowUpChats { get; set; }
        public virtual ICollection<FollowUp> FollowUps { get; set; }
        public virtual ICollection<InteractionEmail> InteractionEmails { get; set; }
        public virtual ICollection<InteractionFacebookComment> InteractionFacebookComments { get; set; }
        public virtual ICollection<InteractionFacebookMessageHistory> InteractionFacebookMessageHistories { get; set; }
        public virtual ICollection<InteractionFacebookMessage> InteractionFacebookMessages { get; set; }
        public virtual ICollection<InteractionHeaderHistory> InteractionHeaderHistories { get; set; }
        public virtual ICollection<InteractionHeaderTakeout> InteractionHeaderTakeouts { get; set; }
        public virtual ICollection<InteractionHeader> InteractionHeaders { get; set; }
        public virtual ICollection<InteractionInstagramComment> InteractionInstagramComments { get; set; }
        public virtual ICollection<InteractionInstagramMessageHistory> InteractionInstagramMessageHistories { get; set; }
        public virtual ICollection<InteractionInstagramMessage> InteractionInstagramMessages { get; set; }
        public virtual ICollection<InteractionManual> InteractionManuals { get; set; }
        public virtual ICollection<InteractionVideoCall> InteractionVideoCalls { get; set; }
        public virtual ICollection<InteractionVoice> InteractionVoices { get; set; }
        public virtual ICollection<Journey> Journeys { get; set; }
        public virtual ICollection<MCustomerContact> MCustomerContactCustIdOldNavigations { get; set; }
        public virtual ICollection<MCustomerContact> MCustomerContactCusts { get; set; }
        public virtual ICollection<MCustomerSuggestionMerge> MCustomerSuggestionMergeCustIdSuggestionNavigations { get; set; }
        public virtual ICollection<MCustomerSuggestionMerge> MCustomerSuggestionMergeCusts { get; set; }
        public virtual ICollection<Ticket> TicketCusts { get; set; }
        public virtual ICollection<Ticket> TicketInformants { get; set; }
    }
}
