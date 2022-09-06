using Xunit;
using Xunit.Abstractions;

namespace eveDirect.EsiConnectort.CheckerMeta.Tests
{
    public class Checher
    {
        CheckerMeta.Checker checker { get; set; }
        ITestOutputHelper output;
        public Checher(ITestOutputHelper output)
        {
            this.output = output;
            checker = new Checker();
        }
        [Fact]
        public void CompareRequests_Ssoes()
        {
            var swaggerRequests = checker.CompareRequests_Ssoes();
            Assert.False(swaggerRequests.hasDifference, $"{string.Join(", ", swaggerRequests.differenceRequests)}");
        }
        [Fact]
        public void CompareRequests_Versions()
        {
            var swaggerRequests = checker.CompareRequests_Versions();
            Assert.False(swaggerRequests.hasDifference, $"{string.Join(", ", swaggerRequests.differenceRequests)}");
        }
        [Fact]
        public void CompareRequests_Roles()
        {
            var swaggerRequests = checker.CompareRequests_ForRoles();
            Assert.False(swaggerRequests.hasDifference, $"{string.Join(", ", swaggerRequests.differenceRequests)}");
        }
        [Fact]
        public void CompareRequests_Results()
        {
            var differenceResults = checker.CompareRequests_ForResults();
            Assert.True(differenceResults.AreEqual, differenceResults.DifferencesString);
        }
    }
}
