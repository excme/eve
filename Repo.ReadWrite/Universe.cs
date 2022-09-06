using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite.IntegrationEvents;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        public List<int> Universe_Types_Ids(Expression<Func<EveOnlineUniverseType, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_UniverseTypes.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return source.Select(x => x.type_id).ToList();
        }

        public void Universe_Types_RemoveBroked()
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            _eveOnlinePublicContext.Eveonline_UniverseTypes
                .Where(x => x.enname == null || x.frname == null || x.dename == null || x.runame == null || x.janame == null || x.koname == null || (x.img_tags == null && x.published))
                .Where(x => x.type_id > 0)
                .DeleteFromQuery();
        }

        public int Universe_Types_AddOrUpdate(List<EveOnlineUniverseType> types)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            List<int> db_type_ids = _eveOnlinePublicContext.Eveonline_UniverseTypes.Select(x => x.type_id).ToList();

            foreach (EveOnlineUniverseType type in types)
            {
                if (type.type_id < 1)
                    continue;

                if (db_type_ids.Any(x => x == type.type_id))
                {
                    var db_type = _eveOnlinePublicContext.Eveonline_UniverseTypes.FirstOrDefault(x => x.type_id == type.type_id);
                    db_type.UpdateProperties(type);
                    _eveOnlinePublicContext.Eveonline_UniverseTypes.Update(type);
                }
                else
                {
                    _eveOnlinePublicContext.Eveonline_UniverseTypes.Add(type);
                }
            }

            return _eveOnlinePublicContext.SaveChanges();
        }

        public bool Universe_Types_IsPublished(int type_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            return (_eveOnlinePublicContext.Eveonline_UniverseTypes.Select(x => new { x.type_id, x.published }).FirstOrDefault(x => x.type_id == type_id))?.published ?? false;
        }

        public List<long> Universe_Structures_Ids(
            Expression<Func<EveOnlineUniverseLocation, bool>> where = null){

            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.EveOnlineUniverseLocations
                .Where(x => x.type == EUniverseLocationType.Structure)
                .AsQueryable();


            if (where != null)
                source = source.Where(where);

            return source.Select(x => x.id).ToList();
        }

        public void Universe_Structures_AddOrUpdate(long structure_id, UniverseStructureInfoResult data)
        {
            using var _context = new PublicContext(_options);
            var db_structure =  _context.EveOnlineUniverseLocations.FirstOrDefault(x => x.id == structure_id);
            if(db_structure == null)
            {
                var system_info =  _context.EveOnlineUniverseLocations.FirstOrDefault(x => x.id == data.solar_system_id);
                if (system_info != null)
                {
                    var new_location = new EveOnlineUniverseLocation()
                    {
                        id = structure_id,
                        system_id = data.solar_system_id,
                        constellation_id = system_info.constellation_id,
                        security_status = system_info.security_status,
                        region_id = system_info.region_id,
                        name = data.name,
                        owner_id = data.owner_id,
                        parent_id = data.solar_system_id,
                        position = data.position,
                        type_id = data.type_id,
                        type = EUniverseLocationType.Structure,
                    };

                     _context.EveOnlineUniverseLocations.Add(new_location);
                     _context.SaveChanges();

                    // Уведомление подписчиков
                    var @event = new UniverseStructureAfterAddIntergrationEvent(structure_id);
                    _eventBus.Publish(@event);
                }
            }
            else
            {
                // Проверка на изменение имени
                if(db_structure.name != data.name)
                {
                    db_structure.name = data.name;
                    _context.Entry(db_structure).Property(p => p.name).IsModified = true;
                     _context.SaveChanges();

                    // Уведомление подписчиков
                    var @event = new UniverseStructureChangeNameIntergrationEvent(structure_id);
                    _eventBus.Publish(@event);
                }

                // Проверка на изменнеи владельца
                if (db_structure.owner_id != data.owner_id)
                {
                    db_structure.owner_id = data.owner_id;
                    _context.Entry(db_structure).Property(p => p.owner_id).IsModified = true;
                     _context.SaveChanges();

                    // Уведомление подписчиков
                    var @event = new UniverseStructureChangeOwnerIntergrationEvent(structure_id);
                    _eventBus.Publish(@event);
                }
            }
        }

        public List<int> Universe_Categories_Ids(Expression<Func<EveOnlineUniverseCategory, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_UniverseCategories.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.category_id).ToList();
        }

        public int Universe_Categories_AddOrUpdate(List<EveOnlineUniverseCategory> categories)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineUniverseCategory category in categories)
            {
                if (category.category_id == 0)
                    continue;

                var db_category =  _eveOnlinePublicContext.Eveonline_UniverseCategories.FirstOrDefault(x => x.category_id == category.category_id);

                if (db_category == null)
                {
                     _eveOnlinePublicContext.Eveonline_UniverseCategories.Add(category);
                }
                else
                {
                    db_category.UpdateProperties(category);
                    _eveOnlinePublicContext.Eveonline_UniverseCategories.Update(db_category);
                }

            }

             return _eveOnlinePublicContext.SaveChanges();
        }

        public List<int> Universe_Graphics_Ids(Expression<Func<EveOnlineUniverseGraphic, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_UniverseGraphics.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.graphic_id).ToList();
        }
        public void Universe_Graphics_AddOrUpdate(List<EveOnlineUniverseGraphic> graphics)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineUniverseGraphic graphic in graphics)
            {
                var db_graphic =  _eveOnlinePublicContext.Eveonline_UniverseGraphics.FirstOrDefault(x => x.graphic_id == graphic.graphic_id);

                if (db_graphic == null)
                {
                     _eveOnlinePublicContext.Eveonline_UniverseGraphics.Add(graphic);
                }
                else
                {
                    db_graphic.UpdateProperties(graphic);
                    _eveOnlinePublicContext.Eveonline_UniverseGraphics.Update(db_graphic);
                }
            }

             _eveOnlinePublicContext.SaveChanges();
        }

        public List<int> Universe_Groups_Ids(Expression<Func<EveOnlineUniverseGroup, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_UniverseGroups.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.group_id).ToList();
        }
        public int Universe_Groups_AddOrUpdate(List<EveOnlineUniverseGroup> groups)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineUniverseGroup group in groups)
            {
                if (group.group_id == 0)
                    continue;

                var db_type =  _eveOnlinePublicContext.Eveonline_UniverseGroups.FirstOrDefault(x => x.group_id == group.group_id);

                if (db_type == null)
                {
                     _eveOnlinePublicContext.Eveonline_UniverseGroups.Add(group);
                }
                else
                {
                    //continue;
                    db_type.UpdateProperties(group);
                    _eveOnlinePublicContext.Eveonline_UniverseGroups.Update(db_type);
                }
            }
            
            return _eveOnlinePublicContext.SaveChanges();
        }

        public void Universe_Bloodlines_AddNew(UniverseBloodlinesResult data)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var bloodline_ids =  _eveOnlinePublicContext.Eveonline_UniverseBloodLines.Select(x => x.bloodline_id).ToList();
            var bloodlines_to_add = data.Where(x => !bloodline_ids.Contains(x.bloodline_id)).ToList();
            List<EveOnlineUniverseBloodLine> toAdd = bloodlines_to_add.Select(x =>
            {
                var item = new EveOnlineUniverseBloodLine();
                item.UpdateProperties(x);
                return item;
            }).ToList();

            _eveOnlinePublicContext.Eveonline_UniverseBloodLines.AddRange(toAdd);
             _eveOnlinePublicContext.SaveChanges();
        }
        public List<int> Universe_Bloodlines_Ids(Expression<Func<EveOnlineUniverseBloodLine, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_UniverseBloodLines.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.bloodline_id).ToList();
        }
        public int Universe_Bloodlines_AddOrUpdate(List<EveOnlineUniverseBloodLine> bllodlines)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineUniverseBloodLine bLine in bllodlines)
            {
                var db_type =  _eveOnlinePublicContext.Eveonline_UniverseBloodLines.FirstOrDefault(x => x.bloodline_id == bLine.bloodline_id);

                if (db_type == null)
                {
                     _eveOnlinePublicContext.Eveonline_UniverseBloodLines.Add(bLine);
                }
                else
                {
                    //continue;
                    db_type.UpdateProperties(bLine);
                    _eveOnlinePublicContext.Eveonline_UniverseBloodLines.Update(db_type);
                }

            }
            return _eveOnlinePublicContext.SaveChanges();
        }
        public int Universe_Factions_AddOrUpdate(List<EveOnlineUniverseFaction> fractions)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineUniverseFaction faction in fractions)
            {
                var db_type =  _eveOnlinePublicContext.Eveonline_UniverseFactions.FirstOrDefault(x => x.faction_id == faction.faction_id);

                if (db_type == null)
                {
                     _eveOnlinePublicContext.Eveonline_UniverseFactions.Add(faction);
                }
                else
                {
                    //continue;
                    db_type.UpdateProperties(faction);
                    _eveOnlinePublicContext.Eveonline_UniverseFactions.Update(db_type);
                }
            }

            return _eveOnlinePublicContext.SaveChanges();
        }
        public List<int> Universe_Factions_Ids(Expression<Func<EveOnlineUniverseFaction, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_UniverseFactions.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.faction_id).ToList();
        }
        public List<int> Universe_Races_Ids(Expression<Func<EveOnlineUniverseRace, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_UniverseRaces.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.race_id).ToList();
        }
        public int Universe_Races_AddOrUpdate(List<EveOnlineUniverseRace> races)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineUniverseRace race in races)
            {
                var db_type =  _eveOnlinePublicContext.Eveonline_UniverseRaces.FirstOrDefault(x => x.race_id == race.race_id);

                if (db_type == null)
                {
                     _eveOnlinePublicContext.Eveonline_UniverseRaces.Add(race);
                }
                else
                {
                    //continue;
                    db_type.UpdateProperties(race);
                    _eveOnlinePublicContext.Eveonline_UniverseRaces.Update(db_type);
                }

                 _eveOnlinePublicContext.SaveChanges();
            }

            return _eveOnlinePublicContext.SaveChanges();
        }
        public List<int> Universe_Ancestries_Ids(Expression<Func<EveOnlineUniverseAncestry, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.EveOnlineUniverseAncestries.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.id).ToList();
        }
        public void Universe_Ancestries_AddOrUpdate(List<EveOnlineUniverseAncestry> ancestries)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineUniverseAncestry ancestry in ancestries)
            {
                var db_type =  _eveOnlinePublicContext.EveOnlineUniverseAncestries.FirstOrDefault(x => x.id == ancestry.id);

                if (db_type == null)
                {
                     _eveOnlinePublicContext.EveOnlineUniverseAncestries.Add(ancestry);
                }
                else
                {
                    //continue;
                    db_type.UpdateProperties(ancestry);
                    _eveOnlinePublicContext.EveOnlineUniverseAncestries.Update(db_type);
                }

                 _eveOnlinePublicContext.SaveChanges();
            }
        }
        public List<long> Universe_Locations_Ids(
            EUniverseLocationType locationType = EUniverseLocationType.Unknown,
            Expression<Func<EveOnlineUniverseLocation, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.EveOnlineUniverseLocations.AsQueryable();

            if (locationType != EUniverseLocationType.Unknown)
                source = source.Where(x => x.type == locationType);

            if (where != null)
                source = source.Where(where);

            return  source.Select(x => x.id).ToList();
        }
        public void Universe_Regions_AddNew(List<UniverseRegionInfoResult> new_regions)
        {
            using var _context = new PublicContext(_options);

            List<EveOnlineUniverseLocation> to_Add = new_regions.Select(x =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = x.region_id.ToInt64(),
                    name = x.name,
                    type = EUniverseLocationType.Region,
                    regionInfo = x
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Region).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();

            // Уведомление подписчиков
            foreach (var new_region in to_Add)
                _eventBus.Publish(new UniverseRegionUpdatedIntegrationEvent(new_region.id));
        }
        public void Universe_Constellations_AddNew(List<UniverseConstellationInfoResult> new_constellations)
        {
            using var _context = new PublicContext(_options);

            List<EveOnlineUniverseLocation> to_Add = new_constellations.Select(x =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = x.constellation_id.ToInt64(),
                    type = EUniverseLocationType.Constellation,
                    parent_id = x.region_id.ToInt64(),
                    region_id = x.region_id.ToInt64(),
                    name = x.name,
                    position = x.position,
                    constellationInfo = x
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Constellation).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public List<UniverseSystemInfoResult> Universe_Systems_Infos()
        {
            using var _context = new PublicContext(_options);
            return  _context.EveOnlineUniverseLocations
                .Where(x => x.type == EUniverseLocationType.System)
                .Select(x => x.systemInfo)
                .ToList();
        }
        public void Universe_Systems_AddNew(List<UniverseSystemInfoResult> new_systems)
        {
            using var _context = new PublicContext(_options);
            var to_remove =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System && !new_systems.Select(x => x.system_id.ToInt64()).Contains(x.id)).Select(x => new { x.id, x.region_id }).ToList();
            var cons_to_regions =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Constellation).Select(x => new { x.id, x.region_id }).ToList();

            List<EveOnlineUniverseLocation> to_Add = new_systems.Select(x =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = x.system_id.ToInt64(),
                    type = EUniverseLocationType.System,
                    security_status = x.security_status,
                    name = x.name,
                    parent_id = x.constellation_id.ToInt64(),
                    constellation_id = x.constellation_id.ToInt64(),
                    region_id = cons_to_regions.FirstOrDefault(xx => xx.id == x.constellation_id.ToInt64())?.region_id,
                    systemInfo = x
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public void Universe_Stars_AddNew(ConcurrentDictionary<int, UniverseStarInfoResult> stars)
        {
            using var _context = new PublicContext(_options);
            var systems_to_const_regions =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System).Select(x => new { system_id = x.id, x.constellation_id, x.region_id, x.security_status }).ToList();

            List<EveOnlineUniverseLocation> to_Add = stars.Select(star =>
                new EveOnlineUniverseLocation()
                {
                    id = star.Key.ToInt64(),
                    name = star.Value.name,
                    security_status = systems_to_const_regions.FirstOrDefault(x => x.system_id == star.Value.solar_system_id.ToInt64())?.security_status,
                    type = EUniverseLocationType.Star,
                    region_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == star.Value.solar_system_id.ToInt64())?.region_id,
                    constellation_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == star.Value.solar_system_id.ToInt64())?.constellation_id,
                    parent_id = star.Value.solar_system_id.ToInt64(),
                    system_id = star.Value.solar_system_id.ToInt64(),
                    starInfo = star.Value,
                }
            ).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Star).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public void Universe_Stargates_AddNew(ConcurrentDictionary<int, UniverseStargateInfoResult> stargates)
        {
            using var _context = new PublicContext(_options);
            var systems_to_const_regions =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System).Select(x => new { system_id = x.id, x.constellation_id, x.region_id, x.security_status }).ToList();

            List<EveOnlineUniverseLocation> to_Add = stargates.Select(stargate =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = stargate.Key.ToInt64(),
                    name = stargate.Value.name,
                    type = EUniverseLocationType.Stargate,
                    security_status = systems_to_const_regions.FirstOrDefault(x => x.system_id == stargate.Value.system_id.ToInt64())?.security_status,
                    region_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == stargate.Value.system_id.ToInt64())?.region_id,
                    constellation_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == stargate.Value.system_id.ToInt64())?.constellation_id,
                    parent_id = stargate.Value.system_id.ToInt64(),
                    system_id = stargate.Value.system_id.ToInt64(),
                    stargateInfo = stargate.Value,
                    position = stargate.Value.position
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Stargate).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public void Universe_Stations_AddNew(ConcurrentDictionary<int, UniverseStationInfoResult> stations)
        {
            using var _context = new PublicContext(_options);
            var systems_to_const_regions =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System).Select(x => new { system_id = x.id, x.constellation_id, x.region_id, x.security_status }).ToList();

            List<EveOnlineUniverseLocation> to_Add = stations.Select(station =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = station.Key.ToInt64(),
                    name = station.Value.name,
                    type = EUniverseLocationType.Station,
                    security_status = systems_to_const_regions.FirstOrDefault(x => x.system_id == station.Value.system_id.ToInt64())?.security_status,
                    region_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == station.Value.system_id.ToInt64())?.region_id,
                    constellation_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == station.Value.system_id.ToInt64())?.constellation_id,
                    parent_id = station.Value.system_id.ToInt64(),
                    system_id = station.Value.system_id.ToInt64(),
                    stationInfo = station.Value,
                    position = station.Value.position
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Station).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public void Universe_Planets_AddNew(ConcurrentDictionary<int, UniversePlanetInfoResult> planets)
        {
            using var _context = new PublicContext(_options);
            var systems_to_const_regions =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System).Select(x => new { system_id = x.id, x.constellation_id, x.region_id, x.security_status }).ToList();

            List<EveOnlineUniverseLocation> to_Add = planets.Select(planet =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = planet.Key.ToInt64(),
                    name = planet.Value.name,
                    type = EUniverseLocationType.Planet,
                    security_status = systems_to_const_regions.FirstOrDefault(x => x.system_id == planet.Value.system_id.ToInt64())?.security_status,
                    region_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == planet.Value.system_id.ToInt64())?.region_id,
                    constellation_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == planet.Value.system_id.ToInt64())?.constellation_id,
                    parent_id = planet.Value.system_id.ToInt64(),
                    system_id = planet.Value.system_id.ToInt64(),
                    planetInfo = planet.Value,
                    position = planet.Value.position
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Planet).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public void Universe_Moons_AddNew(ConcurrentDictionary<int, ValueTuple<int, UniverseMoonInfoResult>> moons)
        {
            using var _context = new PublicContext(_options);
            var systems_to_const_regions =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System).Select(x => new { system_id = x.id, x.constellation_id, x.region_id, x.security_status }).ToList();

            List<EveOnlineUniverseLocation> to_Add = moons.Select(moon =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = moon.Key.ToInt64(),
                    name = moon.Value.Item2.name,
                    type = EUniverseLocationType.Moon,
                    security_status = systems_to_const_regions.FirstOrDefault(x => x.system_id == moon.Value.Item2.system_id.ToInt64())?.security_status,
                    region_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == moon.Value.Item2.system_id.ToInt64())?.region_id,
                    constellation_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == moon.Value.Item2.system_id.ToInt64())?.constellation_id,
                    parent_id = moon.Value.Item1.ToInt64(),
                    system_id = moon.Value.Item2.system_id.ToInt64(),
                    moonInfo = moon.Value.Item2,
                    position = moon.Value.Item2.position,
                    planet_id = moon.Value.Item1.ToInt64()
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Moon).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public void Universe_AsteroidBelts_AddNew(ConcurrentDictionary<int, ValueTuple<int, UniverseAsteroidBeltInfoResult>> asteroid_belts)
        {
            using var _context = new PublicContext(_options);
            var systems_to_const_regions =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.System).Select(x => new { system_id = x.id, x.constellation_id, x.region_id, x.security_status }).ToList();

            List<EveOnlineUniverseLocation> to_Add = asteroid_belts.Select(asteroid_belt =>
            {
                return new EveOnlineUniverseLocation()
                {
                    id = asteroid_belt.Key.ToInt64(),
                    name = asteroid_belt.Value.Item2.name,
                    type = EUniverseLocationType.AsteroidBelt,
                    security_status = systems_to_const_regions.FirstOrDefault(x => x.system_id == asteroid_belt.Value.Item2.system_id.ToInt64())?.security_status,
                    region_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == asteroid_belt.Value.Item2.system_id.ToInt64())?.region_id,
                    constellation_id = systems_to_const_regions.FirstOrDefault(x => x.system_id == asteroid_belt.Value.Item2.system_id.ToInt64())?.constellation_id,
                    parent_id = asteroid_belt.Value.Item1.ToInt64(),
                    system_id = asteroid_belt.Value.Item2.system_id.ToInt64(),
                    asteroidBeltInfo = asteroid_belt.Value.Item2,
                    position = asteroid_belt.Value.Item2.position,
                    planet_id = asteroid_belt.Value.Item1.ToInt64()
                };
            }).ToList();

            // Определение что на добавление, а что на обновление
            var db_ids =  _context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.AsteroidBelt).Select(x => x.id).ToList();
             _context.EveOnlineUniverseLocations.AddRange(to_Add.Where(x => !db_ids.Contains(x.id)));
            _context.EveOnlineUniverseLocations.UpdateRange(to_Add.Where(x => db_ids.Contains(x.id)));
             _context.SaveChanges();
        }
        public List<EveOnlineUniverseLocation> Universe_InnerLocations(long solar_system_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            return _eveOnlinePublicContext.EveOnlineUniverseLocations.Where(x => x.system_id == solar_system_id).ToList();
        }
    }
}
