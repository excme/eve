using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;
using eveDirect.Shared.EsiConnector.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class UniverseОбновлениеКосмоса : ConnectorJob
    {
        public UniverseОбновлениеКосмоса(
            IReadWrite repoPublicCommon,
            ILogger<UniverseОбновлениеКосмоса> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            //Universe_Regions();
            //Universe_Constellations();
            Universe_Systems();
        }

        public void Universe_Regions()
        {

            // Region Ids
            List<int> all_regions = new List<int>();

            // Выполнение запроса
            var request = EsiConnector(esiClient.Universe.Regions);
            if (request.isSuccess)
                all_regions = request.Data.ToList();

            if(all_regions?.Any() ?? false)
            {
                var universeRegions = new BlockingCollection<UniverseRegionInfoResult>();
                
                var list = AttachProgressBarToList(all_regions);
                //await list.ParallelForEachAsync(async region_id =>
                Parallel.ForEach(list, region_id =>
                {
                    // Запрос
                    var request = EsiConnector(esiClient.Universe.Region, region_id);
                    if (request.isSuccess)
                    {
                        universeRegions.Add(request.Data);
                    }
                });

                // Отправка в Бд
                if (universeRegions.Any())
                    _repoPublicCommon.Universe_Regions_AddNew(universeRegions.ToList());

                ToConsole($"Regions: {universeRegions.Count}");
                _jobResult.subValues.Add(new JobResult.Item() { Name = "Regions", Value = universeRegions.Count });
            }
        }

        public void Universe_Constellations()
        {
            List<int> new_constellations = new List<int>();
            // Выполнение запроса
            var request = EsiConnector(esiClient.Universe.Constellations);
            if (request.isSuccess)
                new_constellations = request.Data.ToList();
            if(new_constellations?.Any() ?? false)
            {
                var universeConstellations = new BlockingCollection<UniverseConstellationInfoResult>();

                var list = AttachProgressBarToList(new_constellations);
                //await list.ParallelForEachAsync(async new_constellation =>
                Parallel.ForEach(list, new_constellation =>
                {
                    // Запрос
                    var request = EsiConnector(esiClient.Universe.Constellation, new_constellation);
                    if (request.isSuccess)
                    {
                        universeConstellations.Add(request.Data);
                    }
                });

                // Отправка в Бд
                if (universeConstellations.Any())
                    _repoPublicCommon.Universe_Constellations_AddNew(universeConstellations.ToList());

                ToConsole($"Constellations: {universeConstellations.Count}");
                _jobResult.subValues.Add(new JobResult.Item() { Name = "Constellations", Value = universeConstellations.Count });
            }
        }

        public void Universe_Systems()
        {
            List<int> all_systems = new List<int>();

            //Выполнение запроса
            RequestResult<UniverseSystemsResult> request = null;
            do
            {
                request = EsiConnector(esiClient.Universe.Systems);
            } while (!request.isSuccess);

            if (request.isSuccess)
                all_systems = request.Data.ToList();

            if (all_systems?.Any() ?? false)
            {
                var universeSystems = new BlockingCollection<UniverseSystemInfoResult>();

                // Запрос информации о системе
                Parallel.ForEach(all_systems, new ParallelOptions() { MaxDegreeOfParallelism = 6 }, system_id =>
                {
                    // Запрос к коннектору 
                    RequestResult<UniverseSystemInfoResult> request_system = null;
                    do
                    {
                        request_system = EsiConnector(esiClient.Universe.System, system_id);
                    } while (!request_system.isSuccess);

                    if (request_system.isSuccess)
                        universeSystems.Add(request_system.Data);
                });

                if (universeSystems?.Any() ?? false)
                {
                    // Отправка в Бд
                    _repoPublicCommon.Universe_Systems_AddNew(universeSystems.ToList());
                    ToConsole($"Systems: {universeSystems.Count}");
                    _jobResult.subValues.Add(new JobResult.Item() { Name = "Systems", Value = universeSystems.Count });

                    // Запросы внутренностей
                    // Звезды
                    var universeStars = new ConcurrentDictionary<int, UniverseStarInfoResult>();
                    var all_stars = universeSystems.Select(x => x.star_id).ToList();

                    Parallel.ForEach(all_stars, new ParallelOptions() { MaxDegreeOfParallelism = 6 }, star_id =>
                    {
                        if (star_id > 0)
                        {
                            // Запрос к коннектору 
                            RequestResult<UniverseStarInfoResult> request_star = null;
                            do
                            {
                                request_star = EsiConnector(esiClient.Universe.Star, star_id);
                            } while (!request_star.isSuccess);

                            if (request_star.isSuccess)
                                universeStars.TryAdd(star_id, request_star.Data);
                        }
                    });
                    _repoPublicCommon.Universe_Stars_AddNew(universeStars);
                    ToConsole($"Stars: {universeStars.Count}");
                    _jobResult.subValues.Add(new JobResult.Item() { Name = "Stars", Value = universeStars.Count });

                    // Звездные врата 
                    var all_stargates = universeSystems.SelectMany(x => x.stargates ?? new List<int>()).ToList();
                    var universeStargates = new ConcurrentDictionary<int, UniverseStargateInfoResult>();
                    //await all_stargates.ParallelForEachAsync(async stargate_id =>
                    Parallel.ForEach(all_stargates, new ParallelOptions() { MaxDegreeOfParallelism = 6 }, stargate_id =>
                    {
                        if (stargate_id > 0)
                        {
                            // Запрос к коннектору 
                            RequestResult<UniverseStargateInfoResult> request = null;
                            do
                            {
                                request = EsiConnector(esiClient.Universe.Stargate, stargate_id);
                            } while (!request.isSuccess);

                            if (request.isSuccess)
                                universeStargates.TryAdd(stargate_id, request.Data);
                        }
                    });
                    _repoPublicCommon.Universe_Stargates_AddNew(universeStargates);
                    ToConsole($"Stargates: {universeStargates.Count}");
                    _jobResult.subValues.Add(new JobResult.Item() { Name = "Stargates", Value = universeStargates.Count });

                    // Станции
                    var all_stations = universeSystems.SelectMany(x => x.stations ?? new List<int>()).ToList();
                    var universeStations = new ConcurrentDictionary<int, UniverseStationInfoResult>();
                    //await all_stations.ParallelForEachAsync(async station_id =>
                    Parallel.ForEach(all_stations, new ParallelOptions() { MaxDegreeOfParallelism = 6 }, station_id =>
                    {
                        if (station_id > 0)
                        {
                            // Запрос к коннектору
                            RequestResult<UniverseStationInfoResult> request = null;
                            do
                            {
                                request = EsiConnector(esiClient.Universe.Station, station_id);
                            } while (!request.isSuccess);

                            if (request.isSuccess)
                                universeStations.TryAdd(station_id, request.Data);
                        }
                    });
                    _repoPublicCommon.Universe_Stations_AddNew(universeStations);
                    ToConsole($"Stations: {universeStations.Count}");
                    _jobResult.subValues.Add(new JobResult.Item() { Name = "Stations", Value = universeStations.Count });

                    // Планеты
                    var all_planets = universeSystems.SelectMany(x => x.planets ?? new List<UniverseSystemInfoResult.Planet>()).Select(x => x.planet_id).ToList();
                    var universePlanets = new ConcurrentDictionary<int, UniversePlanetInfoResult>();
                    //await all_planets.ParallelForEachAsync(async planet_id =>
                    Parallel.ForEach(all_planets, new ParallelOptions() { MaxDegreeOfParallelism = 6 }, planet_id =>
                    {
                        if (planet_id > 0)
                        {
                            // Запрос к коннектору 
                            RequestResult<UniversePlanetInfoResult> request = null;
                            do
                            {
                                request = EsiConnector(esiClient.Universe.Planet, planet_id);
                            } while (!request.isSuccess);

                            if (request.isSuccess)
                                universePlanets.TryAdd(planet_id, request.Data);
                        }
                    });
                    _repoPublicCommon.Universe_Planets_AddNew(universePlanets);
                    ToConsole($"Planets: {universePlanets.Count}");
                    _jobResult.subValues.Add(new JobResult.Item() { Name = "Planets", Value = universePlanets.Count });

                    // Луны
                    var all_moons = universeSystems.SelectMany(x => x.planets ?? new List<UniverseSystemInfoResult.Planet>()).SelectMany(planet => planet.moons ?? new List<int>(), (planet, moon_id) => new { planet.planet_id, moon_id }).ToList();
                    var universeMoons = new ConcurrentDictionary<int, ValueTuple<int, UniverseMoonInfoResult>>();
                    //await all_moons.ParallelForEachAsync(async moon =>
                    Parallel.ForEach(all_moons, new ParallelOptions() { MaxDegreeOfParallelism = 6 }, moon =>
                    {
                        if (moon?.moon_id > 0)
                        {
                            // Запрос к коннектору 
                            RequestResult<UniverseMoonInfoResult> request = null;
                            do
                            {
                                request = EsiConnector(esiClient.Universe.Moon, moon.moon_id);
                            } while (!request.isSuccess);
                            if (request.isSuccess)
                                universeMoons.TryAdd(moon.moon_id, (moon.planet_id, request.Data));
                        }
                    });
                    _repoPublicCommon.Universe_Moons_AddNew(universeMoons);
                    ToConsole($"Moons: {universeMoons.Count}");
                    _jobResult.subValues.Add(new JobResult.Item() { Name = "Moons", Value = universeMoons.Count });

                    // Астероидные пояса
                    var all_asteroid_belts = universeSystems.SelectMany(x => x.planets ?? new List<UniverseSystemInfoResult.Planet>()).SelectMany(planet => planet.asteroid_belts ?? new List<int>(), (planet, asteroid_belt_id) => new { planet.planet_id, asteroid_belt_id }).ToList();
                    var universeAsteroidBelts = new ConcurrentDictionary<int, ValueTuple<int, UniverseAsteroidBeltInfoResult>>();
                    //await all_asteroid_belts.ParallelForEachAsync(async asteroid_belt =>
                    Parallel.ForEach(all_asteroid_belts, new ParallelOptions() { MaxDegreeOfParallelism = 6 }, asteroid_belt =>
                    {
                        if (asteroid_belt?.asteroid_belt_id > 0)
                        {
                            // Запрос к коннектору 
                            RequestResult<UniverseAsteroidBeltInfoResult> request = null;
                            do
                            {
                                request = EsiConnector(esiClient.Universe.AsteroidBelt, asteroid_belt.asteroid_belt_id);
                            } while (!request.isSuccess);

                            if (request.isSuccess)
                                universeAsteroidBelts.TryAdd(asteroid_belt.asteroid_belt_id, (asteroid_belt.planet_id, request.Data));
                        }
                    });
                    _repoPublicCommon.Universe_AsteroidBelts_AddNew(universeAsteroidBelts);
                    ToConsole($"AsteroidBelts: {universeAsteroidBelts.Count}");
                    _jobResult.subValues.Add(new JobResult.Item() { Name = "AsteroidBelts", Value = universeAsteroidBelts.Count });
                }
            }
        }
    }
}
