using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.GeneralTest;
using eveDirect.Services.Jobs.Market;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Contracts : UnitTestCore
    {
        public Contracts(ITestOutputHelper output) : base(output)
        {
            _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions);
        }

        
    }
}
