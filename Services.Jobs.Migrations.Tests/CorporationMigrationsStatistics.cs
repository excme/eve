namespace Jobs.Migrations.Tests
{
    //[Obsolete("Ждоба устарела")]
    //public class CorporationMigrationsStatistics : UnitTestCore
    //{
    //    DateTime testOnDate = new DateTime(2020, 9, 14);
    //    int alliance_id = 99008228;
    //    public CorporationMigrationsStatistics(ITestOutputHelper output) : base(output)
    //    {
    //        _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions);
    //    }

    //    void Prepare()
    //    {
    //        // Загрузка данных из Esi

    //        int[] corporations_ids = new[] { 98043813, 98284134, 98383139, 771155649, 795045209 };

    //        foreach(var corporation_id in corporations_ids)
    //        {
    //            _repoPublicCommon.Corporation_AddNew(corporation_id);

    //            // Загрузка Инфо
    //            var corpInfo = Esi_ExecuteAndReturn(connector.Corporation.Information(corporation_id));
    //            _repoPublicCommon.Corporation_Update_PublicInfo(corporation_id, corpInfo);

    //            // Истории Альянсов
    //            var corp_AllianceHistory = Esi_ExecuteAndReturn(connector.Corporation.AllianceHistory(corporation_id));
    //            _repoPublicCommon.Corporation_UpdateAllianceHistory(corporation_id, corp_AllianceHistory
    //                .Where(x => x.start_date < testOnDate)
    //                .ToList());
    //        }
    //    }

    //    void Execute_Job()
    //    {
    //        var job = new CorporationMigrationsStatistics_Job(_publicContextOptions, _repoPublicCommon, null);
    //        job.Execute();
    //    }

    //    //[Fact]
    //    public void Test1()
    //    {
    //        Prepare();
    //    }
    //}
}
