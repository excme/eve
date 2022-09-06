using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.Models;
using eveDirect.Shared.ConfigurationHelper;

namespace eveDirect.Databases.Contexts
{
    public class PublicContext : IdentityDbContext<Account, AccountRole, ulong, IdentityUserClaim<ulong>, AccountRoleRef, IdentityUserLogin<ulong>, IdentityRoleClaim<ulong>, IdentityUserToken<ulong>>
    {
        // Alliance
        public virtual DbSet<EveOnlineAlliance> EveOnline_Alliances { get; set; }
        public DbSet<StatisticAllianceCorporationMigration> Statistic_AllianceCorporationMigrations { get; set; }
        public DbSet<StatisticAllianceCharacterMigration> Statistic_AllianceCharacterMigrations { get; set; }

        // Characters
        public DbSet<EveOnlineCharacter> EveOnline_Characters { get; set; }
        public DbSet<EveOnlineCharacterCorpHistory> EveOnline_CharactersCorporationHistory { get; set; }
        public DbSet<EveDirectCharacterAllianceHistory> EveDirect_CharacterAllianceHistory { get; set; }
        public DbSet<EveOnlineCharacterPortrait> EveOnline_CharacterPortraits { get; set; }

        // Corporation
        /// <summary>
        /// EveOnline Корпорации
        /// </summary>
        public DbSet<EveOnlineCorporation> EveOnline_Corporations { get; set; }
        /// <summary>
        /// Миграции корпораций по альянсам
        /// </summary>
        public DbSet<EveOnlineCorporationAllianceHistory> EveOnline_CorporationAllianceHistories { get; set; }
        public DbSet<StatisticCorporationCharacterMigration> Statistic_CorporationCharacterMigrations { get; set; }
        
        // Contracts
        public virtual DbSet<EveOnlineContract> Eveonline_Contracts { get; set; }
        //public virtual DbSet<EveOnlineContractRef> Eveonline_ContractRefs { get; set; }
        public virtual DbSet<EveOnlineContractItem> Eveonline_ContractItems { get; set; }
        public virtual DbSet<EveOnlineContractBid> Eveonline_ContractBids { get; set; }

        // Industry
        public virtual DbSet<EveOnlineIndustryFacility> Eveonline_IndustryFacilities { get; set; }
        public virtual DbSet<EveOnlineIndustrySystem> Eveonline_IndustrySystems { get; set; }

        // Loyalty
        public virtual DbSet<EveOnlineCorporationLoaltyOffer> Eveonline_CorporationLoaltyOffers { get; set; }

        // Market 
        public virtual DbSet<EveOnlineMarketRegionHistoryPriceInfo> Eveonline_MarketHistoryPrices { get; set; }
        public virtual DbSet<EveOnlineMarketOrder> Eveonline_MarketOrders { get; set; }
        public virtual DbSet<EveOnlineMarketGroup> Eveonline_MarketGroups { get; set; }

        // Dogma
        //public DbSet<EveOnlineDogmaAttribute> Eveonline_DogmaAttributes { get; set; }
        //public DbSet<EveOnlineDogmaEffect> Eveonline_DogmaEffects { get; set; }

        // Opportunities
        public virtual DbSet<EveOnlineOpportunityGroup> Eveonline_OpportunityGroups { get; set; }
        public virtual DbSet<EveOnlineOpportunityTask> Eveonline_OpportunityTasks { get; set; }

        // Universe 
        //public virtual DbSet<EveOnlineUniverseRegion> Eveonline_UniverseRegions { get; set; }
        //public virtual DbSet<EveOnlineUniverseConstellation> Eveonline_UniverseConstellations { get; set; }
        //public virtual DbSet<EveOnlineUniverseSystemLocation> Eveonline_UniverseSystemLocations { get; set; }
        //public virtual DbSet<EveOnlineUniverseSystem> Eveonline_UniverseSystems { get; set; }
        //public virtual DbSet<EveOnlineUniversePlanet> Eveonline_UniversePlanets { get; set; }
        //public virtual DbSet<EveOnlineUniverseStargate> Eveonline_UniverseStargates { get; set; }
        //public virtual DbSet<EveOnlineUniverseMoon> Eveonline_UniverseMoons { get; set; }
        //public virtual DbSet<EveOnlineUniverseStar> Eveonline_UniverseStars { get; set; }
        //public virtual DbSet<EveOnlineUniverseStation> Eveonline_UniverseStations { get; set; }
        //public virtual DbSet<EveOnlineUniverseAsteroidBelt> Eveonline_UniverseAsteroidBelts { get; set; }
        
        public virtual DbSet<EveOnlineUniverseLocation> EveOnlineUniverseLocations { get; set; }
        public virtual DbSet<EveOnlineUniverseType> Eveonline_UniverseTypes { get; set; }
        public virtual DbSet<EveOnlineUniverseCategory> Eveonline_UniverseCategories { get; set; }
        public virtual DbSet<EveOnlineUniverseGroup> Eveonline_UniverseGroups { get; set; }

        public virtual DbSet<EveOnlineUniverseAncestry> EveOnlineUniverseAncestries { get; set; }
        public virtual DbSet<EveOnlineUniverseRace> Eveonline_UniverseRaces { get; set; }
        public virtual DbSet<EveOnlineUniverseFaction> Eveonline_UniverseFactions { get; set; }
        public virtual DbSet<EveOnlineUniverseBloodLine> Eveonline_UniverseBloodLines { get; set; }
        //public virtual DbSet<EveOnlineUniverseStructure> Eveonline_UniverseStructures { get; set; }
        public virtual DbSet<EveOnlineUniverseGraphic> Eveonline_UniverseGraphics { get; set; }

        // Warfare
        // Killmail
        public DbSet<EveOnlineKillMail> Eveonline_KillMails { get; set; }
        public DbSet<EveOnlineKillmailActioner> EveOnlineKillMail_Actioners { get; set; }
        public DbSet<EveOnlineKillMailAttacker> EveOnlineKillMail_Attackers { get; set; }
        public DbSet<EveOnlineKillMailVictim> EveOnlineKillMail_Victims { get; set; }

        // External
        public virtual DbSet<ZKillBoardStatItem> zKillBoardsItems { get; set; }
        public virtual DbSet<EveDirectCheckPoint> EveDirectCheckPoints { get; set; }

        // Wars
        public virtual DbSet<EveOnlineWar> Eveonline_Wars { get; set; }
        public virtual DbSet<EveOnlineWarAlly> Eveonline_WarsAllies { get; set; }

        // Identity
        public virtual DbSet<IdentitySso> Ssos { get; set; }
        public virtual DbSet<IdentitySsoRequest> SsoRequests { get; set; }

        // Search
        public DbSet<SearchItem> SearchItems { get; set; }

        /// <summary>
        /// Последние действия персонажа, корпорации или альянса
        /// </summary>
        public DbSet<EveDirectLastAction> LastActions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Типы столбцов в зависимости от провайдера
            var columnTypes = DatabaseColumnTypesSpec.Get(Database.ProviderName);

            // Last Actions
            modelBuilder.Entity<EveDirectLastAction>().ToTable("last_actions");
            modelBuilder.Entity<EveDirectLastAction>().HasIndex(r => r.owner_id);
            modelBuilder.Entity<EveDirectLastAction>().HasIndex(r => r.parent_id);
            modelBuilder.Entity<EveDirectLastAction>().HasIndex(r => r.type);

            // Alliance
            modelBuilder.Entity<EveOnlineAlliance>().ToTable("alliances");
            //modelBuilder.Entity<EveOnlineAlliance>().Property(p => p.date_founded).HasColumnType(columnTypes.onlyDate);
            //modelBuilder.Entity<EveOnlineAlliance>().Property(m => m.membersMigrations).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineAlliance>().Property(m => m.membersMigrations).HasColumnName("comigr");
            modelBuilder.Entity<EveOnlineAlliance>().Property(m => m.preview).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineAlliance>().Property(m => m.preview).HasColumnName("pr");

            modelBuilder.Entity<StatisticAllianceCorporationMigration>().ToTable("statallycorps");
            modelBuilder.Entity<StatisticAllianceCorporationMigration>().HasIndex(p => p.alliance_id);
            modelBuilder.Entity<StatisticAllianceCorporationMigration>().HasIndex(p => p.date);
            modelBuilder.Entity<StatisticAllianceCorporationMigration>().Property(p => p.date).HasColumnType(columnTypes.onlyDate);
            modelBuilder.Entity<StatisticAllianceCorporationMigration>().Property(m => m._in).HasColumnName("in");
            modelBuilder.Entity<StatisticAllianceCorporationMigration>().Property(m => m._out).HasColumnName("out");

            modelBuilder.Entity<StatisticAllianceCharacterMigration>().ToTable("statallymems");
            modelBuilder.Entity<StatisticAllianceCharacterMigration>().HasIndex(p => p.alliance_id);
            modelBuilder.Entity<StatisticAllianceCharacterMigration>().HasIndex(p => p.date);
            modelBuilder.Entity<StatisticAllianceCharacterMigration>().Property(p => p.date).HasColumnType(columnTypes.onlyDate);
            modelBuilder.Entity<StatisticAllianceCharacterMigration>().Property(m => m._in).HasColumnName("in");
            modelBuilder.Entity<StatisticAllianceCharacterMigration>().Property(m => m._out).HasColumnName("out");

            // Characters
            modelBuilder.Entity<EveOnlineCharacter>().ToTable("chars");
            //modelBuilder.Entity<EveOnlineCharacter>().Property(p => p.birthday).HasColumnType(columnTypes.onlyDate);
            //modelBuilder.Entity<EveOnlineCharacter>().Property(m => m.corpHistory).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineCharacter>().Property(m => m.killmails).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineCharacter>().Property(m => m.killmails).HasColumnName("kma");
            modelBuilder.Entity<EveOnlineCharacter>().Property(m => m.preview).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineCharacter>().Property(m => m.preview).HasColumnName("pr");
            modelBuilder.Entity<EveOnlineCharacter>().Property(m => m.corpHistoryCount).HasColumnName("cmc");
            //modelBuilder.Entity<EveOnlineCharacter>().HasIndex(p => p.birthday);
            //modelBuilder.Entity<EveOnlineCharacter>().Property(m => m.anyCorpMigrations).HasColumnName("migr");

            modelBuilder.Entity<EveDirectCharacterAllianceHistory>().ToTable("charsallyhist");
            modelBuilder.Entity<EveDirectCharacterAllianceHistory>().HasIndex(p => p.character_id);
            modelBuilder.Entity<EveDirectCharacterAllianceHistory>().HasIndex(p => p.alliance_id);

            modelBuilder.Entity<EveOnlineCharacterCorpHistory>().ToTable("charshistory");
            modelBuilder.Entity<EveOnlineCharacterCorpHistory>().HasIndex(b => b.character_id);
            modelBuilder.Entity<EveOnlineCharacterCorpHistory>().HasIndex(b => b.just_newborn);
            modelBuilder.Entity<EveOnlineCharacterCorpHistory>().Property(b => b.just_newborn).HasColumnName("nb");
            modelBuilder.Entity<EveOnlineCharacterCorpHistory>().HasIndex(b => b.corporation_id);
            modelBuilder.Entity<EveOnlineCharacterCorpHistory>().HasIndex(b => b.prev_corp_id);
            modelBuilder.Entity<EveOnlineCharacterCorpHistory>().HasIndex(b => b.start_date);
            //modelBuilder.Entity<EveOnlineCharacterCorpHistory>().Ignore(b => b.corporation_id);

            modelBuilder.Entity<EveOnlineCharacterPortrait>().ToTable("chars_portraits");

            // Corporation
            modelBuilder.Entity<EveOnlineCorporation>().ToTable("corps");
            modelBuilder.Entity<EveOnlineCorporation>().Property(m => m.membersMigrations).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineCorporation>().Property(m => m.membersMigrations).HasColumnName("chmigr");
            modelBuilder.Entity<EveOnlineCorporation>().Property(m => m.preview).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineCorporation>().Property(m => m.preview).HasColumnName("pr");
            modelBuilder.Entity<EveOnlineCorporation>().Property(m => m.lastUpdate_allianceHistory).HasColumnName("al_hi");

            modelBuilder.Entity<EveOnlineCorporationAllianceHistory>().ToTable("corpshistory");
            modelBuilder.Entity<EveOnlineCorporationAllianceHistory>().HasIndex(b => b.corporation_id);
            modelBuilder.Entity<EveOnlineCorporationAllianceHistory>().HasIndex(b => b.alliance_id);

            modelBuilder.Entity<StatisticCorporationCharacterMigration>().ToTable("statcorpmems");
            modelBuilder.Entity<StatisticCorporationCharacterMigration>().HasIndex(p => p.date);
            modelBuilder.Entity<StatisticCorporationCharacterMigration>().Property(p => p.date).HasColumnType(columnTypes.onlyDate);
            modelBuilder.Entity<StatisticCorporationCharacterMigration>().HasIndex(p => p.corporation_id);

            // Contract
            modelBuilder.Entity<EveOnlineContract>().ToTable("contrs");
            
            modelBuilder.Entity<EveOnlineContractBid>().ToTable("contrsbids");
            //modelBuilder.Entity<EveOnlineContractBid>().HasIndex(p => p.contract_id);
            modelBuilder.Entity<EveOnlineContractBid>()
            .HasOne<EveOnlineContract>(s => s.contract)
            .WithMany(g => g.bids)
            .HasForeignKey(s => s.contract_id);

            modelBuilder.Entity<EveOnlineContractItem>().ToTable("contrsitems");
            //modelBuilder.Entity<EveOnlineContractItem>().HasIndex(p => p.contract_id);
            modelBuilder.Entity<EveOnlineContractItem>()
            .HasOne<EveOnlineContract>(s => s.contract)
            .WithMany(g => g.items)
            .HasForeignKey(s => s.contract_id);

            // Industry
            modelBuilder.Entity<EveOnlineIndustryFacility>().ToTable("indac");
            modelBuilder.Entity<EveOnlineIndustrySystem>().ToTable("indsys");

            // Loyalty
            modelBuilder.Entity<EveOnlineCorporationLoaltyOffer>().ToTable("corpsloaltyoffers");

            // Market
            modelBuilder.Entity<EveOnlineMarketRegionHistoryPriceInfo>().ToTable("mhps");
            modelBuilder.Entity<EveOnlineMarketRegionHistoryPriceInfo>().HasIndex(p => p.region_id);
            modelBuilder.Entity<EveOnlineMarketRegionHistoryPriceInfo>().HasIndex(p => p.type_id);
            modelBuilder.Entity<EveOnlineMarketRegionHistoryPriceInfo>().Property(p => p.date).HasColumnType(columnTypes.onlyDate);
            modelBuilder.Entity<EveOnlineMarketGroup>().ToTable("mgs");
            modelBuilder.Entity<EveOnlineMarketGroup>().Property(m => m.types).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineMarketGroup>().Property(m => m.childs).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineMarketGroup>().Ignore(m => m.name);
            modelBuilder.Entity<EveOnlineMarketGroup>().Ignore(m => m.description);
            
            modelBuilder.Entity<EveOnlineMarketOrder>().ToTable("mo");
            modelBuilder.Entity<EveOnlineMarketOrder>().HasIndex(x => x.disabled);
            modelBuilder.Entity<EveOnlineMarketOrder>().HasIndex(x => x.region_id);

            // Opportunities
            modelBuilder.Entity<EveOnlineOpportunityGroup>().ToTable("ogs");
            modelBuilder.Entity<EveOnlineOpportunityTask>().ToTable("ots");

            // Universe
            modelBuilder.Entity<EveOnlineUniverseAncestry>().ToTable("uancestries");
            modelBuilder.Entity<EveOnlineUniverseAncestry>().Ignore(x => x.name);
            modelBuilder.Entity<EveOnlineUniverseAncestry>().Ignore(x => x.description);
            modelBuilder.Entity<EveOnlineUniverseAncestry>().Ignore(x => x.short_description);

            modelBuilder.Entity<EveOnlineUniverseLocation>().ToTable("ulocs");
            modelBuilder.Entity<EveOnlineUniverseLocation>().OwnsOne(p => p.position);
            modelBuilder.Entity<EveOnlineUniverseLocation>().HasIndex(p => p.type);
            modelBuilder.Entity<EveOnlineUniverseLocation>().HasIndex(p => p.owner_id);

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.regionInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.regionInfo.region_id);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.regionInfo.name);
            
            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.constellationInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.constellationInfo.constellation_id);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.constellationInfo.name);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.constellationInfo.region_id);
            
            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.systemInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.systemInfo.system_id);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.systemInfo.name);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.systemInfo.constellation_id);

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.planetInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.planetInfo.planet_id);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.planetInfo.name);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.planetInfo.system_id);

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.stargateInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.stargateInfo.stargate_id);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.stargateInfo.name);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.stargateInfo.system_id);

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.moonInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.moonInfo.moon_id);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.moonInfo.name);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.moonInfo.system_id);

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.starInfo).HasColumnType("jsonb");

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.asteroidBeltInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.asteroidBeltInfo.name);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.asteroidBeltInfo.system_id);

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.stationInfo).HasColumnType("jsonb");
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.stationInfo.station_id);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.stationInfo.name);
            //modelBuilder.Entity<EveOnlineUniverseLocation>().Ignore(x => x.stationInfo.system_id);

            modelBuilder.Entity<EveOnlineUniverseLocation>().Property(m => m.structureInfo).HasColumnType("jsonb");
            

            modelBuilder.Entity<EveOnlineUniverseType>().ToTable("utypes");
            modelBuilder.Entity<EveOnlineUniverseType>().Property(m => m.dogma_attributes).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineUniverseType>().Property(m => m.dogma_effects).HasColumnType("jsonb");
            // Необходимо вречную указать, что min_value type_id = 0

            modelBuilder.Entity<EveOnlineUniverseType>().Ignore(x => x.name);
            modelBuilder.Entity<EveOnlineUniverseType>().Ignore(x => x.description);
            modelBuilder.Entity<EveOnlineUniverseType>().Property(m => m.img_tags).HasColumnType("jsonb");

            modelBuilder.Entity<EveOnlineUniverseCategory>().ToTable("ucategories");
            modelBuilder.Entity<EveOnlineUniverseCategory>().Ignore(x => x.name);

            modelBuilder.Entity<EveOnlineUniverseGroup>().ToTable("ugroups");
            modelBuilder.Entity<EveOnlineUniverseGroup>().Ignore(x => x.name);
            modelBuilder.Entity<EveOnlineUniverseGroup>().Property(m => m.types).HasColumnType("jsonb");

            modelBuilder.Entity<EveOnlineUniverseRace>().ToTable("uraces");
            modelBuilder.Entity<EveOnlineUniverseRace>().Ignore(x => x.name);
            modelBuilder.Entity<EveOnlineUniverseRace>().Ignore(x => x.description);

            modelBuilder.Entity<EveOnlineUniverseFaction>().ToTable("ufactions");
            modelBuilder.Entity<EveOnlineUniverseFaction>().Ignore(x => x.name);
            modelBuilder.Entity<EveOnlineUniverseFaction>().Ignore(x => x.description);

            modelBuilder.Entity<EveOnlineUniverseBloodLine>().ToTable("ubloodLines");
            modelBuilder.Entity<EveOnlineUniverseBloodLine>().Ignore(x => x.name);
            modelBuilder.Entity<EveOnlineUniverseBloodLine>().Ignore(x => x.description);

            
            modelBuilder.Entity<EveOnlineUniverseGraphic> ().ToTable("ugraphics");

            //modelBuilder.Entity<EveOnlineUniverseStation>().Property(m => m.services).HasColumnType("jsonb");

            // Killmail
            modelBuilder.Entity<EveOnlineKillMail>().ToTable("kms");
            modelBuilder.Entity<EveOnlineKillMail>().Property(p => p.preview).HasColumnType("jsonb");

            modelBuilder.Entity<EveOnlineKillMailVictim>().Property(p => p.items).HasColumnType("jsonb");
            modelBuilder.Entity<EveOnlineKillMailVictim>().Ignore(p => p.position);

            modelBuilder.Entity<EveOnlineKillmailActioner>().ToTable("kmsa");
            modelBuilder.Entity<EveOnlineKillmailActioner>()
                .HasDiscriminator(p=>p.d)
                .HasValue<EveOnlineKillMailAttacker>(EEveOnlineKillmailActionerType.Attacker)
                .HasValue<EveOnlineKillmailActioner>(EEveOnlineKillmailActionerType.Base)
                .HasValue<EveOnlineKillMailVictim>(EEveOnlineKillmailActionerType.Victim);

            modelBuilder.Entity<EveOnlineKillmailActioner>().HasIndex(p => p.character_id);
            modelBuilder.Entity<EveOnlineKillmailActioner>().HasIndex(p => p.alliance_id);
            modelBuilder.Entity<EveOnlineKillmailActioner>().HasIndex(p => p.corporation_id);
            modelBuilder.Entity<EveOnlineKillmailActioner>().HasIndex(p => p.d);

            // External
            modelBuilder.Entity<ZKillBoardStatItem>().ToTable("kmsz");
            modelBuilder.Entity<EveDirectCheckPoint>().ToTable("ed_checkpoints");

            // Wars
            modelBuilder.Entity<EveOnlineWar>().ToTable("wars");
            modelBuilder.Entity<EveOnlineWarAlly>().ToTable("warsa");

            // Identity
            modelBuilder.Entity<IdentitySso>().ToTable("issos");
            modelBuilder.Entity<IdentitySso>().Property(p => p.added).HasColumnType(columnTypes.onlyDate);
            modelBuilder.Entity<IdentitySso>().Property(p => p.last_owner_and_status_update).HasColumnType(columnTypes.onlyDate);
            modelBuilder.Entity<IdentitySso>().Property(p => p.token_scopes).HasColumnType("jsonb");

            modelBuilder.Entity<IdentitySsoRequest>().ToTable("issorequests");
            modelBuilder.Entity<IdentitySsoRequest>().Property(p => p.dt).HasColumnType(columnTypes.onlyDate);

            modelBuilder.Entity<IdentityConnectionStringToDb>().ToTable("iconstrs");

            modelBuilder.Entity<Account>().ToTable("iaccounts");
            modelBuilder.Entity<Account>().Property(p => p.сreated).HasColumnType(columnTypes.onlyDate);

            modelBuilder.Entity<AccountRole>().ToTable("iaccroles");

            modelBuilder.Entity<AccountRoleRef>().ToTable("iaccrolesrefs");
            modelBuilder.Entity<AccountRoleRef>().Property(p => p.assign).HasColumnType(columnTypes.onlyDate);
            modelBuilder.Entity<AccountRoleRef>().Ignore(p => p.WhenAssign);

            modelBuilder.Entity<IdentityRoleClaim<ulong>>().ToTable("iaccroleclaims");
            modelBuilder.Entity<IdentityUserClaim<ulong>>().ToTable("iaccuserclaims");
            modelBuilder.Entity<IdentityUserLogin<ulong>>().ToTable("iaccuserlogins");
            modelBuilder.Entity<IdentityUserToken<ulong>>().ToTable("iaccusertokens");

            modelBuilder.Entity<LoginHistoryEntry>().ToTable("iaccloginhist");

            // Search
            modelBuilder.Entity<SearchItem>().ToTable("ed_search");
            modelBuilder.Entity<SearchItem>().HasIndex(p => p.title);
            modelBuilder.Entity<SearchItem>().HasIndex(p => p.type);

            // дискреминатор в внутренностях системы
            // В EF Core не работает FLUENT API
            //modelBuilder.Entity<EveOnlineUniverseSystemLocation>()
            //    .HasDiscriminator<SystemLocDiscriminatorId>("d")
            //    .HasValue<EveOnlineUniversePlanet>(SystemLocDiscriminatorId.EveOnlineUniversePlanet)
            //    .HasValue<EveOnlineUniverseStargate>(SystemLocDiscriminatorId.EveOnlineUniverseStargate)
            //    .HasValue<EveOnlineUniverseMoon>(SystemLocDiscriminatorId.EveOnlineUniverseMoon)
            //    .HasValue<EveOnlineUniverseStar>(SystemLocDiscriminatorId.EveOnlineUniverseStar)
            //    .HasValue<EveOnlineUniverseStation>(SystemLocDiscriminatorId.EveOnlineUniverseStation)
            //    .HasValue<EveOnlineUniverseAsteroidBelt>(SystemLocDiscriminatorId.EveOnlineUniverseAsteroidBelt);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = ConfigurationStatic.LoadConnectionString("PublicDb-Test");
                optionsBuilder = ContextStatic.DbContextOptions(connStr);
            }

            base.OnConfiguring(optionsBuilder);
        }
        public PublicContext(DbContextOptions<PublicContext> options) : base(options) { }
        public PublicContext() { }
    }
    
    public class PublicContextFactory : IDesignTimeDbContextFactory<PublicContext>
    {
        public PublicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PublicContext>();
            var connStr = ConfigurationStatic.LoadConnectionString("PublicDb-Test");
            optionsBuilder.UseNpgsql(connStr);

            return new PublicContext(optionsBuilder.Options);
        }
    }
}
