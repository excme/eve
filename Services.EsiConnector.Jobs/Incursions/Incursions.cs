using eveDirect.Services.EsiConnector;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class Incursions : ConnectorJob
    {
        //static string l_reqName = "Incursions";
        //static ERequestFolder l_folder = ERequestFolder.Incursions;
        //public Incursions():base(l_reqName, l_folder, _withSso: false) { }
        //public Incursions(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, _withSso: false)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob()
        //{
        //    // Выполнение запроса
        //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(connector.Incursions.GetAll().ExecuteAsync);
        //    var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<IncursionResult>(запросКоннектора, folder, IncursionResult.TimeExpire(), IncursionResult.GetArgs()).GetAwaiter().GetResult();

        //    if (ConnectorResult.success)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(-255, ESsoRequestType.incursions, ConnectorResult.value.Count);

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Очистка старых записей
        //            _dbContext.Eveonline_Incursions.RemoveRange(_dbContext.Eveonline_Incursions);

        //            List<EveOnlineIncursion> list = new List<EveOnlineIncursion>();
        //            foreach (var _incursion in ConnectorResult.value)
        //            {
        //                EveOnlineIncursion incursion = new EveOnlineIncursion();
        //                incursion.UpdateProperties(_incursion);
        //                list.Add(incursion);
        //            }

        //            _dbContext.Eveonline_Incursions.AddRange(list);
        //            _dbContext.SaveChanges();
        //        }
        //    }
        //}
    }
}
