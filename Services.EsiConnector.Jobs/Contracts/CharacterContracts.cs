using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterContracts : ConnectorJob
    {
        //static string l_reqName = "Character_Contracts";
        //static string l_scope = Scope.Contracts.ReadCharacterContracts.Name;
        //static ERequestFolder l_folder = ERequestFolder.Contracts;
        //public CharacterContracts() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterContracts(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    var ConnectorResult = SsoPaged<ContractsResult, ContractsResult.Contract>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Contracts.GetContracts, sso.character_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            List<EveOnlineContract> db_values = _dbContext.Eveonline_ContractRefs.Where(x => x.owner_id == sso.character_id)
        //                .Join(_dbContext.Eveonline_Contracts,
        //                    rf => rf.contract_id,
        //                    cont => cont.contract_id,
        //                    (rf, cont) => new { rf, cont }
        //                )
        //                .Select(x => x.cont)
        //                .ToList();

        //            //foreach (var _contract in ConnectorResult.items)
        //            Parallel.ForEach(ConnectorResult.items, new ParallelOptions() { MaxDegreeOfParallelism = 70 }, _contract =>
        //            {
        //                var predicate = new Func<EveOnlineContract, bool>(x => x.contract_id == _contract.contract_id);
        //                var newValue = new EveOnlineContract() { contract_id = _contract.contract_id };
        //                Action<EveContextDbContext> moreAdd = (dbContext) =>
        //                {
        //                    //Добавление связи и сохранение
        //                    dbContext.Eveonline_ContractRefs.Add(new EveOnlineContractRef() { contract_id = _contract.contract_id, owner_id = sso.character_id });
        //                };
        //                GenericOperations.UpdateItem(_contract, db_values, predicate, newValue, _dbContext, moreAdd, true);
        //            });

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterContracts, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
