using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationIndustryMiningObservers : ConnectorJob
    {
        //static string l_reqName = "Corporation_IndustryMiningObservers";
        //static string l_scope = Scope.Industry.ReadCorporationMining.Name;
        //static ERequestFolder l_folder = ERequestFolder.Industry;
        //static string[] l_needed_roles = new string[] { "Accountant" };
        //public CorporationIndustryMiningObservers() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationIndustryMiningObservers(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { _maxCharactersToUpdate = maxCharactersToUpdate; }
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0, ssoResponses = 0;
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        // Выкачивание
        //        var ConnectorResult = SsoPaged<CorporationIndustryMiningObserversResult, CorporationIndustryMiningObserversResult.CorporationIndustryMiningObserversItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Industry.GetMiningObservers, sso.corporation_id, folder, jobName, 1000);

        //        if (ConnectorResult.success)
        //        {
        //            ssoResponses += ConnectorResult.items.Count;
        //            var db_values = _dbContext.Eveonline_CorporationIndustryMinigObservers.AsNoTracking().Where(x => x.corporation_id == sso.corporation_id).ToList();
        //            foreach (var observer in ConnectorResult.items ?? new List<CorporationIndustryMiningObserversResult.CorporationIndustryMiningObserversItem>())
        //            {
        //                // Обновление, добавление
        //                var predicate = new Func<EveOnlineCorporationIndustryMinigObserver, bool>(x => x.observer_id == observer.observer_id);
        //                var newValue = new EveOnlineCorporationIndustryMinigObserver() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(observer, db_values, predicate, newValue, _dbContext);

        //                // Детали
        //                var observer_details = SsoPaged<CorporationIndustryMiningObserverItemResult, CorporationIndustryMiningObserverItemResult.CorporationIndustryMiningObserverItemInner, long>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Industry.GetObservedMining, sso.corporation_id, observer.observer_id, folder, jobName, 1000);
        //                if(observer_details.success && observer_details.items.Any())
        //                {
        //                    ssoResponses += ConnectorResult.items.Count;
        //                    var db_details = _dbContext.Eveonline_CorporationIndustryMinigObserverDetails.AsNoTracking().Where(x => x.corporation_id == sso.corporation_id && x.observer_id == observer.observer_id).ToList();
        //                    foreach(var observer_detail in observer_details.items ?? new List<CorporationIndustryMiningObserverItemResult.CorporationIndustryMiningObserverItemInner>())
        //                    {
        //                        // Обновление, добавление
        //                        var predicate1 = new Func<EveOnlineCorporationIndustryMinigObserverDetail, bool>(x => x.observer_id == observer.observer_id && x.character_id == observer_detail.character_id && x.last_updated == observer_detail.last_updated && x.type_id == observer_detail.type_id);
        //                        var newValue1 = new EveOnlineCorporationIndustryMinigObserverDetail() { corporation_id = sso.corporation_id, observer_id = observer.observer_id };
        //                        GenericOperations.UpdateItem(observer_detail, db_details, predicate1, newValue1, _dbContext);
        //                    }
        //                }
        //            }
        //            dbChanges = _dbContext.SaveChanges();
        //        }

        //        AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationIndustryMiningObservers, ssoResponses, dbChanges);
        //    }
        //}
    }
}
