using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Jobs.Processing
{
    public class RecalcCharacterMigrations
    {
        private DbContextOptions<PublicContext> publicContextOptions;

        public RecalcCharacterMigrations(DbContextOptions<PublicContext> publicContextOptions)
        {
            this.publicContextOptions = publicContextOptions;
        }

        public void Work()
        {
            // Пересчет всех character_Migrations
            using var context = new PublicContext(publicContextOptions);

            var list = context.EveOnline_CharactersCorporationHistory
                .Where(x => x.just_newborn == true)
                .Select(x => new { x.character_id, x.record_id })
                .ToList();

            var grouped = list
                .GroupBy(x => x.character_id)
                .Select(x => new { character_id = x.Key, count = x.Count(), record_ids = x.Select(y => y.record_id).ToList() })
                .Where(x => x.count > 1)
                .ToList();

            //var migrations_toUpdate = await context.EveOnline_CharactersCorporationHistory
            //.Where(x => grouped.Select(xx => xx.character_id).ToList().Any(xx => xx == x.character_id))
            //.Select(x => new EveOnlineCharacterCorpHistory() { record_id = x.record_id, have_migrations = x.have_migrations })
            //.ToListAsync();

            var migrations_toUpdate = grouped
                .SelectMany(x => x.record_ids)
                .Select(x => new EveOnlineCharacterCorpHistory() { record_id = x, just_newborn = false })
                .ToList();

            migrations_toUpdate.ForEach(migr => { migr.just_newborn = false; });
            context.EveOnline_CharactersCorporationHistory.AttachRange(migrations_toUpdate);
            migrations_toUpdate.ForEach(migr => context.Entry(migr).Property(x => x.just_newborn).IsModified = true);
            context.SaveChanges();
        }
    }
}
