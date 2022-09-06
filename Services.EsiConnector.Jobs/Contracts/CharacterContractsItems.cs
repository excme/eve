using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterContractsItems : ConnectorJob
    {
        //static string l_reqName = "Character_ContractsItems";
        //static string l_scope = Scope.Contracts.ReadCharacterContracts.Name;
        //static ERequestFolder l_folder = ERequestFolder.Contracts;
        //public CharacterContractsItems() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterContractsItems(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
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
        //            if (contract == null || DateTime.UtcNow - contract.lastUpdateItems <= TimeSpan.FromHours(3))
        //                continue;

        //            // Выполнение зарпоса
        //            var ConnectorResult = SsoPaged<ContractsItemsResult, ContractsItemsResult.ContractsItem, int>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Contracts.GetItems, sso.character_id, contract.contract_id, folder, jobName, 2000);

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

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterContractsItems, success_contracts, dbChanges);

        //    //// Выполнение запрос контрактов
        //    //var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //    //// Запрос актуальных контрактов по корпорации
        //    //var contract_ids = _dbContext.EveOnlineContractRefs.Where(x => x.owner_id == sso.character_id).Select(t => t.contract_id).ToList();

        //    //int success_contracts = 0;

        //    //foreach (var contract_id in contract_ids)
        //    //{
        //    //    // Проверка на актуальность обновления
        //    //    var contract = _dbContext.EveOnlineContracts.FirstOrDefault(x => x.contract_id == contract_id);
        //    //    if (contract == null || DateTime.UtcNow - contract.lastUpdateItems <= TimeSpan.FromHours(3))
        //    //        continue;

        //    //    // Выполнение зарпоса
        //    //    Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Character.Contracts.GetItems(sso.character_id, contract_id).ExecuteAsync);
        //    //    var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<ContractsItemsResult>(запросКоннектора1, folder, ContractsItemsResult.TimeExpire(), ContractsItemsResult.GetArgsChar(sso.character_id, contract_id)).GetAwaiter().GetResult();

        //    //    if (ConnectorResult.success)
        //    //    {
        //    //        success_contracts++;

        //    //        // Удаляем предыдущие items
        //    //        _dbContext.EveOnlineContractItems.RemoveRange(_dbContext.EveOnlineContractItems.Where(x => x.contract_id == contract_id && !ConnectorResult.value.Select(y => y.record_id).Any(yy => yy == x.record_id)));
        //    //        _dbContext.SaveChanges();

        //    //        // Добавляем и обновляем
        //    //        foreach (var record in ConnectorResult.value)
        //    //        {
        //    //            var db_value = _dbContext.EveOnlineContractItems.FirstOrDefault(x => x.record_id == record.record_id && x.contract_id == contract_id);
        //    //            if (db_value == null)
        //    //            {
        //    //                db_value = new EveOnlineContractItem() { contract_id = contract_id };
        //    //                db_value.UpdateProperties(record);
        //    //                _dbContext.EveOnlineContractItems.Add(db_value);
        //    //            }
        //    //            else
        //    //            {
        //    //                db_value.UpdateProperties(record);
        //    //                _dbContext.EveOnlineContractItems.Update(db_value);
        //    //            }
        //    //        }

        //    //        // Обновляем время в контракте
        //    //        contract.lastUpdateItems = DateTime.UtcNow;
        //    //        _dbContext.EveOnlineContracts.Update(contract);

        //    //        _dbContext.SaveChanges();
        //    //    }
        //    //}
        //}
    }
}
