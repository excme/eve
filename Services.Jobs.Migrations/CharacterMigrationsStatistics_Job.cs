using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Services.Jobs.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace eveDirect.Services.Jobs.Migrations
{
    /// <summary>
    /// Расчет статистики мграций персонажей по корпорациям и их альянсам
    /// </summary>
    //[System.Obsolete("Все миграции персонажей запрашиваются из БД. Без предрасчетов")]
    //public class CharacterMigrationsStatistics_Job : JobBase
    //{
    //    DbContextOptions<PublicContext> _options { get; }
    //    IReadWrite _repoPublic { get; }
    //    int MaxTake { get; }
    //    public CharacterMigrationsStatistics_Job(
    //        DbContextOptions<PublicContext> options,
    //        IReadWrite repoPublic,
    //        ILogger<CharacterMigrationsStatistics_Job> logger,
    //        int maxTake = 20000) : base(logger)
    //    {
    //        _options = options;
    //        _repoPublic = repoPublic;
    //        MaxTake = maxTake;
    //    }

    //    public override void Execute()
    //    {
           
    //        using var dbContext = new PublicContext(_options);

    //        // Получаем записи миграций, которые не учтены в статистике
    //        var charsMigrations = dbContext.EveOnline_CharactersCorporationHistory
    //            .Where(x => !x.instat)
    //            .Take(MaxTake)
    //            .ToList();

    //        _jobResult.subValues.Add(new JobResult.Item() { Name = "All", Value = charsMigrations.Count });

    //        // npc корпорации
    //        var ccp_corps = dbContext.EveOnline_Corporations
    //            .Where(x => x.ncp)
    //            .Select(x => x.corporation_id)
    //            .ToList();

    //        var list = AttachProgressBarToList(charsMigrations);
    //        foreach (var migration in list)
    //        {
    //            // Корпорация, в которую зашли
    //            if(!ccp_corps.Contains(migration.corporation_id) && migration.corporation_id > 0)
    //                dbContext.Statistic_CorporationCharacterMigrations
    //                .Upsert(new StatisticCorporationCharacterMigration() { 
    //                    corporation_id = migration.corporation_id,
    //                    date = migration.start_date.Date,
    //                    count = _repoPublic.Corporation_MembersCountOnDate(migration.corporation_id, migration.start_date.Date),
    //                    _in = 1,
    //                })
    //                .On(r => r.corporation_id == migration.corporation_id && r.date.Date == migration.start_date.Date)
    //                .WhenMatched(r => new StatisticCorporationCharacterMigration
    //                {
    //                    count = _repoPublic.Corporation_MembersCountOnDate(migration.corporation_id, migration.start_date.Date),
    //                    _in = r._in + 1,
    //                })
    //                .Run();

    //            // Корпорация, из которой вышли
    //            if (!ccp_corps.Contains(migration.prev_corp_id) && migration.prev_corp_id > 0)
    //                dbContext.Statistic_CorporationCharacterMigrations
    //                    .Upsert(new StatisticCorporationCharacterMigration()
    //                    {
    //                        corporation_id = migration.prev_corp_id,
    //                        date = migration.start_date.Date,
    //                        count = _repoPublic.Corporation_MembersCountOnDate(migration.prev_corp_id, migration.start_date.Date),
    //                        _out = 1
    //                    })
    //                    .On(r => r.corporation_id == migration.prev_corp_id && r.date.Date == migration.start_date.Date)
    //                    .WhenMatched(r => new StatisticCorporationCharacterMigration
    //                    {
    //                        count = _repoPublic.Corporation_MembersCountOnDate(migration.prev_corp_id, migration.start_date.Date),
    //                        _out = r._out + 1
    //                    })
    //                    .Run();

    //            //// Альянс, в который зашли
    //            //if(migration.alliance_id > 0)
    //            //    dbContext.Statistic_AllianceCharacterMigrations
    //            //        .Upsert(new StatisticAllianceCharacterMigration() { 
    //            //            alliance_id = migration.alliance_id,
    //            //            date = migration.start_date.Date,
    //            //            count = _repoPublic.Alliance_CountCharactersOnDate(migration.alliance_id, migration.start_date),
    //            //            _in = 1,
    //            //        })
    //            //        .On(r => r.alliance_id == migration.alliance_id && r.date.Date == migration.start_date.Date)
    //            //        .WhenMatched(r => new StatisticAllianceCharacterMigration
    //            //        {
    //            //            count = _repoPublic.Alliance_CountCharactersOnDate(migration.alliance_id, migration.start_date.Date),
    //            //            _in = r._in + 1,
    //            //        })
    //            //        .Run();

    //            //// Альянс из которого вышли
    //            //if(migration.prev_ally_id > 0)
    //            //    dbContext.Statistic_AllianceCharacterMigrations
    //            //        .Upsert(new StatisticAllianceCharacterMigration()
    //            //        {
    //            //            alliance_id = migration.prev_ally_id,
    //            //            date = migration.start_date.Date,
    //            //            count = _repoPublic.Alliance_CountCharactersOnDate(migration.prev_ally_id, migration.start_date),
    //            //            _out = 1,
    //            //        })
    //            //        .On(r => r.alliance_id == migration.prev_ally_id && r.date.Date == migration.start_date.Date)
    //            //        .WhenMatched(r => new StatisticAllianceCharacterMigration
    //            //        {
    //            //            count = _repoPublic.Alliance_CountCharactersOnDate(migration.prev_ally_id, migration.start_date.Date),
    //            //            _out = r._out + 1,
    //            //        })
    //            //        .Run();

    //            // Если все успешно, то сохраняем
    //            migration.instat = true;
    //            dbContext.Entry(migration).Property(p => p.instat).IsModified = true;
    //            dbContext.SaveChanges();
    //        }
    //    }
    //}
}
