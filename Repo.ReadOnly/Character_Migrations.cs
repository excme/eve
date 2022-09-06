using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.PublicReadOnly.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<LoadResult> Characters_MigrationsRoot_Items(DataSourceLoadOptions loadOptions)
        {
            await using var context = new PublicContext(_options);
            var query = context.EveOnline_CharactersCorporationHistory
                .AsNoTracking()
                .Where(x => !x.just_newborn)
                .Join(context.EveOnline_Characters.AsNoTracking().Select(x => new { x.character_id, x.birthday }),
                    h => h.character_id,
                    ch => ch.character_id,
                    (h, ch) => new { h, ch }
                )
                .Select(x => new CharacterMigrationRootItem()
                {
                    i = x.h.record_id,
                    o = x.h.character_id,
                    d = x.h.is_deleted,
                    s = x.h.start_date,
                    a = x.h.corporation_id,
                    b = x.h.next_corp_id,
                    c = x.h.prev_corp_id,
                    e = x.h.end_date,
                    h = x.ch.birthday
                });

            return await DataSourceLoader.LoadAsync(query, loadOptions);
        }
    }
}
