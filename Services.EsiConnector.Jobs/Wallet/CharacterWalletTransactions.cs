using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterWalletTransactions : ConnectorJob
    {
        //static string l_reqName = "Character_WalletTransactions";
        //static string l_scope = Scope.Wallet.ReadCharacterWallet.Name;
        //static ERequestFolder l_folder = ERequestFolder.Wallet;
        //public CharacterWalletTransactions() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterWalletTransactions(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _itemToUpdate = characterToUpdate;
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<WalletTransactionsCharacterResult, WalletTransactionsCharacterResult.WalletTransactionsCharacterItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Wallet.GetWalletTransactions, sso.character_id, folder, jobName, 2500);

        //    // Только добавление
        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_values = _dbContext.Eveonline_CharacterWalletsTransactions.Where(x => x.character_id == sso.character_id).ToList();
        //            foreach (var wTransaction in ConnectorResult.items ?? new List<WalletTransactionsCharacterResult.WalletTransactionsCharacterItem>())
        //            {
        //                GenericOperations.AddNotExistItem(wTransaction, db_values, new Func<EveOnlineCharacterWalletsTransactionItem, bool>(x => x.transaction_id == wTransaction.transaction_id), new EveOnlineCharacterWalletsTransactionItem() { character_id = sso.character_id }, _dbContext);
        //            }

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterWalletTransactions, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
