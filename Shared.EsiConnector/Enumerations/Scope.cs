using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Enumerations
{
    /// <summary>SSO Scopes</summary>
    public class Scope
    {
        public Scope(string val)
        {
            Name = val;
        }

        public Scope(string val, string groupName) : this(val)
        {
            GroupName = groupName;
        }

        /// <summary>SSO Scope</summary>
        public string Name { get; }
        /// <summary>
        /// Название группы
        /// </summary>
        public string GroupName { get; set; }
        public bool IsSelected { get; set; }

        /// <summary>No Scopes</summary>
        public static readonly Scope None = new Scope(string.Empty);

        public static List<Scope> AllScopeNames()
        {
            var list = new List<Scope>();
            list.AddRange(AllCharacterReadScopes);
            list.AddRange(AllCharacterWriteScopes);
            list.AddRange(AllCorporationReadScopes);
            list.AddRange(AllCorporationWriteScopes);
            list.AddRange(AllAllianceScopes);

            return list;
        }

        public static List<Scope> AllCharacterReadScopes
        {
            get {
                return new List<Scope>() {
                    Assets.ReadAssets,

                    Bookmarks.ReadCharacterBookmarks,

                    Calendar.ReadCalendarEvents,
                    Calendar.RespondCalendarEvents,

                    Characters.ReadAgentsResearch,
                    Characters.ReadBlueprints,
                    Characters.ReadChatChannels,
                    Characters.ReadContacts,
                    Characters.ReadCorporationRoles,
                    Characters.ReadFatigue,
                    Characters.ReadLoyalty,
                    Characters.ReadMedals,
                    Characters.ReadNotifications,
                    Characters.ReadOpportunities,
                    Characters.ReadStats,
                    Characters.ReadStandings,
                    Characters.ReadTitles,

                    Clones.ReadClones,
                    Clones.ReadImplants,

                    Contracts.ReadCharacterContracts,

                    FactionWarface.ReadCharacterStats,

                    Fittings.ReadFittings,

                    Fleets.ReadFleet,

                    Industry.ReadCharacterJobs,
                    Industry.ReadCharacterMining,

                    Killmails.ReadKillmails,

                    Location.ReadLocation,
                    Location.ReadOnline,
                    Location.ReadShipType,

                    Mail.ReadMail,

                    Markets.ReadCharacterOrders,
                    Markets.StructureMarkets,

                    Planets.ReadCustomsOffices,

                    Search.SearchStructures,

                    Skills.ReadSkillQueue,
                    Skills.ReadSkills,

                    Universe.ReadStructures,

                    Wallet.ReadCharacterWallet
                };
            }
        }
        public static List<Scope> AllCharacterWriteScopes
        {
            get
            {
                return new List<Scope>() {
                    Characters.WriteContacts,

                    Fittings.WriteFittings,

                    Fleets.WriteFleet,

                    Mail.OrganizeMail,
                    Mail.SendMail,

                    Planets.ManagePlanets
                };
            }
        }

        public static List<Scope> AllCorporationReadScopes
        {
            get
            {
                return new List<Scope>() {
                    Assets.ReadCorporationAssets,

                    Bookmarks.ReadCorporationBookmarks,

                    Contracts.ReadCorporationContracts,

                    Corporations.ReadBlueprints,
                    Corporations.ReadContacts,
                    Corporations.ReadContainerLogs,
                    Corporations.ReadCorporationMembership,
                    Corporations.ReadDivisions,
                    Corporations.ReadFacilities,
                    Corporations.ReadMedals,
                    Corporations.ReadStandings,
                    Corporations.ReadStarbases,
                    Corporations.ReadStructures,
                    Corporations.ReadTitles,
                    Corporations.TrackMembers,
                    //Corporations.ReadOutposts,

                    FactionWarface.ReadCorporationStats,

                    Industry.ReadCorporationJobs,
                    Industry.ReadCorporationMining,

                    Killmails.ReadCorporationKillmails,

                    Markets.ReadCorporationOrders,

                    Wallet.ReadCorporationWallets,
                };
            }
        }

        public static List<Scope> AllCorporationWriteScopes
        {
            get
            {
                return new List<Scope>()
                {
                    //Corporations.WriteStructures
                };
            }
        }
        public static List<Scope> AllAllianceScopes
        {
            get
            {
                return new List<Scope>()
                {
                    Alliances.ReadContacts
                };
            }
        }

        public static class Alliances
        {
            /// <summary>Read Alliance Contacts Scope</summary>
            public static readonly Scope ReadContacts = new Scope("esi-alliances.read_contacts.v1", "Alliance Read Scopes");
        }

        /// <summary>Asset Scopes</summary>
        public static class Assets
        {
            /// <summary>Read Assets Scope</summary>
            public static readonly Scope ReadAssets = new Scope("esi-assets.read_assets.v1", "Character Read Scopes");
            /// <summary>Read Corporation Assets Scope</summary>
            public static readonly Scope ReadCorporationAssets = new Scope("esi-assets.read_corporation_assets.v1", "Corporation Read Scopes");
        }

        /// <summary>Bookmarks Scopes</summary>
        public static class Bookmarks
        {
            /// <summary>Read Character Bookmarks Scope</summary>
            public static readonly Scope ReadCharacterBookmarks = new Scope("esi-bookmarks.read_character_bookmarks.v1", "Character Read Scopes");
            /// <summary>Read Corporation Bookmarks Scope</summary>
            public static readonly Scope ReadCorporationBookmarks = new Scope("esi-bookmarks.read_corporation_bookmarks.v1", "Corporation Read Scopes");
        }

        /// <summary>Calendar Scopes</summary>
        public static class Calendar
        {
            /// <summary>Read Calendar Events Scope</summary>
            public static readonly Scope ReadCalendarEvents = new Scope("esi-calendar.read_calendar_events.v1", "Character Read Scopes");
            /// <summary>Respond Calendar Events Scope</summary>
            public static readonly Scope RespondCalendarEvents = new Scope("esi-calendar.respond_calendar_events.v1", "Character Read Scopes");
        }

        /// <summary>Character Scopes</summary>
        public static class Characters
        {
            /// <summary>Read Character Agents Research Scope</summary>
            public static readonly Scope ReadAgentsResearch = new Scope("esi-characters.read_agents_research.v1", "Character Read Scopes");
            /// <summary>Read Character Blueprints Scope</summary>
            public static readonly Scope ReadBlueprints = new Scope("esi-characters.read_blueprints.v1", "Character Read Scopes");
            /// <summary>Read Character Chat Channels</summary>
            public static readonly Scope ReadChatChannels = new Scope("esi-characters.read_chat_channels.v1", "Character Read Scopes");
            /// <summary>Read Character Contacts Scope</summary>
            public static readonly Scope ReadContacts = new Scope("esi-characters.read_contacts.v1", "Character Read Scopes");
            /// <summary>Read Character Corporation Roles Scope</summary>
            public static readonly Scope ReadCorporationRoles = new Scope("esi-characters.read_corporation_roles.v1", "Character Read Scopes");
            /// <summary>Read Character Fatigue Scope</summary>
            public static readonly Scope ReadFatigue = new Scope("esi-characters.read_fatigue.v1", "Character Read Scopes");
            /// <summary>Read Character Loyalty Scope</summary>
            public static readonly Scope ReadLoyalty = new Scope("esi-characters.read_loyalty.v1", "Character Read Scopes");
            /// <summary>Read Character Medals Scope</summary>
            public static readonly Scope ReadMedals = new Scope("esi-characters.read_medals.v1", "Character Read Scopes");
            /// <summary>Read Character Notifications Scope</summary>
            public static readonly Scope ReadNotifications = new Scope("esi-characters.read_notifications.v1", "Character Read Scopes");
            /// <summary>Read Character Opportunities Scope</summary>
            public static readonly Scope ReadOpportunities = new Scope("esi-characters.read_opportunities.v1", "Character Read Scopes");
            /// <summary>Read Character Standings Scope</summary>
            public static readonly Scope ReadStandings = new Scope("esi-characters.read_standings.v1", "Character Read Scopes");
            /// <summary>Read Character Stats</summary>
            public static readonly Scope ReadStats = new Scope("esi-characterstats.read.v1", "Character Read Scopes");
            /// <summary>Read Character Titles Scope</summary>
            public static readonly Scope ReadTitles = new Scope("esi-characters.read_titles.v1", "Character Read Scopes");
            /// <summary>Write Character Contacts Scope</summary>
            public static readonly Scope WriteContacts = new Scope("esi-characters.write_contacts.v1", "Character Write Scopes");
        }

        /// <summary>Clones Scopes</summary>
        public static class Clones
        {
            /// <summary>Read Clones Scope</summary>
            public static readonly Scope ReadClones = new Scope("esi-clones.read_clones.v1", "Character Read Scopes");
            /// <summary>Read Active Clone Implants Scope</summary>
            public static readonly Scope ReadImplants = new Scope("esi-clones.read_implants.v1", "Character Read Scopes");
        }

        /// <summary>Contracts Scopes</summary>
        public static class Contracts
        {
            /// <summary>Read Character Contracts Scope</summary>
            public static readonly Scope ReadCharacterContracts = new Scope("esi-contracts.read_character_contracts.v1", "Character Read Scopes");
            /// <summary>Read Corporation Contracts Scope</summary>
            public static readonly Scope ReadCorporationContracts = new Scope("esi-contracts.read_corporation_contracts.v1", "Corporation Read Scopes");
        }

        /// <summary>Corporation Scopes</summary>
        public static class Corporations
        {
            /// <summary>Read Corporation Blueprints Scope</summary>
            public static readonly Scope ReadBlueprints = new Scope("esi-corporations.read_blueprints.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Container Logs Scope</summary>
            public static readonly Scope ReadContainerLogs = new Scope("esi-corporations.read_container_logs.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Contacts Scope</summary>
            public static readonly Scope ReadContacts = new Scope("esi-corporations.read_contacts.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Membership Scope</summary>
            public static readonly Scope ReadCorporationMembership = new Scope("esi-corporations.read_corporation_membership.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Divisions Scope</summary>
            public static readonly Scope ReadDivisions = new Scope("esi-corporations.read_divisions.v1", "Corporation Read Scopes");
            /// <summary>Read Corporations Facilities</summary>
            public static readonly Scope ReadFacilities = new Scope("esi-corporations.read_facilities.v1", "Corporation Read Scopes");
            /// <summary>Read Corporations Medals</summary>
            public static readonly Scope ReadMedals = new Scope("esi-corporations.read_medals.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Standings Scope</summary>
            public static readonly Scope ReadStandings = new Scope("esi-corporations.read_standings.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Starbases Scope</summary>
            public static readonly Scope ReadStarbases = new Scope("esi-corporations.read_starbases.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Structures Scope</summary>
            public static readonly Scope ReadStructures = new Scope("esi-corporations.read_structures.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Titles Scope</summary>
            public static readonly Scope ReadTitles = new Scope("esi-corporations.read_titles.v1", "Corporation Read Scopes");
            /// <summary>Track Corporation Members Scope</summary>
            public static readonly Scope TrackMembers = new Scope("esi-corporations.track_members.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Outposts Scope</summary>
            //public static readonly Scope ReadOutposts = new Scope("esi-corporations.read_outposts.v1", "Corporation Read Scopes");
            /// <summary>Write Corporation Structures Scope</summary>
            //public static readonly Scope WriteStructures = new Scope("esi-corporations.write_structures.v1", "Corporation Write Scopes");
        }

        /// <summary>Fitting Scopes</summary>
        public static class FactionWarface
        {
            /// <summary>Read Fittings Scope</summary>
            public static readonly Scope ReadCharacterStats = new Scope("esi-characters.read_fw_stats.v1", "Character Read Scopes");
            /// <summary>Write Fittings Scope</summary>
            public static readonly Scope ReadCorporationStats = new Scope("esi-corporations.read_fw_stats.v1", "Corporation Read Scopes");
        }

        /// <summary>Fitting Scopes</summary>
        public static class Fittings
        {
            /// <summary>Read Fittings Scope</summary>
            public static readonly Scope ReadFittings = new Scope("esi-fittings.read_fittings.v1", "Character Read Scopes");
            /// <summary>Write Fittings Scope</summary>
            public static readonly Scope WriteFittings = new Scope("esi-fittings.write_fittings.v1", "Character Write Scopes");
        }

        /// <summary>Fleet Scopes</summary>
        public static class Fleets
        {
            /// <summary>Read Fleet Scope</summary>
            public static readonly Scope ReadFleet = new Scope("esi-fleets.read_fleet.v1", "Character Read Scopes");
            /// <summary>Write Fleet Scope</summary>
            public static readonly Scope WriteFleet = new Scope("esi-fleets.write_fleet.v1", "Character Write Scopes");
        }

        /// <summary>Industry Scopes</summary>
        public static class Industry
        {
            /// <summary>Read Character Jobs Scope</summary>
            public static readonly Scope ReadCharacterJobs = new Scope("esi-industry.read_character_jobs.v1", "Character Read Scopes");
            /// <summary>Read Character Mining Scope</summary>
            public static readonly Scope ReadCharacterMining = new Scope("esi-industry.read_character_mining.v1", "Character Read Scopes");
            /// <summary>Read Corporation Jobs Scope</summary>
            public static readonly Scope ReadCorporationJobs = new Scope("esi-industry.read_corporation_jobs.v1", "Corporation Read Scopes");
            /// <summary>Read Corporation Mining Scope</summary>
            public static readonly Scope ReadCorporationMining = new Scope("esi-industry.read_corporation_mining.v1", "Corporation Read Scopes");
        }

        /// <summary>Killmail Scopes</summary>
        public static class Killmails
        {
            /// <summary>Read Corporation Killmails Scope</summary>
            public static readonly Scope ReadCorporationKillmails = new Scope("esi-killmails.read_corporation_killmails.v1", "Corporation Read Scopes");
            /// <summary>Read Character Killmails Scope</summary>
            public static readonly Scope ReadKillmails = new Scope("esi-killmails.read_killmails.v1", "Character Read Scopes");
        }

        /// <summary>Location Scopes</summary>
        public static class Location
        {
            /// <summary>Read Location Scope</summary>
            public static readonly Scope ReadLocation = new Scope("esi-location.read_location.v1", "Character Read Scopes");
            /// <summary>Read Online Scope</summary>
            public static readonly Scope ReadOnline = new Scope("esi-location.read_online.v1", "Character Read Scopes");
            /// <summary>Read Ship Type Scope</summary>
            public static readonly Scope ReadShipType = new Scope("esi-location.read_ship_type.v1", "Character Read Scopes");
        }

        /// <summary>Mail Scopes</summary>
        public static class Mail
        {
            /// <summary>Organize Mail Scope</summary>
            public static readonly Scope OrganizeMail = new Scope("esi-mail.organize_mail.v1", "Character Write Scopes");
            /// <summary>Read Mail Scope</summary>
            public static readonly Scope ReadMail = new Scope("esi-mail.read_mail.v1", "Character Read Scopes");
            /// <summary>Send Scope</summary>
            public static readonly Scope SendMail = new Scope("esi-mail.send_mail.v1", "Character Write Scopes");
        }

        /// <summary>Market Scopes</summary>
        public static class Markets
        {
            /// <summary>Read Character Orders Scope</summary>
            public static readonly Scope ReadCharacterOrders = new Scope("esi-markets.read_character_orders.v1", "Character Read Scopes");
            /// <summary>Read Corporation Orders Scope</summary>
            public static readonly Scope ReadCorporationOrders = new Scope("esi-markets.read_corporation_orders.v1", "Corporation Read Scopes");
            /// <summary>Structure Markets Scope</summary>
            public static readonly Scope StructureMarkets = new Scope("esi-markets.structure_markets.v1", "Character Read Scopes");
        }

        /// <summary>Planet Scopes</summary>
        public static class Planets
        {
            /// <summary>Manage Planets Scope</summary>
            public static readonly Scope ManagePlanets = new Scope("esi-planets.manage_planets.v1", "Character Write Scopes");
            /// <summary>Read Customs Offices Scope</summary>
            public static readonly Scope ReadCustomsOffices = new Scope("esi-planets.read_customs_offices.v1", "Character Read Scopes");
        }

        /// <summary>Search Scopes</summary>
        public static class Search
        {
            /// <summary>Search Scope</summary>
            public static readonly Scope SearchStructures = new Scope("esi-search.search_structures.v1", "Character Read Scopes");
        }

        /// <summary>Skill Scopes</summary>
        public static class Skills
        {
            /// <summary>Read Skill Queue Scope</summary>
            public static readonly Scope ReadSkillQueue = new Scope("esi-skills.read_skillqueue.v1", "Character Read Scopes");
            /// <summary>Read Skills Scope</summary>
            public static readonly Scope ReadSkills = new Scope("esi-skills.read_skills.v1", "Character Read Scopes");
        }

        /// <summary>User Interface Scopes</summary>
        public static class Ui
        {
            /// <summary>Open Window Scope</summary>
            public static readonly Scope OpenWindow = new Scope("esi-ui.open_window.v1");
            /// <summary>Write Waypoint Scope</summary>
            public static readonly Scope WriteWaypoint = new Scope("esi-ui.write_waypoint.v1");
        }

        /// <summary>Universe Scopes</summary>
        public static class Universe
        {
            /// <summary>Read Structure Scope</summary>
            public static readonly Scope ReadStructures = new Scope("esi-universe.read_structures.v1", "Character Read Scopes");
        }

        /// <summary>Wallet Scopes</summary>
        public static class Wallet
        {
            /// <summary>Read Character Wallet Scope</summary>
            public static readonly Scope ReadCharacterWallet = new Scope("esi-wallet.read_character_wallet.v1", "Character Read Scopes");
            /// <summary>Read Corporation Wallet Scope</summary>
            //public static readonly Scope ReadCorporationWallet = new Scope("esi-wallet.read_corporation_wallet.v1");
            /// <summary>Read Corporation Wallets Scope</summary>
            public static readonly Scope ReadCorporationWallets = new Scope("esi-wallet.read_corporation_wallets.v1", "Corporation Read Scopes");
        }
    }
}
