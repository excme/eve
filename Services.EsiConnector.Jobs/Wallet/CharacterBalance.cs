using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterWalletBalance : ConnectorJob
    {
        //static string l_reqName = "Character_Wallet";
        //static string l_scope = Scope.Wallet.ReadCharacterWallet.Name;
        //static ERequestFolder l_folder = ERequestFolder.Wallet;
        //public CharacterWalletBalance() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterWalletBalance(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOneItem<CharacterWalletsResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Wallet.GetWalletBalance, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        // 100M
        //        var balanceStep = 100000000;
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var character_balance = _dbContext.Eveonline_WalletBalances.LastOrDefault(x => x.owner_id == sso.character_id);
        //            var balanceDiff = character_balance != null ? Math.Abs(character_balance.balance - Convert.ToInt64(ConnectorResult.item)) : ConnectorResult.item;
        //            if (character_balance == null ||
        //                (
        //                    balanceDiff > balanceStep &&
        //                    balanceDiff / character_balance.balance >= 0.05
        //                ))
        //            {
        //                var wBalance = new EveOnlineWalletBalance() { owner_id = sso.character_id, division_id = 1, OnDateTime = DateTime.UtcNow, balance = Convert.ToInt64(ConnectorResult.item) };
        //                _dbContext.Eveonline_WalletBalances.Add(wBalance);
        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterWalletBalance, ConnectorResult.success ? 1 : 0, dbChanges);
        //}
    }
}
