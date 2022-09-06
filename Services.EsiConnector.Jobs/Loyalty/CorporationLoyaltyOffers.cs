using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationLoyaltyOffers : ConnectorJob
    {
        //static string l_reqName = "Corporation_LoyaltyOffers";
        //static ERequestFolder l_folder = ERequestFolder.Loyalty;
        //public CorporationLoyaltyOffers() : base(l_reqName, l_folder) { }
        //public CorporationLoyaltyOffers(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob()
        //{
        //    // TODO: Определиться с тем как качать, потому что здесь без авторизации можно запрошивать все корпорации

        //    var reqName = "Corporation_Offers";
        //    ERequestFolder requestFolder = ERequestFolder.Loyalty;
        //    _logger.LogInformation($"{reqName}. Начало");
        //    var s = connector.Loyalty.GetStoreOffers(0).RequiresScope;

        //    //foreach (var sso in all_ssos)
        //    //{
        //    //    //var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);

        //    //    // Запрос папок
        //    //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Corporation.GetStandings(sso.corporation_id).ExecuteAsync);
        //    //    var request = _eveOnlineGeneric.ExecuteRequest<StandingsResult>(запросКоннектора, requestFolder, reqName, StandingsResult.GetArgsCorp(sso.corporation_id)).GetAwaiter().GetResult();
        //    //    _logger.LogInformation($"{reqName}. corporation {sso.corporation_id} success = {request.success}. # {request.value?.Count}");

        //    //    if (request.success)
        //    //    {
        //    //        _eveOnlineGeneric.Sso_RequestStatistic(corporation_id, ESsoRequestType.corporationLoyaltyOffers, request.value.Count);

        //    //        using (EveContextDbContext _dbContext = new EveContextDbContext(options.Options))
        //    //        {
        //    //            foreach (var standing in request.value)
        //    //            {
        //    //                // Очистка неактуальных
        //    //                _dbContext.EveOnlineCorporationStandings.RemoveRange(_dbContext.EveOnlineCorporationStandings.Where(x => !request.value.Select(y => y.from_id).ToList().Contains(x.from_id) && x.corporation_id == sso.corporation_id));
        //    //                _dbContext.SaveChanges();

        //    //                // Добавление и обновление
        //    //                var db_value = _dbContext.EveOnlineCorporationStandings.FirstOrDefault(x => x.corporation_id == sso.corporation_id && x.from_id == standing.from_id);
        //    //                if (db_value == null)
        //    //                {
        //    //                    db_value = new EveOnlineCorporationStanding() { corporation_id = sso.corporation_id };
        //    //                    db_value.UpdateConnectionRequestResult(standing);
        //    //                    _dbContext.EveOnlineCorporationStandings.Add(db_value);

        //    //                }
        //    //                else
        //    //                {
        //    //                    db_value.UpdateConnectionRequestResult(standing);
        //    //                    _dbContext.EveOnlineCorporationStandings.Update(db_value);
        //    //                }
        //    //                _dbContext.SaveChanges();
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    //_logger.LogInformation($"{reqName}. Конец");
        //}
    }
}
