using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationContracts : ConnectorJob
    {
        //static string l_reqName = "Corporation_Contracts";
        //static string l_scope = Scope.Contracts.ReadCorporationContracts.Name;
        //static ERequestFolder l_folder = ERequestFolder.Contracts;
        //public CorporationContracts() : base(l_reqName, l_folder, l_scope) { }
        //public CorporationContracts(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30, int corpToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = corpToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    // Проверка на npc корпорацию
        //    if (_eveOnlineGeneric.IsNpcCorporation(sso.corporation_id))
        //        return;

        //    int dbChanges = 0;
        //    // Выполнение запрос контрактов
        //    var ConnectorResult = SsoPaged<ContractsResult, ContractsResult.Contract>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Contracts.GetContracts, sso.corporation_id, folder, jobName, 1000, getArgsParam:"co");

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_values = _dbContext.Eveonline_ContractRefs.Where(x => x.owner_id == sso.corporation_id)
        //                .Join(_dbContext.Eveonline_Contracts,
        //                    rf => rf.contract_id,
        //                    cont => cont.contract_id,
        //                    (rf, cont) => new { rf, cont }
        //                )
        //                .Select(x => x.cont)
        //                .ToList();

        //            foreach (var _contract in ConnectorResult.items)
        //            {
        //                var predicate = new Func<EveOnlineContract, bool>(x => x.contract_id == _contract.contract_id);
        //                var newValue = new EveOnlineContract() { contract_id = _contract.contract_id };
        //                Action<EveContextDbContext> moreAdd = (dbContext) =>
        //                {
        //                    //Добавление связи и сохранение
        //                    dbContext.Eveonline_ContractRefs.Add(new EveOnlineContractRef() { contract_id = _contract.contract_id, owner_id = sso.corporation_id });
        //                };
        //                GenericOperations.UpdateItem(_contract, db_values, predicate, newValue, _dbContext, moreAdd);
        //            }

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationContracts, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
