using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationDivisions : ConnectorJob
    {
        //static string l_reqName = "Corporation_Divisions";
        //static string l_scope = Scope.Corporations.ReadDivisions.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationDivisions() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationDivisions(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { _maxCharactersToUpdate = maxCharactersToUpdate; }
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0, succesResponses = 0;
        //    // Запрос доп инфомарции
        //    var ConnectorResult = SsoOneItem<CorporationDivisionsResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetDivisions, sso.corporation_id, folder, jobName);
        //    if (ConnectorResult.success)
        //    {
        //        succesResponses = 1;

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_value = _dbContext.Eveonline_CorporationDivisions.AsNoTracking().FirstOrDefault(x => x.corporation_id == sso.corporation_id);
        //            if(db_value == null)
        //            {
        //                db_value = new EveOnlineCorporationDivision() { corporation_id = sso.corporation_id };
        //                _dbContext.Eveonline_CorporationDivisions.Add(db_value);
        //                dbChanges += _dbContext.SaveChanges();
        //            }

        //            var updated = IUpdateCompareProperties.UpdateProperties(ref db_value, ConnectorResult.item);
        //            if (updated)
        //                _dbContext.Eveonline_CorporationDivisions.Update(db_value);

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationDivisions, succesResponses, dbChanges);
        //}
    }
}
