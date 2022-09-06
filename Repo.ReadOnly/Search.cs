using eveDirect.Databases.Contexts;
using eveDirect.Repo.PublicReadOnly.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<List<SearchItemModel>> Search_BySubValue(string subString)
        {
            using var context = new PublicContext(_options);
            return await context.SearchItems
                .Select(x => new SearchItemModel()
                {
                    n = x.title,
                    i = x.item_id,
                    t = (byte)x.type
                })
                .Where(x => x.n.Contains(subString))
                .Take(7)
                .ToListAsync();
        }
    }
}
