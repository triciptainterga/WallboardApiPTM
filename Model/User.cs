using System;
using System.Collections.Generic;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class User
    {
        public User()
        {
            FaqCategoryCreatedByNavigations = new HashSet<FaqCategory>();
            FaqCategoryUpdatedByNavigations = new HashSet<FaqCategory>();
            FaqCreatedByNavigations = new HashSet<Faq>();
            FaqUpdatedByNavigations = new HashSet<Faq>();
            FollowUpChats = new HashSet<FollowUpChat>();
            FollowUpEmailAttachments = new HashSet<FollowUpEmailAttachment>();
            FollowUpEmailMessages = new HashSet<FollowUpEmailMessage>();
            FollowUpEmails = new HashSet<FollowUpEmail>();
            FollowUps = new HashSet<FollowUp>();
            InteractionEmailAttachments = new HashSet<InteractionEmailAttachment>();
            InteractionEmailMessages = new HashSet<InteractionEmailMessage>();
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
            InteractionManualCreatedByNavigations = new HashSet<InteractionManual>();
            InteractionManualUpdatedByNavigations = new HashSet<InteractionManual>();
            InteractionVideoCalls = new HashSet<InteractionVideoCall>();
            InteractionVideocallLinkCreatedByNavigations = new HashSet<InteractionVideocallLink>();
            InteractionVideocallLinkUpdatedByNavigations = new HashSet<InteractionVideocallLink>();
            InteractionVoices = new HashSet<InteractionVoice>();
            InverseCreatedByNavigation = new HashSet<User>();
            InverseSupervisor = new HashSet<User>();
            InverseUpdatedByNavigation = new HashSet<User>();
            Journeys = new HashSet<Journey>();
            MAdditionalFieldCustomerCreatedByNavigations = new HashSet<MAdditionalFieldCustomer>();
            MAdditionalFieldCustomerUpdatedByNavigations = new HashSet<MAdditionalFieldCustomer>();
            MAdditionalFieldTicketCreatedByNavigations = new HashSet<MAdditionalFieldTicket>();
            MAdditionalFieldTicketUpdatedByNavigations = new HashSet<MAdditionalFieldTicket>();
            MAdditionalOptionFieldCreatedByNavigations = new HashSet<MAdditionalOptionField>();
            MAdditionalOptionFieldUpdatedByNavigations = new HashSet<MAdditionalOptionField>();
            MAuxCreatedByNavigations = new HashSet<MAux>();
            MAuxUpdatedByNavigations = new HashSet<MAux>();
            MCategoryCreatedByNavigations = new HashSet<MCategory>();
            MCategoryMainCreatedByNavigations = new HashSet<MCategoryMain>();
            MCategoryMainUpdatedByNavigations = new HashSet<MCategoryMain>();
            MCategoryUpdatedByNavigations = new HashSet<MCategory>();
            MChannelCreatedByNavigations = new HashSet<MChannel>();
            MChannelUpdatedByNavigations = new HashSet<MChannel>();
            MCustomerCreatedByNavigations = new HashSet<MCustomer>();
            MCustomerUpdatedByNavigations = new HashSet<MCustomer>();
            MGroupCreatedByNavigations = new HashSet<MGroup>();
            MGroupUpdatedByNavigations = new HashSet<MGroup>();
            MSkillCreatedByNavigations = new HashSet<MSkill>();
            MSkillUpdatedByNavigations = new HashSet<MSkill>();
            MSourceCreatedByNavigations = new HashSet<MSource>();
            MSourceUpdatedByNavigations = new HashSet<MSource>();
            MTicketPriorityCreatedByNavigations = new HashSet<MTicketPriority>();
            MTicketPriorityUpdatedByNavigations = new HashSet<MTicketPriority>();
            MTicketStatusCreatedByNavigations = new HashSet<MTicketStatus>();
            MTicketStatusUpdatedByNavigations = new HashSet<MTicketStatus>();
            MUnitCreatedByNavigations = new HashSet<MUnit>();
            MUnitUpdatedByNavigations = new HashSet<MUnit>();
            NewsCreatedByNavigations = new HashSet<News>();
            NewsUpdatedByNavigations = new HashSet<News>();
            TicketCreatedByNavigations = new HashSet<Ticket>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketPickupByNavigations = new HashSet<Ticket>();
            TicketUpdatedByNavigations = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Level { get; set; }
        public int UnitId { get; set; }
        public int? SupervisorId { get; set; }
        public string Fullname { get; set; }
        public string Nickname { get; set; }
        public string Phone { get; set; }
        public string UserPbx { get; set; }
        public string PwdPbx { get; set; }
        public string PabxHost { get; set; }
        public string UserPbxSales { get; set; }
        public string PwdPbxSales { get; set; }
        public string PabxHostSales { get; set; }
        public string ProfilePic { get; set; }
        public int FailLogin { get; set; }
        public byte IsInternal { get; set; }
        public byte IsService { get; set; }
        public byte IsSales { get; set; }
        public byte IsMarketer { get; set; }
        public byte IsActive { get; set; }
        public byte IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string GeneratedKey { get; set; }
        public string Username { get; set; }
        public byte Is2fa { get; set; }
        public string GeneratedAuth { get; set; }
        public string Provinsi { get; set; }
        public string Kota { get; set; }
        public string Jabatan { get; set; }
        public string JobGroup { get; set; }
        public string Area { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User Supervisor { get; set; }
        public virtual MUnit Unit { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<FaqCategory> FaqCategoryCreatedByNavigations { get; set; }
        public virtual ICollection<FaqCategory> FaqCategoryUpdatedByNavigations { get; set; }
        public virtual ICollection<Faq> FaqCreatedByNavigations { get; set; }
        public virtual ICollection<Faq> FaqUpdatedByNavigations { get; set; }
        public virtual ICollection<FollowUpChat> FollowUpChats { get; set; }
        public virtual ICollection<FollowUpEmailAttachment> FollowUpEmailAttachments { get; set; }
        public virtual ICollection<FollowUpEmailMessage> FollowUpEmailMessages { get; set; }
        public virtual ICollection<FollowUpEmail> FollowUpEmails { get; set; }
        public virtual ICollection<FollowUp> FollowUps { get; set; }
        public virtual ICollection<InteractionEmailAttachment> InteractionEmailAttachments { get; set; }
        public virtual ICollection<InteractionEmailMessage> InteractionEmailMessages { get; set; }
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
        public virtual ICollection<InteractionManual> InteractionManualCreatedByNavigations { get; set; }
        public virtual ICollection<InteractionManual> InteractionManualUpdatedByNavigations { get; set; }
        public virtual ICollection<InteractionVideoCall> InteractionVideoCalls { get; set; }
        public virtual ICollection<InteractionVideocallLink> InteractionVideocallLinkCreatedByNavigations { get; set; }
        public virtual ICollection<InteractionVideocallLink> InteractionVideocallLinkUpdatedByNavigations { get; set; }
        public virtual ICollection<InteractionVoice> InteractionVoices { get; set; }
        public virtual ICollection<User> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<User> InverseSupervisor { get; set; }
        public virtual ICollection<User> InverseUpdatedByNavigation { get; set; }
        public virtual ICollection<Journey> Journeys { get; set; }
        public virtual ICollection<MAdditionalFieldCustomer> MAdditionalFieldCustomerCreatedByNavigations { get; set; }
        public virtual ICollection<MAdditionalFieldCustomer> MAdditionalFieldCustomerUpdatedByNavigations { get; set; }
        public virtual ICollection<MAdditionalFieldTicket> MAdditionalFieldTicketCreatedByNavigations { get; set; }
        public virtual ICollection<MAdditionalFieldTicket> MAdditionalFieldTicketUpdatedByNavigations { get; set; }
        public virtual ICollection<MAdditionalOptionField> MAdditionalOptionFieldCreatedByNavigations { get; set; }
        public virtual ICollection<MAdditionalOptionField> MAdditionalOptionFieldUpdatedByNavigations { get; set; }
        public virtual ICollection<MAux> MAuxCreatedByNavigations { get; set; }
        public virtual ICollection<MAux> MAuxUpdatedByNavigations { get; set; }
        public virtual ICollection<MCategory> MCategoryCreatedByNavigations { get; set; }
        public virtual ICollection<MCategoryMain> MCategoryMainCreatedByNavigations { get; set; }
        public virtual ICollection<MCategoryMain> MCategoryMainUpdatedByNavigations { get; set; }
        public virtual ICollection<MCategory> MCategoryUpdatedByNavigations { get; set; }
        public virtual ICollection<MChannel> MChannelCreatedByNavigations { get; set; }
        public virtual ICollection<MChannel> MChannelUpdatedByNavigations { get; set; }
        public virtual ICollection<MCustomer> MCustomerCreatedByNavigations { get; set; }
        public virtual ICollection<MCustomer> MCustomerUpdatedByNavigations { get; set; }
        public virtual ICollection<MGroup> MGroupCreatedByNavigations { get; set; }
        public virtual ICollection<MGroup> MGroupUpdatedByNavigations { get; set; }
        public virtual ICollection<MSkill> MSkillCreatedByNavigations { get; set; }
        public virtual ICollection<MSkill> MSkillUpdatedByNavigations { get; set; }
        public virtual ICollection<MSource> MSourceCreatedByNavigations { get; set; }
        public virtual ICollection<MSource> MSourceUpdatedByNavigations { get; set; }
        public virtual ICollection<MTicketPriority> MTicketPriorityCreatedByNavigations { get; set; }
        public virtual ICollection<MTicketPriority> MTicketPriorityUpdatedByNavigations { get; set; }
        public virtual ICollection<MTicketStatus> MTicketStatusCreatedByNavigations { get; set; }
        public virtual ICollection<MTicketStatus> MTicketStatusUpdatedByNavigations { get; set; }
        public virtual ICollection<MUnit> MUnitCreatedByNavigations { get; set; }
        public virtual ICollection<MUnit> MUnitUpdatedByNavigations { get; set; }
        public virtual ICollection<News> NewsCreatedByNavigations { get; set; }
        public virtual ICollection<News> NewsUpdatedByNavigations { get; set; }
        public virtual ICollection<Ticket> TicketCreatedByNavigations { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<Ticket> TicketPickupByNavigations { get; set; }
        public virtual ICollection<Ticket> TicketUpdatedByNavigations { get; set; }
    }
}
