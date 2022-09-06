using eveDirect.Services.Jobs.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Services.Jobs.Migrations
{
    /// <summary>
    /// Джоба расчета миграций корпораций между альянсами
    /// </summary>
    [System.Obsolete("Все миграции корпораций запрашиваются из БД. Без предрасчетов")]
    public class CorporationMigrationsStatistics_Job //: JobBase
    {
        //DbContextOptions<PublicContext> _options { get; }
        //IReadWrite _repoPublic { get; set; }
        //int MaxTake { get; }
        //public CorporationMigrationsStatistics_Job(
        //    DbContextOptions<PublicContext> options, 
        //    IReadWrite repoPublic,
        //    ILogger<CorporationMigrationsStatistics_Job> logger,
        //    int maxTake = 20000
        //    ) : base(logger)
        //{
        //    _options = options;
        //    _repoPublic = repoPublic;
        //    MaxTake = maxTake;
        //}
        //public override void Execute()
        //{
           
        //    using var dbContext = new PublicContext(_options);

        //    // Получаем записи миграций, которые не учтены в статистике
        //    var corpsMigrations = dbContext.EveOnline_CorporationAllianceHistories
        //        .Where(x => !x.instat)
        //        .Take(MaxTake)
        //        .ToList();

        //    _jobResult.subValues.Add(new JobResult.Item() { Name = "All", Value = corpsMigrations.Count });

        //    var list = AttachProgressBarToList(corpsMigrations);
        //    foreach (EveOnlineCorporationAllianceHistory migration in list)
        //    {
        //        if (migration.alliance_id > 0 || migration.prev_ally_id > 0)
        //        {
        //            // Альянс, в который зашли
        //            if (migration.alliance_id > 0)
        //                dbContext.Statistic_AllianceCorporationMigrations
        //                    .Upsert(new StatisticAllianceCorporationMigration()
        //                    {
        //                        alliance_id = migration.alliance_id,
        //                        date = migration.start_date.Date,
        //                        count = _repoPublic.Alliance_CountCorporationsOnDate(migration.alliance_id, migration.start_date.Date),
        //                        _in = 1,
        //                    })
        //                    .On(r => r.alliance_id == migration.alliance_id && r.date.Date == migration.start_date.Date)
        //                    .WhenMatched(r => new StatisticAllianceCorporationMigration
        //                    {
        //                        count = _repoPublic.Alliance_CountCorporationsOnDate(migration.alliance_id, migration.start_date.Date),
        //                        _in = r._in + 1,
        //                    })
        //                .Run();

        //            // Альянс, из которого вышлы
        //            if (migration.prev_ally_id > 0)
        //                dbContext.Statistic_AllianceCorporationMigrations
        //                    .Upsert(new StatisticAllianceCorporationMigration()
        //                    {
        //                        alliance_id = migration.prev_ally_id,
        //                        date = migration.start_date.Date,
        //                        count = _repoPublic.Alliance_CountCorporationsOnDate(migration.prev_ally_id, migration.start_date.Date),
        //                        _out = 1,
        //                    })
        //                    .On(r => r.alliance_id == migration.prev_ally_id && r.date.Date == migration.start_date.Date)
        //                    .WhenMatched(r => new StatisticAllianceCorporationMigration
        //                    {
        //                        count = _repoPublic.Alliance_CountCorporationsOnDate(migration.prev_ally_id, migration.start_date.Date),
        //                        _out = r._out + 1,
        //                    })
        //                .Run();


        //            var corpMembers = _repoPublic.Corporation_MembersCountOnDate(migration.corporation_id, migration.start_date);

        //            // Альянс, в который зашли
        //            if (migration.alliance_id > 0)
        //                dbContext.Statistic_AllianceCharacterMigrations
        //                    .Upsert(new StatisticAllianceCharacterMigration()
        //                    {
        //                        alliance_id = migration.alliance_id,
        //                        date = migration.start_date.Date,
        //                        count = _repoPublic.Alliance_CountCharactersOnDate(migration.alliance_id, migration.start_date),
        //                    })
        //                    .On(r => r.alliance_id == migration.alliance_id && r.date.Date == migration.start_date.Date)
        //                    .WhenMatched(r => new StatisticAllianceCharacterMigration
        //                    {
        //                        count = _repoPublic.Alliance_CountCharactersOnDate(migration.alliance_id, migration.start_date.Date),
        //                        _in = r._in + corpMembers,
        //                    })
        //                    .Run();

        //            // Альянс из которого вышли
        //            if (migration.prev_ally_id > 0)
        //                dbContext.Statistic_AllianceCharacterMigrations
        //                    .Upsert(new StatisticAllianceCharacterMigration()
        //                    {
        //                        alliance_id = migration.prev_ally_id,
        //                        date = migration.start_date.Date,
        //                        count = _repoPublic.Alliance_CountCharactersOnDate(migration.prev_ally_id, migration.start_date),
        //                        _out = corpMembers,
        //                    })
        //                    .On(r => r.alliance_id == migration.prev_ally_id && r.date.Date == migration.start_date.Date)
        //                    .WhenMatched(r => new StatisticAllianceCharacterMigration
        //                    {
        //                        count = _repoPublic.Alliance_CountCharactersOnDate(migration.prev_ally_id, migration.start_date.Date),
        //                        _out = r._out + corpMembers,
        //                    })
        //                    .Run();

        //            // Если все успешно, то сохраняем
        //            migration.instat = true;
        //            dbContext.Entry(migration).Property(p => p.instat).IsModified = true;
        //            dbContext.SaveChanges();

        //        }
        //    }
        //}
 
    }
}
