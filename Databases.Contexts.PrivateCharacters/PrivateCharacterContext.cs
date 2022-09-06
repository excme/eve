using eveDirect.Databases.Contexts.PrivateCharacters.Models;

using Microsoft.EntityFrameworkCore;

namespace eveDirect.Databases.Contexts
{
    public class PrivateCharacterContext: PrivateCharCorpContext
    {
        // Calendar
        public DbSet<EveOnlineCharacterCalendarItem> Eveonline_CharacterCalendars { get; set; }
        public DbSet<EveOnlineCharacterCalendarItemAccess> Eveonline_CharacterCalendarItemAccesses { get; set; }
        public DbSet<EveOnlineCharacterCalendarItemAttendee> Eveonline_CharacterCalendarItemAttendes { get; set; }

        //Character
        public DbSet<EveOnlineCharacterAgentResearch> Eveonline_CharacterAgentResearches { get; set; }
        public DbSet<EveOnlineCharacterFatigue> Eveonline_CharacterFatigues { get; set; }
        public DbSet<EveOnlineCharacterMedal> Eveonline_CharacterMedals { get; set; }
        public DbSet<EveOnlineCharacterNotification> Eveonline_CharacterNotifications { get; set; }
        public DbSet<EveOnlineCharacterNotificationContact> Eveonline_CharacterNotificationsContacts { get; set; }
        public DbSet<EveOnlineCharacterRole> Eveonline_CharacterRoles { get; set; }
        public DbSet<EveOnlineStanding> Eveonline_Standings { get; set; }
        public DbSet<EveOnlineCharacterStatsItem> Eveonline_CharacterStats { get; set; }
        public DbSet<EveOnlineCharacterTitle> EveOnline_CharacterTitles { get; set; }

        // Clones
        public DbSet<EveOnlineCharacterClone> Eveonline_CharacterClones { get; set; }
        public DbSet<EveOnlineCharacterCloneImplant> Eveonline_CharacterCloneImplants { get; set; }

        // Fittings
        public DbSet<EveOnlineCharacterFitting> Eveonline_CharacterFittings { get; set; }

        // Fleets
        public DbSet<EveOnlineChararcterFleet> EveOnline_ChararcterFleets { get; set; }
        public DbSet<EveOnlineChararcterFleetMember> EveOnline_ChararcterFleetMembers { get; set; }
        public DbSet<EveOnlineChararcterFleetWing> EveOnline_ChararcterFleetWings { get; set; }

        // Industry
        public DbSet<EveOnlineCharacterIndustryMining> Eveonline_CharacterIndustryMinings { get; set; }

        // Location
        public DbSet<EveOnlineCharacterLocation> Eveonline_CharacterLocations { get; set; }
        public DbSet<EveOnlineCharacterLocationShip> Eveonline_CharacterLocationShips { get; set; }
        public DbSet<EveOnlineCharacterLocationOnline> Eveonline_CharacterLocationOnlines { get; set; }

        // Loyalty
        public DbSet<EveOnlineCharacterLoyalty> Eveonline_CharacterLoyalties { get; set; }

        // Mail
        public DbSet<EveOnlineMail> Eveonline_Mails { get; set; }
        public DbSet<EveOnlineMailRecipient> Eveonline_MailRecipients { get; set; }

        // Opportunities
        public DbSet<EveOnlineCharacterOpportunity> Eveonline_CharacterOpportunities { get; set; }

        // Planetary Interaction
        public DbSet<EveOnlineCharacterPlanet> Eveonline_CharacterPlanets { get; set; }
        public DbSet<EveOnlineCharacterPlanet.Link> Eveonline_CharacterPlanetLinks { get; set; }
        public DbSet<EveOnlineCharacterPlanet.Pin> Eveonline_CharacterPlanetPins { get; set; }
        public DbSet<EveOnlineCharacterPlanet.Route> Eveonline_CharacterPlanetRoutes { get; set; }

        // Skills
        public DbSet<EveOnlineCharacterSkill> Eveonline_CharacterSkills { get; set; }
        public DbSet<EveOnlineCharacterSkillReport> Eveonline_CharacterSkillReports { get; set; }
        public DbSet<EveOnlineCharacterSkillQueue> Eveonline_CharacterSkillQueues { get; set; }

        // Wallet
        public DbSet<EveOnlineCharacterWalletsJournalItem> Eveonline_CharacterWalletsJournals { get; set; }
        public DbSet<EveOnlineCharacterWalletsTransactionItem> Eveonline_CharacterWalletsTransactions { get; set; }
    }
}
