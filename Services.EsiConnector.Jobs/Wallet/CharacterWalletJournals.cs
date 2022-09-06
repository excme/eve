using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterWalletJournals : ConnectorJob
    {
        //static string l_reqName = "Character_WalletJournals";
        //static string l_scope = Scope.Wallet.ReadCharacterWallet.Name;
        //static ERequestFolder l_folder = ERequestFolder.Wallet;
        //public CharacterWalletJournals() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterWalletJournals(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<WalletJournalResult, WalletJournalResult.WalletJournalItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Wallet.GetWalletJounal, sso.character_id, folder, jobName, 2500);

        //    // Только добавление
        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_values = _dbContext.Eveonline_CharacterWalletsJournals.Where(x => x.character_id == sso.character_id).ToList();
        //            foreach(var wJournal in ConnectorResult.items ?? new List<WalletJournalResult.WalletJournalItem>())
        //            {
        //                GenericOperations.AddNotExistItem(wJournal, db_values, new Func<EveOnlineCharacterWalletsJournalItem, bool>(x => x.record_id == wJournal.record_id), new EveOnlineCharacterWalletsJournalItem() { character_id = sso.character_id }, _dbContext);
        //            }

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterWalletJournals, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
