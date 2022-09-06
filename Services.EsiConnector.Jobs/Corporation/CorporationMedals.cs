using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationMedals : ConnectorJob
    {
        //static string l_reqName = "Corporation_Medals";
        //static string l_scope = Scope.Corporations.ReadMedals.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationMedals() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationMedals(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //void Medals(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationMedalsResult, CorporationMedalsResult.CorporationMedalsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetMedals, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationMedal, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationMedal, bool>(x => !ConnectorResult.items.Any(xx => xx.medal_id == x.medal_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var medal in ConnectorResult.items ?? new CorporationMedalsResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationMedal, bool>(x => x.medal_id == medal.medal_id);
        //                var newValue = new EveOnlineCorporationMedal() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(medal, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationMedals, ConnectorResult.items.Count, dbChanges);
        //}
        //void MedalsIssued(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;

        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationMedalsIssuedResult, CorporationMedalsIssuedResult.CorporationMedalsIssuedItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetMedalsIssued, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationMedalIssued, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationMedalIssued, bool>(x => !ConnectorResult.items.Any(xx => xx.medal_id == x.medal_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var medalIssued in ConnectorResult.items ?? new CorporationMedalsIssuedResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationMedalIssued, bool>(x => x.medal_id == medalIssued.medal_id && x.character_id == medalIssued.character_id && x.issuer_id == medalIssued.issuer_id);
        //                var newValue = new EveOnlineCorporationMedalIssued() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(medalIssued, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationMedalsIssued, ConnectorResult.items.Count, dbChanges);
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    Medals(sso);
        //    MedalsIssued(sso);
            
        //    // Выполнение запрос контрактов
        //    //var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData); int prevPage = 0; var isSuccess = false;
        //    //(CorporationMedalsResult value, bool success, DateTime expireOn, string message) tempConnectorResult;
        //    //var ConnectorResult = new CorporationMedalsResult();
        //    //do
        //    //{
        //    //    prevPage++;
        //    //    Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Corporation.GetMedals(sso.corporation_id, prevPage).ExecuteAsync);
        //    //    tempConnectorResult = _eveOnlineGeneric.ExecuteRequest<CorporationMedalsResult>(запросКоннектора1, folder, CorporationMedalsResult.TimeExpire(), CorporationMedalsResult.GetArgs(sso.corporation_id)).GetAwaiter().GetResult();
        //    //    if (prevPage == 1) isSuccess = tempConnectorResult.success;

        //    //    if (tempConnectorResult.value?.Count > 0) ConnectorResult.AddRange(tempConnectorResult.value);
        //    //} while (tempConnectorResult.value?.Count == 1000);

        //    //if (isSuccess)
        //    //{
        //    //    _eveOnlineGeneric.Sso_RequestStatistic(sso.corporation_id, ESsoRequestType.corporationMedals, ConnectorResult.Count);

        //    //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    //    {
        //    //        // Удаляем неактуальные связи с контрактами
        //    //        _dbContext.EveOnlineCorporationMedals.RemoveRange(_dbContext.EveOnlineCorporationMedals.Where(x => x.corporation_id == sso.corporation_id && !ConnectorResult.Select(xx => xx.medal_id).Any(y => y == x.medal_id)));
        //    //        _dbContext.SaveChanges();

        //    //        // Сохранение контрактов в бд
        //    //        foreach (var _medal in ConnectorResult)
        //    //        {
        //    //            // Добавление и обновление
        //    //            var db_value = _dbContext.EveOnlineCorporationMedals.FirstOrDefault(x => x.corporation_id == sso.corporation_id && x.medal_id == _medal.medal_id);
        //    //            if (db_value == null)
        //    //            {
        //    //                db_value = new EveOnlineCorporationMedal();
        //    //                db_value.UpdateProperties(_medal);
        //    //                _dbContext.EveOnlineCorporationMedals.Add(db_value);
        //    //            }
        //    //            else
        //    //            {
        //    //                db_value.UpdateProperties(_medal);
        //    //                _dbContext.EveOnlineCorporationMedals.Update(db_value);
        //    //            }
        //    //            _dbContext.SaveChanges();
        //    //        }

        //    //        // Запросы награжденных
        //    //        prevPage = 0; isSuccess = false;
        //    //        (CorporationMedalsIssuedResult value, bool success, DateTime expireOn, string message) tempConnectorResult1;
        //    //        var ConnectorResult1 = new CorporationMedalsIssuedResult();
        //    //        do
        //    //        {
        //    //            prevPage++;
        //    //            Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Corporation.GetMedalsIssued(sso.corporation_id, prevPage).ExecuteAsync);
        //    //            tempConnectorResult1 = _eveOnlineGeneric.ExecuteRequest<CorporationMedalsIssuedResult>(запросКоннектора1, folder, CorporationMedalsIssuedResult.TimeExpire(), CorporationMedalsIssuedResult.GetArgs(sso.corporation_id)).GetAwaiter().GetResult();
        //    //            if (prevPage == 1) isSuccess = tempConnectorResult1.success;

        //    //            if (tempConnectorResult1.value?.Count > 0) ConnectorResult1.AddRange(tempConnectorResult1.value);
        //    //        } while (tempConnectorResult1.value.Count == 1000);

        //    //        // Удаление по несуществующим медалям
        //    //        var medal_ids = ConnectorResult1.Select(xx => xx.medal_id).ToList();
        //    //        _dbContext.EveOnlineCorporationMedalIssueds.RemoveRange(_dbContext.EveOnlineCorporationMedalIssueds.Where(x => x.corporation_id == sso.corporation_id && !medal_ids.Any(y => y == x.medal_id)));
        //    //        _dbContext.SaveChanges();

        //    //        // Добавление и обновление награжденных медалями
        //    //        foreach (var medalIssued_byMedailId in ConnectorResult1.GroupBy(x => x.medal_id).ToList())
        //    //        {
        //    //            // Удаление удаленный награжденных
        //    //            _dbContext.EveOnlineCorporationMedalIssueds.Where(x => x.corporation_id == sso.corporation_id && x.medal_id == medalIssued_byMedailId.Key && !medalIssued_byMedailId.Select(xx => xx.character_id).Any(y => y == x.character_id));
        //    //            _dbContext.SaveChanges();

        //    //            foreach (var medailIssued in medalIssued_byMedailId)
        //    //            {
        //    //                // Добавление и обновление
        //    //                var db_value = _dbContext.EveOnlineCorporationMedalIssueds.FirstOrDefault(x => x.corporation_id == sso.corporation_id && x.medal_id == medailIssued.medal_id && x.character_id == medailIssued.character_id);
        //    //                if (db_value == null)
        //    //                {
        //    //                    db_value = new EveOnlineCorporationMedalIssued();
        //    //                    db_value.UpdateProperties(medailIssued);
        //    //                    _dbContext.EveOnlineCorporationMedalIssueds.Add(db_value);
        //    //                }
        //    //                else
        //    //                {
        //    //                    db_value.UpdateProperties(medailIssued);
        //    //                    _dbContext.EveOnlineCorporationMedalIssueds.Update(db_value);
        //    //                }
        //    //                _dbContext.SaveChanges();
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}
    }
}
