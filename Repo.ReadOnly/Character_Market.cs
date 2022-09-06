using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.PublicReadOnly.Models;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<LoadResult> Character_MarketContracts(int character_id, DataSourceLoadOptionsBase lo)
        {
            await using var context = new PublicContext(_options);

            var query = context.Eveonline_Contracts
                .AsQueryable()
                .Where(x => x.issuer_id == character_id)
                .Select(x => new CharacterMarketContractModel()
                {
                    i = x.contract_id,
                    p = x.price,
                    t = (byte)x.type,
                    de = x.date_expired,
                    di = x.date_issued,
                    v = x.volume
                });

            return await DataSourceLoader.LoadAsync(query, lo);
        }
    }
}
