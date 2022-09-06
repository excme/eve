using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationShareholders : ConnectorJob
    {
        //static string l_reqName = "Corporation_Shareholders";
        //static string l_scope = Scope.Wallet.ReadCorporationWallets.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationShareholders() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationShareholders(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { _maxCharactersToUpdate = maxCharactersToUpdate; }
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationShareholdersResult, CorporationShareholdersResult.CorporationShareholdersItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetShareholders, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationShareholder, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationShareholder, bool>(x => !ConnectorResult.items.Any(xx => xx.shareholder_id == x.shareholder_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var shareholder in ConnectorResult.items ?? new CorporationShareholdersResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationShareholder, bool>(x => x.shareholder_id == shareholder.shareholder_id);
        //                var newValue = new EveOnlineCorporationShareholder() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(shareholder, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationShareHolders, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
