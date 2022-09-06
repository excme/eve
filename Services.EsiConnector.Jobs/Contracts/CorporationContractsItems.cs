using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationContractsItems : ConnectorJob
    {
        //static string l_reqName = "Corporation_ContractsItems";
        //static string l_scope = Scope.Contracts.ReadCorporationContracts.Name;
        //static ERequestFolder l_folder = ERequestFolder.Contracts;
        //public CorporationContractsItems() : base(l_reqName, l_folder, l_scope) { }
        //public CorporationContractsItems(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30, int corpToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = corpToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    // Проверка на npc корпорацию
        //    if (_eveOnlineGeneric.IsNpcCorporation(sso.corporation_id))
        //        return;

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
        //            if (contract == null || DateTime.UtcNow - contract.lastUpdateItems <= TimeSpan.FromHours(3))
        //                continue;

        //            // Выполнение зарпоса
        //            var ConnectorResult = SsoPaged<ContractsItemsResult, ContractsItemsResult.ContractsItem, int>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Contracts.GetItems, sso.corporation_id, contract.contract_id, folder, jobName, 2000);

        //            if (ConnectorResult.success)
        //            {
        //                success_contracts++;

        //                // Удаление неактуальных
        //                var dbPredicate = new Func<EveOnlineContractItem, bool>(x => x.contract_id == contract.contract_id);
        //                var toRemovePredicate = new Func<EveOnlineContractItem, bool>(x => !ConnectorResult.items.Any(xx => xx.record_id == x.record_id));
        //                var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //                dbChanges += db_values.changes;

        //                // Добавляем и обновляем
        //                foreach (var item in ConnectorResult.items)
        //                {
        //                    // Добавление и изменение из обновления
        //                    var predicate = new Func<EveOnlineContractItem, bool>(x => x.record_id == item.record_id);
        //                    var newValue = new EveOnlineContractItem() { contract_id = contract.contract_id };
        //                    GenericOperations.UpdateItem(item, db_values.items, predicate, newValue, _dbContext);
        //                }

        //                // Обновляем время в контракте
        //                contract.lastUpdateItems = DateTime.UtcNow;
        //                _dbContext.Eveonline_Contracts.Update(contract);
        //            }
        //        }

        //        dbChanges += _dbContext.SaveChanges();
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationContractsItems, success_contracts, dbChanges);
        //}
    }
}
