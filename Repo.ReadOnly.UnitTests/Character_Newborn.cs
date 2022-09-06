using eveDirect.Shared.GeneralTest;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Repo.PublicReadOnly.UnitTests
{
    public class Character_Newborn : UnitTestCore
    {
        public Character_Newborn(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task CharacterNewbornItems_Calc()
        {
            await _repoReadOnly.CharacterNewbornItems_Calc();
            var result = await _repoReadOnly.CharacterNewbornItems();
        }
    }
}
