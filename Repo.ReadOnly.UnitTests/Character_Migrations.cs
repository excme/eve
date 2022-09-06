using eveDirect.Shared.GeneralTest;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Repo.PublicReadOnly.UnitTests
{
    public class Character_Migrations : UnitTestCore
    {
        public Character_Migrations(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task Characters_MigrationsRoot_Items()
        {
            //update_DbConnOprions("PublicDb");
            //var result = await _repoReadOnly.Characters_MigrationsRoot_Items(50,0);
        }
    }
}
