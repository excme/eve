using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class GenericClearLogs : ConnectorJob
    {
        //static string l_reqName = "Generic_ClearLogs";
        //public GenericClearLogs() : base(l_reqName, null, _withSso: false) { }
        //public GenericClearLogs(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger) : base(genericService, options, logger, l_reqName, null, _withSso: false) { }
        //public override void TaskJob()
        //{
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        _dbContext.Evezone_LogEvents.RemoveRange(_dbContext.Evezone_LogEvents.Where(x => x.TimeStamp < DateTime.UtcNow.AddDays(-5)));
        //        var dbChanges = _dbContext.SaveChanges();
        //        AddSsoRequest(-255, ESsoRequestType.generic, 0, dbChanges);
        //    }
        //}
    }
}
