using eveDirect.Databases.Contexts;
using eveDirect.Shared.GeneralTest;
using Xunit;
using Xunit.Abstractions;

namespace Services.Jobs.Processings.Tests
{
    public class Jobs : UnitTestCore
    {
        public Jobs(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void SearchItemsJob()
        {
            _publicContextOptions = LoadContextOptions<PublicContext>("PublicDb");

            var job = new SearchItemsJob(null, _publicContextOptions);
            job.Execute();
        }
    }
}
