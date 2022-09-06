using eveDirect.Databases.Contexts.PrivateCorporation.Models;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Databases.Contexts
{
    public class PrivateCorporationContext : PrivateCharCorpContext
    {
        // Corporation
        public DbSet<EveOnlineCorporationContainersLog> Eveonline_CorporationContainersLogs { get; set; }
        public DbSet<EveOnlineCorporationDivision> Eveonline_CorporationDivisions { get; set; }
        public DbSet<EveOnlineCorporationFacility> Eveonline_CorporationFacilities { get; set; }
        public DbSet<EveOnlineCorporationMedal> Eveonline_CorporationMedals { get; set; }
        public DbSet<EveOnlineCorporationMedalIssued> Eveonline_CorporationMedalIssueds { get; set; }
        public DbSet<EveOnlineCorporationMember> EveOnline_CorporationMembers { get; set; }
        public DbSet<EveOnlineCorporationMemberTrackingItem> Eveonline_CorporationMemberTrackingItems { get; set; }
        public DbSet<EveOnlineCorporationMemberRoles> Eveonline_CorporationMemberRoles { get; set; }
        public DbSet<EveOnlineCorporationStructure> Eveonline_CorporationStructures { get; set; }
        public DbSet<EveOnlineCorporationStarbase> Eveonline_CorporationStarbases { get; set; }
        public DbSet<EveOnlineCorporationShareholder> Eveonline_CorporationShareholders { get; set; }

        // Industry
        public DbSet<EveOnlineCorporationIndustryMinigExtraction> Eveonline_CorporationIndustryMinigExtractions { get; set; }
        public DbSet<EveOnlineCorporationIndustryMinigObserver> Eveonline_CorporationIndustryMinigObservers { get; set; }
        public DbSet<EveOnlineCorporationIndustryMinigObserverDetail> Eveonline_CorporationIndustryMinigObserverDetails { get; set; }

        // Planetary Interaction
        public DbSet<EveOnlineCorporationCustomsOffice> Eveonline_CorporationCustomsOffices { get; set; }

        // Wallet
        public DbSet<EveOnlineCorporationWalletsJournalItem> Eveonline_CorporationWalletsJournals { get; set; }
        public DbSet<EveOnlineCorporationWalletsTransactionItem> Eveonline_CorporationWalletsTransactions { get; set; }
    }
}
