using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationMembers : ConnectorJob
    {
        //static string l_reqName = "Corporation_Members";
        //static string l_scope = Scope.Corporations.ReadCorporationMembership.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //public CorporationMembers() : base(l_reqName, l_folder, l_scope) { }
        //public CorporationMembers(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope) {
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    // Проверка на npc корпорацию
        //    if (_eveOnlineGeneric.IsNpcCorporation(sso.corporation_id))
        //        return;

        //    int dbChanges = 0;
        //    var ConnectorResult = SsoOnePageListStruct<CorporationMembersResult, int>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetMembers, sso.corporation_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        var in_corp = new List<EveOnlineCharacter>();
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            in_corp = _dbContext.Eveonline_Characters.Where(x => x.corporation_id == sso.corporation_id).ToList();

        //            // Которые вошли в корпу
        //            var to_in = ConnectorResult.items.Where(x => !in_corp.Select(a => a.character_id).Contains(x)).ToList();
        //            // Которые выпали из корпы
        //            var to_out = in_corp.Where(x => !ConnectorResult.items.Contains(x.character_id)).ToList();

        //            string sqlCmd = "UPDATE Eveonline_Characters set corporation_id = @corporation_id where character_id = @character_id";
        //            to_in.ForEach(character_id =>
        //            {
        //                var co = new SqlParameter("@corporation_id", sso.corporation_id);
        //                var ch = new SqlParameter("@character_id", character_id);
        //                dbChanges += _dbContext.Database.ExecuteSqlCommand(sqlCmd, co, ch);
        //            });
        //            to_out.ForEach(character =>
        //            {
        //                var co = new SqlParameter("@corporation_id", -1);
        //                var ch = new SqlParameter("@character_id", character.character_id);
        //                dbChanges += _dbContext.Database.ExecuteSqlCommand(sqlCmd, co, ch);
        //            });
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationMembers, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
    }
}
