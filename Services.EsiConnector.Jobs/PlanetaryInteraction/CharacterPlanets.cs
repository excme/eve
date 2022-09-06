using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterPlanets : ConnectorJob
    {
        //static string l_reqName = "Character_Planets";
        //static string l_scope = Scope.Planets.ManagePlanets.Name;
        //static ERequestFolder l_folder = ERequestFolder.PlanetaryInteraction;
        //public CharacterPlanets() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterPlanets(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterPlanetsResult, CharacterPlanetsResult.CharacterPlanetItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.PlanetaryInteraction.GetPlanets, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterPlanet, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterPlanet, bool>(x => !ConnectorResult.items.Any(xx => xx.planet_id == x.planet_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            foreach (var planet in ConnectorResult.items ?? new CharacterPlanetsResult())
        //            {
        //                // Добавление и изменение из обновления
        //                var predicate = new Func<EveOnlineCharacterPlanet, bool>(x => x.planet_id == planet.planet_id);
        //                var newValue = new EveOnlineCharacterPlanet() { character_id = sso.character_id, planet_id = planet.planet_id };
        //                GenericOperations.UpdateItem(planet, db_values.items, predicate, newValue, _dbContext);

        //                // Запрос доп инфомарции
        //                var ConnectorResult1 = SsoOneItem<CharactersPlanetColonyResult, int>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.PlanetaryInteraction.GetPlanetInfo, sso.character_id, planet.planet_id, folder, jobName);

        //                if (ConnectorResult1.success)
        //                {
        //                    // Линк
        //                    dbChanges += UpdateLinks(sso.character_id, planet.planet_id, ConnectorResult1.item.links, _dbContext);

        //                    // Пин
        //                    dbChanges += UpdatePins(sso.character_id, planet.planet_id, ConnectorResult1.item.pins, _dbContext);

        //                    // Роут
        //                    dbChanges += UpdateRoutes(sso.character_id, planet.planet_id, ConnectorResult1.item.routes, _dbContext);
        //                }
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterPlanets, ConnectorResult.items?.Count ?? 0, dbChanges);
        //}

        //int UpdateLinks(int character_id, int planet_id, List<CharactersPlanetColonyResult.Link> links, EveContextDbContext _dbContext)
        //{
        //    // Удаление неактуальных
        //    var dbPredicate = new Func<EveOnlineCharacterPlanet.Link, bool>(x => x.character_id == character_id && x.planet_id == planet_id);
        //    var toRemovePredicate = new Func<EveOnlineCharacterPlanet.Link, bool>(x => !links.Any(xx => xx.destination_pin_id == x.destination_pin_id && xx.source_pin_id == x.source_pin_id));
        //    var db_values = GenericOperations.RemoveNotActual(dbPredicate, /*links,*/ toRemovePredicate, _dbContext);

        //    // Добавлени и изменение из обновления
        //    foreach (var source_link in links ?? new List<CharactersPlanetColonyResult.Link>())
        //    {
        //        var predicate = new Func<EveOnlineCharacterPlanet.Link, bool>(x => x.destination_pin_id == source_link.destination_pin_id && x.source_pin_id == source_link.source_pin_id);
        //        var newValue = new EveOnlineCharacterPlanet.Link() { character_id = character_id, planet_id = planet_id };
        //        GenericOperations.UpdateItem(source_link, db_values.items, predicate, newValue, _dbContext);
        //    }

        //    return db_values.changes;
        //}
        //int UpdateRoutes(int character_id, int planet_id, List<CharactersPlanetColonyResult.Route> routes, EveContextDbContext _dbContext)
        //{
        //    // Удаление неактуальных
        //    var dbPredicate = new Func<EveOnlineCharacterPlanet.Route, bool>(x => x.character_id == character_id && x.planet_id == planet_id);
        //    var toRemovePredicate = new Func<EveOnlineCharacterPlanet.Route, bool>(x => !routes.Any(xx => xx.route_id == x.route_id));
        //    var db_values = GenericOperations.RemoveNotActual(dbPredicate, /*routes,*/ toRemovePredicate, _dbContext);

        //    // Добавлени и изменение из обновления
        //    foreach (var source_route in routes ?? new List<CharactersPlanetColonyResult.Route>())
        //    {
        //        var predicate = new Func<EveOnlineCharacterPlanet.Route, bool>(x => x.route_id == source_route.route_id);
        //        var newValue = new EveOnlineCharacterPlanet.Route() { character_id = character_id, planet_id = planet_id };

        //        GenericOperations.UpdateItem(source_route, db_values.items, predicate, newValue, _dbContext);
        //    }

        //    return db_values.changes;
        //}
        //int UpdatePins(int character_id, int planet_id, List<CharactersPlanetColonyResult.Pin> pins, EveContextDbContext _dbContext)
        //{
        //    // Удаление неактуальных
        //    var dbPredicate = new Func<EveOnlineCharacterPlanet.Pin, bool>(x => x.character_id == character_id && x.planet_id == planet_id);
        //    var toRemovePredicate = new Func<EveOnlineCharacterPlanet.Pin, bool>(x => !pins.Any(xx => xx.pin_id == x.pin_id));
        //    var db_values = GenericOperations.RemoveNotActual(dbPredicate/*, pins*/, toRemovePredicate, _dbContext);

        //    // Добавлени и изменение из обновления
        //    foreach (var source_pin in pins ?? new List<CharactersPlanetColonyResult.Pin>())
        //    {
        //        var predicate = new Func<EveOnlineCharacterPlanet.Pin, bool>(x => x.pin_id == source_pin.pin_id);
        //        var newValue = new EveOnlineCharacterPlanet.Pin() { character_id = character_id, planet_id = planet_id };

        //        GenericOperations.UpdateItem(source_pin, db_values.items, predicate, newValue, _dbContext);
        //        if (source_pin.schematic_id > 0)
        //            AddSchematic(source_pin.schematic_id, _dbContext);
        //    }

        //    return db_values.changes;
        //}
        //List<EveOnlineUniverseSchematic> schematics;
        //void AddSchematic(int schematic_id, EveContextDbContext _dbContext)
        //{
        //    if(schematics == null || !schematics.Any())
        //        schematics = _dbContext.Eveonline_UniverseSchematics.Local.Any() ? _dbContext.Eveonline_UniverseSchematics.Local.ToList() : _dbContext.Eveonline_UniverseSchematics.ToList();

        //    var schematic = schematics.FirstOrDefault(x => x.schematic_id == schematic_id);
        //    if (schematic == null)
        //    {
        //        Func<Task<EsiResponse>> запросКоннектора4 = new Func<Task<EsiResponse>>(connector.PlanetaryInteraction.GetSchematicInfo(schematic_id).ExecuteAsync);
        //        var ConnectorResult4 = _eveOnlineGeneric.ExecuteRequest<UniverseSchematicInfoResult>(запросКоннектора4, ERequestFolder.PlanetaryInteraction, UniverseSchematicInfoResult.TimeExpire(), UniverseSchematicInfoResult.GetArgs(schematic_id)).GetAwaiter().GetResult();

        //        if (ConnectorResult4.success && ConnectorResult4.value != null)
        //        {
        //            schematic = new EveOnlineUniverseSchematic() { schematic_id = schematic_id };
        //            schematic.UpdateProperties(ConnectorResult4.value);
        //            _dbContext.Eveonline_UniverseSchematics.Add(schematic);
        //            schematics.Clear();
        //        }
        //    }
        //}
    }
}