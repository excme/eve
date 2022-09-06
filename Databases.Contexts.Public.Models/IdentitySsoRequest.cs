using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class IdentitySsoRequest
    {
        public Guid Id { get; set; }
        public int owner_id { get; set; }
        public DateTime dt { get; set; }
        //public ESsoRequestType type { get; set; }
        public int sso_Records_updates { get; set; }
        public int db_changes { get; set; }
        public void Update(int _countUpdates, int _dbChanges/*, ESsoRequestType _type*/)
        {
            sso_Records_updates = _countUpdates;
            //type = _type;
            db_changes = _dbChanges;
            dt = DateTime.UtcNow;
        }
    }
    //public enum ESsoRequestType : byte
    //{
    //    Unknown = 0,
    //    characterAssets = 1,
    //    characterBookmarks = 2,
    //    characterCalendars = 3,
    //    characterAffiliation = 4,
    //    characterAgentResearches = 5,
    //    characterBlueprints = 6,
    //    characterContacts = 7,
    //    characterCorpHistory = 8,
    //    characterFatigues = 9,
    //    characterMedals = 10,
    //    characterNotifications = 11,
    //    characterNotificationsContacts = 12,
    //    characterPublicInfo = 13,
    //    characterRoles = 14,
    //    characterStandings = 15,
    //    characterStats = 16,
    //    characterClones = 17,
    //    characterImplants = 18,
    //    characterContracts = 19,
    //    characterContractsItems = 20,
    //    characterContractsBids = 21,
    //    characterFractionWarStat = 22,
    //    characterFitting = 23,
    //    characterIndustryJobs = 24,
    //    characterIndustryMining = 25,
    //    characterKillmails = 26,
    //    characterLocationOnline = 27,
    //    characterLocationShip = 28,
    //    characterLocationCurrent = 29,
    //    characterLoyalties = 30,
    //    characterMail = 31,
    //    characterMarket = 32,
    //    characterMarketHistory = 33,
    //    characterPlanets = 34,
    //    characterSkills = 35,
    //    characterSkillsAttributes = 36,
    //    characterSkillsQueues = 37,
    //    characterWalletBalance = 38,
    //    characterWalletJournals = 39,
    //    characterWalletTransactions = 40,

    //    corporationAssets = 100,
    //    corporationBookmarks = 101,
    //    corporationContracts = 102,
    //    corporationContractsItems = 103,
    //    corporationContractsBids = 104,
    //    corporationAllianceHistory = 105,
    //    corporationBlueprints = 106,
    //    corporationContacts = 107,
    //    corporationContainerLogs = 108,
    //    corporationDivisions = 109,
    //    corporationFacilities = 110,
    //    corporationMedals = 111,
    //    corporationMembers = 112,
    //    corporationMembertracking = 113,
    //    corporationNcps = 114,
    //    corporationPublicInfo = 115,
    //    corporationRoles = 116,
    //    corporationShareHolders = 117,
    //    corporationStandings = 118,
    //    corporationStarbases = 119,
    //    corporationStructures = 120,
    //    corporationIndustryJobs = 121,
    //    corporationIndustryMiningExtractions = 122,
    //    corporationIndustryMiningObservers = 123,
    //    corporationKillmails = 124,
    //    corporationLoyaltyOffers = 125,
    //    corporationMarket = 126,
    //    corporationMarketHistory = 127,
    //    corporationCorporationCustomsOffices = 128,
    //    corporationWalletBalance = 129,
    //    corporationWalletJournals = 130,
    //    corporationWalletTransactions = 131,
    //    corporationMedalsIssued = 132,

    //    alliancesList = 180,
    //    alliancesListCorporations = 181,
    //    alliancesPublicInfo = 182,

    //    ssoOwnerCheck = 200,
    //    dogmaAttributes = 201,
    //    dogmaEffects = 202,
    //    incursions = 203,
    //    industryFacilities = 204,
    //    industrySystems = 205,
    //    marketGroups = 206,
    //    marketActualOrders = 207,
    //    marketHistoryPrices = 208,
    //    opportunitiesGroups = 209,
    //    opportunitiesTasks = 210,
    //    publicContracts = 211,

    //    warIds = 220,
    //    warInfo = 221,
    //    warKillmails = 222,

    //    universeTypes = 223,
    //    universeStructures = 224,
    //    universeCategories = 225,
    //    universeGroups = 226,
    //    universeRaces = 227,
    //    universeFractions = 231,
    //    universeBloodlines = 232,
    //    UniverseRegions = 228,
    //    UniverseConstellations = 229,
    //    UniverseSystems = 230,
    //    UniverseAncestries = 233,
    //    UniverseNames = 234,

    //    zKillboard = 231,
    //    KillmailsInfo = 232,
    //    KillmailsSearchInnerIds = 233
    //}
}
