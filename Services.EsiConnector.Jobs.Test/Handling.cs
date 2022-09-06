using eveDirect.Shared.ConfigurationHelper;
using eveDirect.Shared.GeneralTest;
using Xunit;
using Xunit.Abstractions;
using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Jobs.Processing;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Handling : UnitTestCore
    {
        public Handling(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void LookOverIdCharacters()
        {
            var job = new UniverseSearchNewInRange(_repoPublicCommon, null);

            //var from =  _repoPublicCommon.Db_SelectColumn_MaxMinValue<EveOnlineCharacter, int>(
            //        max: x => x.character_id,
            //        where: x => x.birthday.Date == new DateTime(2020, 7, 3).Date
            //    );

            //var to =  _repoPublicCommon.Db_SelectColumn_MaxMinValue<EveOnlineCharacter, int>(
            //        max: x => x.character_id,
            //        where: x => x.birthday.Date == new DateTime(2020, 7, 7).Date
            //    );

             job.TaskSimple2();
        }

        [Fact]
        public void RecalcCharacterMigrations()
        {
            var job = new RecalcCharacterMigrations(_publicContextOptions);
             job.Work();
        }

        
    }
}
