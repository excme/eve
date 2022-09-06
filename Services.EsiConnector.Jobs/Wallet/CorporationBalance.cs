using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationWalletBalance : ConnectorJob
    {
        //static string l_reqName = "Corporation_Wallets";
        //static string l_scope = Scope.Wallet.ReadCorporationWallets.Name;
        //static ERequestFolder l_folder = ERequestFolder.Wallet;
        //static string[] l_needed_roles = new string[] { "Accountant", "Junior_Accountant" };
        //public CorporationWalletBalance() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationWalletBalance(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    var division_ids = Enumerable.Range(1, 7);
        //    int ssoResponses = 0, dbChanges = 0;
            
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CorporationWalletsResult, CorporationWalletsResult.CorporationWalletsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Wallet.GetBalances, sso.corporation_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        ssoResponses = ConnectorResult.items.Count;
        //        var balanceStep = 100000000;
        //        // Внутренние номер кошельков
        //        var divisions = Enumerable.Range(1, 7);

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            foreach (var division_id in divisions)
        //            {
        //                var curerntDivision = ConnectorResult.items.FirstOrDefault(x => x.division == division_id);
        //                if (curerntDivision != null)
        //                {
        //                    var lastWalletBalance = _dbContext.Eveonline_WalletBalances.LastOrDefault(x => x.owner_id == sso.corporation_id && x.division_id == division_id);
        //                    if (lastWalletBalance == null || (Math.Abs(lastWalletBalance.balance - Convert.ToInt64(curerntDivision.balance)) > balanceStep))
        //                    {
        //                        var item = new EveOnlineWalletBalance() { division_id = Convert.ToByte(curerntDivision.division), owner_id = sso.corporation_id, OnDateTime = DateTime.UtcNow, balance = Convert.ToInt64(curerntDivision.balance) };
        //                        _dbContext.Eveonline_WalletBalances.Add(item);
        //                    }
        //                }
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    _eveOnlineGeneric.Sso_RequestStatistic(sso.corporation_id, ESsoRequestType.corporationWalletBalance, ssoResponses, dbChanges);
        //}
    }
}
