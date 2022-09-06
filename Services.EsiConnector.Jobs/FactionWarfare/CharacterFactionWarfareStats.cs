using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterFactionWarfareStats : ConnectorJob
    {
        //static string l_reqName = "Character_FactionWarfareStats";
        //static string l_scope = Scope.FactionWarface.ReadCharacterStats.Name;
        //static ERequestFolder l_folder = ERequestFolder.FactionWarfare;
        //public CharacterFactionWarfareStats() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterFactionWarfareStats(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 3000, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _itemToUpdate = characterToUpdate;
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.FactionWarfare.GetStats(sso.character_id).ExecuteAsync);
        //    var request = _eveOnlineGeneric.ExecuteRequest<CharactersFwStatsResult>(запросКоннектора, folder, CharactersFwStatsResult.TimeExpire(), CharactersFwStatsResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();
        //    _logger.LogInformation($"{jobName}. character {sso.character_id} success = {request.success}.");

        //    if (request.success)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterFractionWarStat, 1);

        //        if (request.value.current_rank != 0)
        //        {
        //            using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //            {
        //                var last_stats = _dbContext.Eveonline_CharacterFactionWarfareStats.LastOrDefault(x => x.character_id == sso.character_id);

        //                if (last_stats == null || DateTime.UtcNow.TimeOfDay > new TimeSpan(11, 6, 0))
        //                {
        //                    EveOnlineCharacterFactionWarfareStat stat = new EveOnlineCharacterFactionWarfareStat() { character_id = sso.character_id, onDateTime = DateTime.UtcNow.Date };
        //                    stat.UpdateProperties(request.value);
        //                    _dbContext.Eveonline_CharacterFactionWarfareStats.Add(stat);
        //                    _dbContext.SaveChanges();
        //                }
        //                else if (last_stats.onDateTime.Date < DateTime.UtcNow.Date.AddDays(-1) && DateTime.UtcNow.TimeOfDay < new TimeSpan(11, 4, 0))
        //                {
        //                    EveOnlineCharacterFactionWarfareStat stat = new EveOnlineCharacterFactionWarfareStat() { character_id = sso.character_id, onDateTime = DateTime.UtcNow.Date.AddDays(-1) };
        //                    stat.UpdateProperties(request.value);
        //                    _dbContext.Eveonline_CharacterFactionWarfareStats.Add(stat);
        //                    _dbContext.SaveChanges();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
