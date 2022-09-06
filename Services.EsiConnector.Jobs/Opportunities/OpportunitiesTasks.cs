using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class OpportunitiesTasks : ConnectorJob
    {
        //static string l_reqName = "Opportunities_Tasks";
        //static ERequestFolder l_folder = ERequestFolder.Opportunities;
        //public OpportunitiesTasks() : base(l_reqName, l_folder, _withSso: false) { }
        //public OpportunitiesTasks(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, _withSso: false)
        //{
        //    _itemToUpdate = maxCharactersToUpdate;
        //}
        //public void Opportunities_TaskIds()
        //{
        //    // Выполнение запроса
        //    var request = _eveOnlineGeneric.ExecuteRequest<OpportunitiesTasksResult>(connector.Opportunities.GetTasks().ExecuteAsync, folder, OpportunitiesTasksResult.TimeExpire(), OpportunitiesTasksResult.GetArgs()).GetAwaiter().GetResult();
        //    if (request.success)
        //    {
        //        var ids = request.value.ToList();
        //        if (_itemToUpdate > 0)
        //            ids = ids.Take(_itemToUpdate).ToList();

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // На удаление
        //            var to_del = _dbContext.Eveonline_OpportunityTasks.Where(x => !ids.Contains(x.task_id));
        //            _dbContext.Eveonline_OpportunityTasks.RemoveRange(to_del);
        //            // На обновление
        //            var to_update = _dbContext.Eveonline_OpportunityTasks.Where(x => ids.Contains(x.task_id));
        //            foreach (var update_item in to_update)
        //            {
        //                var update = Opportunities_Task_UpdateInfo(update_item.task_id);
        //                update_item.UpdateProperties(update);
        //            }
        //            // На добавление
        //            ids.RemoveAll(x => to_update.Any(xx => xx.task_id == x));
        //            if (ids.Count > 0)
        //            {
        //                _dbContext.Eveonline_OpportunityTasks.AddRange(ids.Select(x => {
        //                    var opportinityAttr = new EveOnlineOpportunityTask() { task_id = x };
        //                    var update = Opportunities_Task_UpdateInfo(x);
        //                    opportinityAttr.UpdateProperties(update);
        //                    return opportinityAttr;
        //                }));
        //            }

        //            _dbContext.SaveChanges();
        //        }
        //    }
        //}
        //OpportunitiesTaskInfoResult Opportunities_Task_UpdateInfo(int task_id)
        //{
        //    string reqName = "Opportunities_TaskInfo";

        //    // Запрос к коннектору
        //    var taskInfoReq = _eveOnlineGeneric.ExecuteRequest<OpportunitiesTaskInfoResult>(connector.Opportunities.GetTaskInfo(task_id).ExecuteAsync, folder, OpportunitiesTaskInfoResult.TimeExpire(), OpportunitiesTaskInfoResult.GetArgs(task_id)).GetAwaiter().GetResult();

        //    return taskInfoReq.value;
        //}

        ////public void Opportunities_TaskUpdates(bool useParallel = false)
        ////{
        ////    string reqName = "Opportunities_TaskUpdates";
        ////    ERequestFolder folder = ERequestFolder.Opportunities;

        ////    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        ////    {
        ////        var allTaskIDs = _dbContext.EveOnlineOpportunityTasks.Select(x => new { guid = x.Id, id = x.task_id }).ToList();

        ////        Action<Guid> action = new Action<Guid>((guid) => {
        ////            _eveOnlineGeneric.Opportunity_Task_UpdateInfo(guid);
        ////        });

        ////        if (useParallel)
        ////        {
        ////            Parallel.ForEach(allTaskIDs, value => {

        ////                _eveOnlineGeneric.Generic_СтандартныйМетодОбновления(value.guid, reqName, folder, OpportunitiesTaskInfoResult.GetArgs(value.id), OpportunitiesTaskInfoResult.TimeExpire(), action, _dbContext);
        ////            });
        ////        }
        ////        else
        ////        {
        ////            allTaskIDs.ForEach(value => {

        ////                _eveOnlineGeneric.Generic_СтандартныйМетодОбновления(value.guid, reqName, folder, OpportunitiesTaskInfoResult.GetArgs(value.id), OpportunitiesTaskInfoResult.TimeExpire(), action, _dbContext);
        ////            });
        ////        }
        ////    }
        ////}

        //public override void TaskJob()
        //{
        //    Opportunities_TaskIds();
        //    //Opportunities_TaskUpdates();
        //}
    }
}
