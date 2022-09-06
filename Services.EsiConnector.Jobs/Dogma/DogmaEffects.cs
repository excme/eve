using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class DogmaEffects : ConnectorJob
    {
        //static string l_reqName = "Dogma_UpdateEffects";
        //static ERequestFolder l_folder = ERequestFolder.Dogma;
        //public DogmaEffects():base(l_reqName, l_folder, _withSso: false) { }
        //public DogmaEffects(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxItems=30) : base(genericService, options, logger, l_reqName, l_folder, _withSso:false)
        //{
        //    _maxCharactersToUpdate = maxItems;
        //}
        //public override void TaskJob()
        //{
        //    Dogma_UpdateEffectIds();
        //    //Dogma_UpdateEffectInfo();
        //}

        ///// <summary>
        ///// Получение всех аттрибутов догмы
        ///// </summary>
        //void Dogma_UpdateEffectIds()
        //{
        //    // Выполнение запроса
        //    var request = _eveOnlineGeneric.ExecuteRequest<DogmaEffectsResult>(connector.Dogma.GetEffects().ExecuteAsync, folder, DogmaEffectsResult.TimeExpire(), DogmaEffectsResult.GetArgs()).GetAwaiter().GetResult();
        //    if (request.success)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(-255, ESsoRequestType.dogmaEffects, request.value.Count);

        //        var ids = request.value.ToList();
        //        if (_maxCharactersToUpdate > 0)
        //            ids = ids.Take(_maxCharactersToUpdate).ToList();

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // На удаление
        //            var to_del = _dbContext.Eveonline_DogmaEffects.Where(x => !ids.Contains(x.effect_id));
        //            _dbContext.Eveonline_DogmaEffects.RemoveRange(to_del);
        //            // На обновление
        //            var to_update = _dbContext.Eveonline_DogmaEffects.Where(x => ids.Contains(x.effect_id));
        //            foreach (var update_item in to_update)
        //            {
        //                var update = Dogma_Effect_UpdateInfo(update_item.effect_id);
        //                update_item.UpdateProperties(update);
        //            }
        //            // На добавление
        //            ids.RemoveAll(x => to_update.Any(xx => xx.effect_id == x));
        //            if (ids.Count > 0)
        //            {
        //                _dbContext.Eveonline_DogmaEffects.AddRange(ids.Select(x => {
        //                    var dogmaAttr = new EveOnlineDogmaEffect() { effect_id = x };
        //                    var update = Dogma_Effect_UpdateInfo(x);
        //                    dogmaAttr.UpdateProperties(update);
        //                    return dogmaAttr;
        //                }));
        //            }

        //            _dbContext.SaveChanges();
        //        }
        //    }
        //}

        //DogmaEffectInfoResult Dogma_Effect_UpdateInfo(int effect_id)
        //{
        //    string reqName = "Dogma_Effect_UpdateInfo";
        //    ERequestFolder requestFolder = ERequestFolder.Dogma;

        //    // Запрос к коннектору
        //    var dogmaAttrReq = _eveOnlineGeneric.ExecuteRequest<DogmaEffectInfoResult>(connector.Dogma.GetEffectInformation(effect_id).ExecuteAsync, requestFolder, DogmaEffectInfoResult.TimeExpire(), DogmaEffectInfoResult.GetArgs(effect_id)).GetAwaiter().GetResult();

        //    return dogmaAttrReq.value;
        //}
    }
}
