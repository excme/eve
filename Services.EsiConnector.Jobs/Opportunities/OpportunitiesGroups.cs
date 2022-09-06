using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class OpportunitiesGroups : ConnectorJob
    {
        //static string l_reqName = "Opportunities_Groups";
        //static ERequestFolder l_folder = ERequestFolder.Opportunities;
        //public OpportunitiesGroups() : base(l_reqName, l_folder, _withSso: false) { }
        //public OpportunitiesGroups(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, _withSso: false)
        //{
        //    _itemToUpdate = maxCharactersToUpdate;
        //}
        //public void Opportunities_GroupIds()
        //{
        //    string reqName = "Opportunities_GroupIds";
        //    ERequestFolder folder = ERequestFolder.Opportunities;

        //    // Выполнение запроса
        //    var request = _eveOnlineGeneric.ExecuteRequest<OpportunitiesGroupsResult>(connector.Opportunities.GetGroups().ExecuteAsync, folder, OpportunitiesGroupsResult.TimeExpire(), OpportunitiesGroupsResult.GetArgs()).GetAwaiter().GetResult();
        //    if (request.success)
        //    {
        //        var ids = request.value.ToList();
        //        if (_itemToUpdate > 0)
        //            ids = ids.Take(_itemToUpdate).ToList();

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // На удаление
        //            var to_del = _dbContext.Eveonline_OpportunityGroups.Where(x => !ids.Contains(x.group_id));
        //            _dbContext.Eveonline_OpportunityGroups.RemoveRange(to_del);
        //            // На обновление
        //            var to_update = _dbContext.Eveonline_OpportunityGroups.Where(x => ids.Contains(x.group_id));
        //            foreach (var update_item in to_update)
        //            {
        //                var update = Opportunities_Group_UpdateInfo(update_item.group_id);
        //                update_item.UpdateProperties(update);
        //            }
        //            // На добавление
        //            ids.RemoveAll(x => to_update.Any(xx => xx.group_id == x));
        //            if (ids.Count > 0)
        //            {
        //                _dbContext.Eveonline_OpportunityGroups.AddRange(ids.Select(x => {
        //                    var opportinityAttr = new EveOnlineOpportunityGroup() { group_id = x };
        //                    var update = Opportunities_Group_UpdateInfo(x);
        //                    opportinityAttr.UpdateProperties(update);
        //                    return opportinityAttr;
        //                }));
        //            }

        //            _dbContext.SaveChanges();
        //        }
        //    }
        //}
        //OpportunitiesGroupInfoResult Opportunities_Group_UpdateInfo(int group_id)
        //{
        //    string reqName = "Opportunities_GroupInfo";

        //    // Запрос к коннектору
        //    var groupInfoReq = _eveOnlineGeneric.ExecuteRequest<OpportunitiesGroupInfoResult>(connector.Opportunities.GetGroupInfo(group_id).ExecuteAsync, folder, OpportunitiesGroupInfoResult.TimeExpire(), OpportunitiesGroupInfoResult.GetArgs(group_id)).GetAwaiter().GetResult();

        //    return groupInfoReq.value;
        //}
        ////public void Opportunities_GroupUpdates(bool useParallel = false)
        ////{
        ////    string reqName = "Opportunities_GroupUpdates";
        ////    ERequestFolder folder = ERequestFolder.Opportunities;

        ////    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        ////    {
        ////        var allGroupIds = _dbContext.EveOnlineOpportunityGroups.Select(x => new { guid = x.Id, id = x.group_id }).ToList();

        ////        Action<Guid> action = new Action<Guid>((guid) => {
        ////            _eveOnlineGeneric.Opportunity_Group_UpdateInfo(guid);
        ////        });

        ////        if (useParallel)
        ////        {
        ////            Parallel.ForEach(allGroupIds, value => {

        ////                _eveOnlineGeneric.Generic_СтандартныйМетодОбновления(value.guid, reqName, folder, OpportunitiesGroupInfoResult.GetArgs(value.id), OpportunitiesGroupInfoResult.TimeExpire(), action, _dbContext);
        ////            });
        ////        }
        ////        else
        ////        {
        ////            allGroupIds.ForEach(value => {

        ////                _eveOnlineGeneric.Generic_СтандартныйМетодОбновления(value.guid, reqName, folder, OpportunitiesGroupInfoResult.GetArgs(value.id), OpportunitiesGroupInfoResult.TimeExpire(), action, _dbContext);
        ////            });
        ////        }
        ////    }
        ////}

        //public override void TaskJob()
        //{
        //    Opportunities_GroupIds();
        //    //Opportunities_GroupUpdates();
        //}
    }
}
