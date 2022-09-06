using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using eveDirect.Shared.GeneralTest;
using eveDirect.Api.Public.Areas.Characters.Controllers;

namespace Common.UnitTests
{
    public class Character : UnitTestCore
    {
        public Character(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task Contracts_List()
        {
            // Arrange

            // Act
            var marketContoller = new NewbornsController(_repoReadOnly);
            var actionResult = await marketContoller.NewbornsAsync();

            // Assert
        }
    }
}
