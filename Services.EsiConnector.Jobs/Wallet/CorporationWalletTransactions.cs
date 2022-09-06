using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationWalletTransactions : ConnectorJob
    {
        //static string l_reqName = "Corporation_WalletTransactions";
        //static string l_scope = Scope.Wallet.ReadCorporationWallets.Name;
        //static ERequestFolder l_folder = ERequestFolder.Wallet;
        //static string[] l_needed_roles = new string[] { "Accountant", "Junior_Accountant" };
        //public CorporationWalletTransactions() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationWalletTransactions(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int corpToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = corpToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    var division_ids = Enumerable.Range(1, 7);
        //    int ssoResponses = 0, dbChanges = 0;
        //    foreach (var div_id in division_ids)
        //    {
        //        // Выкачивание
        //        var ConnectorResult = SsoPaged<WalletTransactionsCharacterResult, WalletTransactionsCharacterResult.WalletTransactionsCharacterItem, int>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Wallet.GetTransactions, sso.corporation_id, div_id, folder, jobName, 2500);

        //        if (ConnectorResult.success)
        //        {
        //            using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //            {
        //                var db_values = _dbContext.Eveonline_CorporationWalletsTransactions.Where(x => x.corporation_id == sso.corporation_id && x.division_id == div_id).ToList();
        //                foreach (var wTransaction in ConnectorResult.items ?? new List<WalletTransactionsCharacterResult.WalletTransactionsCharacterItem>())
        //                {
        //                    GenericOperations.AddNotExistItem(wTransaction, db_values, new Func<EveOnlineCorporationWalletsTransactionItem, bool>(x => x.transaction_id == wTransaction.transaction_id), new EveOnlineCorporationWalletsTransactionItem() { corporation_id = sso.corporation_id }, _dbContext);
        //                }

        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //            ssoResponses += ConnectorResult.items.Count;
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationWalletTransactions, ssoResponses, dbChanges);
        //}
    }
}
