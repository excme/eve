using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationWalletJournals : ConnectorJob
    {
        //static string l_reqName = "Corporation_WalletJournals";
        //static string l_scope = Scope.Wallet.ReadCorporationWallets.Name;
        //static ERequestFolder l_folder = ERequestFolder.Wallet;
        //static string[] l_needed_roles = new string[] { "Accountant", "Junior_Accountant" };
        //public CorporationWalletJournals() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationWalletJournals(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int corpToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = corpToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0, ssoResponses = 0;
        //    var division_ids = Enumerable.Range(1, 7);
        //    foreach (var div_id in division_ids)
        //    {
        //        // Выкачивание
        //        var ConnectorResult = SsoPaged<WalletJournalResult, WalletJournalResult.WalletJournalItem, int>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Wallet.GetJournal, sso.corporation_id, div_id, folder, jobName, 2500);

        //        // Только добавление
        //        if (ConnectorResult.success)
        //        {
        //            using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //            {
        //                var db_values = _dbContext.Eveonline_CorporationWalletsJournals.Where(x => x.corporation_id == sso.corporation_id && x.division_id == div_id).ToList();
        //                foreach (var wJournal in ConnectorResult.items ?? new List<WalletJournalResult.WalletJournalItem>())
        //                {
        //                    GenericOperations.AddNotExistItem(wJournal, db_values, new Func<EveOnlineCorporationWalletsJournalItem, bool>(x => x.record_id == wJournal.record_id), new EveOnlineCorporationWalletsJournalItem() { corporation_id = sso.corporation_id }, _dbContext);
        //                }

        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //            ssoResponses += ConnectorResult.items.Count;
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationWalletJournals, ssoResponses, dbChanges);
        //}
    }
}
