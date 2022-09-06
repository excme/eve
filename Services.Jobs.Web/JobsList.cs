using Hangfire;
using eveDirect.Services.Jobs.Core;
using System;
using System.Collections.Generic;
using eveDirect.Services.Jobs.Market;
using eveDirect.Shared.WebHost;
using eveDirect.Jobs.Processing;
using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Warface.Jobs;
using Services.Jobs.Processings;

namespace eveDirect.Services.Jobs.Web
{
    public static class JobsList
    {
        //static IBackgroundJobClient _backgroundJobs { get; set; }
        //static List<RecurringJobDto> recurringJobs { get; set; }
        //static IMonitoringApi _monitor { get; set; }
        public static void Load(/*IBackgroundJobClient backgroundJobs,*/ List<EJobsCategories> queues/*, bool isProduction = false*/)
        {
            //_backgroundJobs = backgroundJobs;
            //var _monitor = JobStorage.Current.GetMonitoringApi();

            // Чтобы не пересоздавался hangfire server
            // https://stackoverflow.com/questions/50077072/hangfire-new-server-appears-every-time-code-changed
            //if (isProduction)
            //{
            //var processingCount = _monitor.ProcessingCount();
            //if (processingCount > 0)
            //{
            //    var list = _monitor.ProcessingJobs(0, (int)processingCount);
            //    foreach (var job in list)
            //    {
            //        backgroundJobs.Delete(job.Key);
            //    }
            //}
            //}

            foreach (var queue in queues)
            {
                switch (queue)
                {
                    case EJobsCategories.search_ids:

                        // Поиск новых персонажей перебором
                        Schedule_Trigger<CharacterSearchNewborn>(TimeSpan.FromMinutes(1), queue);
                        Schedule_Trigger<CharacterNPCSearchNewborn>(TimeSpan.FromHours(6), queue);
                        Schedule_Trigger<AllianceSearchNew>(TimeSpan.FromMinutes(5), queue);
                        Schedule_Trigger<CorporationSearchNew>(TimeSpan.FromMinutes(5), queue);

                        break;

                    case EJobsCategories.public_characters:

                        // Character
                        Schedule_Trigger<CharacterAffiliation>(TimeSpan.FromMinutes(1), queue);
                        //Schedule_Trigger<CharacterAgentsResearches>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CharacterBlueprints>(TimeSpan.FromHours(6));

                        Schedule_Trigger<CharacterCorpHistory>(TimeSpan.FromMinutes(1), queue);

                        //Schedule_Trigger<CharacterFatigues>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CharacterMedals>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CharacterNotifications>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CharacterNotificationsContacts>(TimeSpan.FromHours(6));

                        Schedule_Trigger<CharacterPublicInformationJob>(TimeSpan.FromMinutes(1), queue);
                        //Schedule_Trigger<CharacterRoles>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CharacterStandings>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CharacterStats>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CharacterПроверкаOwnerHashПерсонажей>(TimeSpan.FromHours(6));


                        break;
                    case EJobsCategories.public_ally:
                        // Alliance 
                        Schedule_Trigger<AlliancesGetList>(TimeSpan.FromHours(12), queue);
                        Schedule_Trigger<AlliancesGetListCorporations>(TimeSpan.FromHours(1), queue);
                        Schedule_Trigger<AlliancesPublicInformation>(TimeSpan.FromHours(24), queue);
                        break;

                    case EJobsCategories.public_corp:

                        // Corporation
                        Schedule_Trigger<CorporationAllianceHistories>(TimeSpan.FromSeconds(3600), queue);
                        //Schedule_Trigger<CorporationBlueprints>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationContacts>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationContainersLogs>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationDivisions>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationFacilities>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationMedals>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationMembers>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationMembersTracking>(TimeSpan.FromHours(6));
                        Schedule_Trigger<CorporationNpcs>(TimeSpan.FromDays(1), queue, at11_05: true);

                        // TODO: Проверить, чтобы chmgr не обнулялся при обновлении публ инфо
                        Schedule_Trigger<CorporationPublicInformation>(TimeSpan.FromMinutes(1), queue);

                        //Schedule_Trigger<CorporationRoles>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationShareholders>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationStandings>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationStarbases>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationStructures>(TimeSpan.FromHours(6));


                        break;
                    case EJobsCategories.public_warface:

                        // Dogma
                        //Schedule_Trigger<DogmaAttributes>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<DogmaEffects>(TimeSpan.FromHours(6));

                        // FactionWarfare
                        //Schedule_Trigger<CharacterFactionWarfareStats>(TimeSpan.FromHours(6));

                        // Killmails
                        //Schedule_Trigger<CharacterKillmails>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<CorporationKillmails>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<KillmailsInfos>(TimeSpan.FromMinutes(1), queue);
                        //Schedule_Trigger<SearcInnerIds>(TimeSpan.FromMinutes(1), queue);
                        //Schedule_Trigger<UpdateLocationIds>(TimeSpan.FromMinutes(1));

                        // Wars
                        //Schedule_Trigger<WarIdsUpdate>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<WarInfosUpdate>(TimeSpan.FromHours(6));
                        //Schedule_Trigger<WarKillmailsUpdate>(TimeSpan.FromHours(6));

                        // zKillboard
                        Schedule_Trigger<CollectionKillsFromZKillBoardApi>(TimeSpan.FromMinutes(1), queue);

                        break;
                    case EJobsCategories.public_universe:

                        // Universe
                        Schedule_Trigger<UniverseCategories>(TimeSpan.FromDays(14), queue);
                        Schedule_Trigger<UniverseGroups>(TimeSpan.FromDays(14), queue);
                        Schedule_Trigger<UniverseFaction>(TimeSpan.FromDays(14), queue);
                        Schedule_Trigger<UniverseBloodline>(TimeSpan.FromDays(14), queue);
                        Schedule_Trigger<UniverseRace>(TimeSpan.FromDays(14), queue);
                        //Schedule_Trigger<UniverseStructures>(TimeSpan.FromHours(6));
                        Schedule_Trigger<UniverseTypes>(TimeSpan.FromDays(7), queue);
                        Schedule_Trigger<UniverseОбновлениеКосмоса>(TimeSpan.FromDays(7), queue);
                        // Поиск новых персонажей, корпораций и альянсов
                        //Schedule_Trigger<UniverseSearchNewCharactersCorporationsAlliances>(TimeSpan.FromMinutes(5));

                        break;
                    case EJobsCategories.public_market:

                        Schedule_Trigger<MarketActualOrders>(TimeSpan.FromSeconds(305), queue, at11_05: false);
                        Schedule_Trigger<MarketHistoryPrices>(TimeSpan.FromDays(1), queue, at11_05: true);
                        Schedule_Trigger<PublicContracts>(TimeSpan.FromSeconds(1800), queue, at11_05: false);
                        Schedule_Trigger<MarketGroups>(TimeSpan.FromDays(1), queue, at11_05: true);

                        break;

                    case EJobsCategories._default:
                        // Пересчет миграций
                        //BackgroundJob.Enqueue<RecalcCharacterMigrations>(j => j.Work());

                        break;

                    case EJobsCategories.processing:

                        Schedule_Trigger<SearchItemsJob>(TimeSpan.FromDays(1), queue);

                        break;

                }
            }

            // Assets
            //Schedule_Trigger<CharacterAssets>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationAssets>(TimeSpan.FromHours(6));

            // Bookmarks
            //Schedule_Trigger<CharacterBookmarks>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationBookmarks>(TimeSpan.FromHours(6));

            // Calendar
            //Schedule_Trigger<CharacterCalendars>(TimeSpan.FromHours(6));

            // Clones
            //Schedule_Trigger<CharacterCloneImplants>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterClones>(TimeSpan.FromHours(6));

            // Contracts
            //Schedule_Trigger<CharacterContracts>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterContractsBids>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterContractsItems>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationContracts>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationContractsBids>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationContractsItems>(TimeSpan.FromHours(6));

            // Corporation Taxes
            //Schedule_Trigger<CorporationCombatAnomaliesTaxModule>(TimeSpan.FromHours(6));

            // Fittings
            //Schedule_Trigger<CharacterFittings>(TimeSpan.FromHours(6));

            // Incursions
            //Schedule_Trigger<Incursions>(TimeSpan.FromHours(6));

            // Industry
            //Schedule_Trigger<CharacterIndustryJobs>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterIndustryMining>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationIndustryJobs>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationIndustryMiningExtractions>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationIndustryMiningObservers>(TimeSpan.FromHours(6));
            //Schedule_Trigger<IndustryFacilities>(TimeSpan.FromHours(6));
            //Schedule_Trigger<IndustrySystems>(TimeSpan.FromHours(6));

            // Location
            //Schedule_Trigger<CharacterLocations>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterOnlines>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterShips>(TimeSpan.FromHours(6));

            // Loyalty
            //Schedule_Trigger<CharacterLoyalties>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationLoyaltyOffers>(TimeSpan.FromHours(6));

            // Mail
            //Schedule_Trigger<CharacterMails>(TimeSpan.FromHours(6));

            // Market
            //Schedule_Trigger<CharacterMarketOrders>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationMarketOrders>(TimeSpan.FromHours(6)); 
            //Schedule_Trigger<MarketGroups>(TimeSpan.FromHours(6));
            //Schedule_Trigger<MarketPrices>(TimeSpan.FromHours(6));
            //Schedule_Trigger<MarketHistoryPrices>(TimeSpan.FromHours(6));

            // Opportunities
            //Schedule_Trigger<OpportunitiesGroups>(TimeSpan.FromHours(6));
            //Schedule_Trigger<OpportunitiesTasks>(TimeSpan.FromHours(6));

            // Planetary Interaction
            //Schedule_Trigger<CharacterPlanets>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationCustomsOffices>(TimeSpan.FromHours(6));

            // Skills
            //Schedule_Trigger<CharacterSkills>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterSkillsAttributes>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterSkillsQueues>(TimeSpan.FromHours(6));

            // Wallet
            //Schedule_Trigger<CharacterWalletBalance>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterWalletJournals>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CharacterWalletTransactions>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationWalletBalance>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationWalletJournals>(TimeSpan.FromHours(6));
            //Schedule_Trigger<CorporationWalletTransactions>(TimeSpan.FromHours(6));
        }
        static void Schedule_Trigger<T>(TimeSpan interval, EJobsCategories _queue, bool startNow = false, bool at11_05 = false)
            where T : JobBase
        {
            //Проверка, есть ли эта задача уже в циклических
            //RecurringJob.RemoveIfExists(typeof(T).Name + ".TaskJob");

            if (at11_05)
            {
                RecurringJob.AddOrUpdate<T>(j => j.TaskJob(null), $"06 11 * * *");
            }
            else
            {
                //Циклическое выполнение
                if (interval.Seconds > 0)
                    RecurringJob.AddOrUpdate<T>(j => j.TaskJob(null),
                        $"{cronElementParse(interval.Seconds)} {cronElementParse(interval.Minutes)} {cronElementParse(interval.Hours)} * * *",
                        queue: _queue.ToString());
                else if(interval.Seconds == 0 && interval.Minutes == 0)
                    RecurringJob.AddOrUpdate<T>(j => j.TaskJob(null),
                        $"0 {cronElementParse(interval.Hours)} {cronElementParse(interval.Days)} * *",
                        queue: _queue.ToString());
                else if (interval.Seconds == 0 && interval.Minutes == 0 && interval.Hours == 0)
                    RecurringJob.AddOrUpdate<T>(j => j.TaskJob(null),
                        $"0 0 {cronElementParse(interval.Days)} * *",
                        queue: _queue.ToString());
                else
                    RecurringJob.AddOrUpdate<T>(j => j.TaskJob(null),
                        $"{cronElementParse(interval.Minutes)} {cronElementParse(interval.Hours)} {cronElementParse(interval.Days)} * *",
                        queue: _queue.ToString());
            }

            string cronElementParse(int val)
            {
                if (val > 0)
                    return $"*/{val}";

                return $"*";
            }
        }
    }
}
