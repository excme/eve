using eveDirect.Shared.GeneralTest;
using eveDirect.Api.Public.Areas.Universe.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Common.UnitTests
{
    public class Universe : UnitTestCore
    {
        public Universe(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task TypeNames()
        {
            // Arrange
            var type_ids = Enumerable.Range(1000, 150).ToArray();

            // Act
            var contoller = new PublicController(_repoReadOnly, _cache);
            var actionResult = await contoller.Type_Names(type_ids, "ru");

            // Assert
        }
    }
}
