using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterAssets : ConnectorJob
    {
        //static string l_reqName = "Character_Assets";
        //static string l_scope = Scope.Assets.ReadAssets.Name;
        //static ERequestFolder l_folder = ERequestFolder.Assets;

        //public CharacterAssets() : base(l_reqName, l_folder, l_scope) {

        //}
        //public CharacterAssets(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    List<AssetsResult.AssetItem> character_assets = new List<AssetsResult.AssetItem>();
        //    int pageNum = 0; int lastCount = 0;

        //    do
        //    {
        //        lastCount = 0;
        //        pageNum++;

        //        // Выполнение запроса
        //        var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //        Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Assets.GetAll(sso.character_id, pageNum).ExecuteAsync);
        //        var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<AssetsResult>(запросКоннектора, folder, AssetsResult.TimeExpire(), AssetsResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();

        //        if (ConnectorResult.success)
        //        {
        //            character_assets.AddRange(ConnectorResult.value);
        //            lastCount = ConnectorResult.value.Count;
        //        }
        //    } while (lastCount == 1000);

        //    if (character_assets.Count > 0)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterAssets, character_assets.Count);
        //        _eveOnlineGeneric.UpdateAssets<EveOnlineAssetSpanshotItem, EveOnlineAssetChangeItem, EveOnlineAssetsReport>(sso.character_id, character_assets);
        //    }
        //}
    }
}
