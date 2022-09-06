using eveDirect.Databases;
using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.GeneralTest;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Upsert : UnitTestCore
    {
        public Upsert(ITestOutputHelper output) : base(output) { }
        [Fact]
        public async Task Method()
        {
            using var context = new PublicContext(_publicContextOptions);
            await context.Eveonline_Wars.Upsert(new EveOnlineWar()
            {
                war_id = 10,
                aggressor_ships_killed = 100
            })
            .On(v => v.war_id)
            .WhenMatched(v => new EveOnlineWar
            {
                aggressor_ships_killed = v.aggressor_ships_killed + 1,
            })
            .RunAsync();
        }
    }
}
