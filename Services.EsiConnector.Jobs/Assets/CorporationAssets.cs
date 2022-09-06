using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationAssets : ConnectorJob
    {
        //static string l_reqName = "Corporation_Assets";
        //static string l_scope = Scope.Assets.ReadCorporationAssets.Name;
        //static ERequestFolder l_folder = ERequestFolder.Assets;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationAssets():base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationAssets(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int corpToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) {
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = corpToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    List<AssetsResult.AssetItem> corporation_assets = new List<AssetsResult.AssetItem>();
        //    int pageNum = 0; int lastCount = 0;

        //    do
        //    {
        //        lastCount = 0;
        //        pageNum++;
        //        // Выполнение запроса
        //        var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //        Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Corporation.Assets.GetAll(sso.corporation_id, pageNum).ExecuteAsync);
        //        var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<AssetsResult>(запросКоннектора, folder, AssetsResult.TimeExpire(), AssetsResult.GetArgsCorp(sso.corporation_id)).GetAwaiter().GetResult();

        //        if (ConnectorResult.success)
        //        {
        //            corporation_assets.AddRange(ConnectorResult.value);
        //            lastCount = ConnectorResult.value.Count;
        //        }
        //    } while (lastCount == 1000);

        //    if (corporation_assets.Count > 0)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.corporation_id, ESsoRequestType.corporationAssets, corporation_assets.Count);
        //        _eveOnlineGeneric.UpdateAssets<EveOnlineAssetSpanshotItem, EveOnlineAssetChangeItem, EveOnlineAssetsReport>(sso.corporation_id, corporation_assets);
        //    }
        //}
    }
}
