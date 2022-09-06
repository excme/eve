using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterContractsBids : ConnectorJob
    {
        //static string l_reqName = "Character_ContractsBids";
        //static string l_scope = Scope.Contracts.ReadCharacterContracts.Name;
        //static ERequestFolder l_folder = ERequestFolder.Contracts;
        //public CharacterContractsBids() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterContractsBids(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0, success_contracts = 0;
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        // Запрос актуальных контрактов
        //        var contracts = _dbContext.Eveonline_ContractRefs.Where(x => x.owner_id == sso.character_id)
        //            .Join(_dbContext.Eveonline_Contracts.AsNoTracking(),
        //                rf => rf.contract_id,
        //                co => co.contract_id,
        //                (rf, co) => new { rf, co }
        //            )
        //            .Select(x => x.co)
        //            .ToList();

        //        foreach (var contract in contracts)
        //        {
        //            // Проверка на актуальность обновления
        //            if (contract == null || DateTime.UtcNow - contract.lastUpdateBids <= TimeSpan.FromHours(3))
        //                continue;

        //            // Выполнение зарпоса
        //            var ConnectorResult = SsoPaged<ContractsBidsResult, ContractsBidsResult.ContractsBidsItem, int>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Contracts.GetBids, sso.character_id, contract.contract_id, folder, jobName, 2000);

        //            if (ConnectorResult.success)
        //            {
        //                success_contracts++;

        //                // Удаление неактуальных
        //                var dbPredicate = new Func<EveOnlineContractBid, bool>(x => x.contract_id == contract.contract_id);
        //                var toRemovePredicate = new Func<EveOnlineContractBid, bool>(x => !ConnectorResult.items.Any(xx => xx.bid_id == x.bid_id));
        //                var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //                dbChanges += db_values.changes;

        //                // Добавляем и обновляем
        //                foreach (var bid in ConnectorResult.items)
        //                {
        //                    // Добавление и изменение из обновления
        //                    var predicate = new Func<EveOnlineContractBid, bool>(x => x.bid_id == bid.bid_id);
        //                    var newValue = new EveOnlineContractBid() { contract_id = contract.contract_id };
        //                    GenericOperations.UpdateItem(bid, db_values.items, predicate, newValue, _dbContext);
        //                }

        //                // Обновляем время в контракте
        //                contract.lastUpdateBids = DateTime.UtcNow;
        //                _dbContext.Eveonline_Contracts.Update(contract);
        //            }
        //        }

        //        dbChanges += _dbContext.SaveChanges();
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterContractsBids, success_contracts, dbChanges);
        //}
    }
}