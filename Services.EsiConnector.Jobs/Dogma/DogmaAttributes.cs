using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class DogmaAttributes : ConnectorJob
    {
        //static string l_reqName = "Dogma_UpdateAttributes";
        //static ERequestFolder l_folder = ERequestFolder.Dogma;
        //public DogmaAttributes():base(l_reqName, l_folder, _withSso: false) { }
        //public DogmaAttributes(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxItems = 30) : base(genericService, options, logger, l_reqName, l_folder, _withSso:false)
        //{
        //    _maxCharactersToUpdate = maxItems;
        //}
        //public override void TaskJob()
        //{
        //    Dogma_UpdateAttributeIds();
        //    //Dogma_UpdateAttributeInfo();
        //}

        ///// <summary>
        ///// Получение всех аттрибутов догмы
        ///// </summary>
        //void Dogma_UpdateAttributeIds()
        //{
        //    // Выполнение запроса
        //    var request = _eveOnlineGeneric.ExecuteRequest<DogmaAttributesResult>(connector.Dogma.GetAttributes().ExecuteAsync, folder, DogmaAttributesResult.TimeExpire(), DogmaAttributesResult.GetArgs()).GetAwaiter().GetResult();
        //    if (request.success)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(-255, ESsoRequestType.dogmaAttributes, request.value.Count);

        //        var ids = request.value.ToList();
        //        if (_maxCharactersToUpdate > 0)
        //            ids = ids.Take(_maxCharactersToUpdate).ToList();

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // На удаление
        //            var to_del = _dbContext.Eveonline_DogmaAttributes.Where(x => !ids.Contains(x.attribute_id));
        //            _dbContext.Eveonline_DogmaAttributes.RemoveRange(to_del);

        //            // На обновление
        //            var to_update = _dbContext.Eveonline_DogmaAttributes.Where(x => ids.Contains(x.attribute_id));
        //            foreach (var update_item in to_update)
        //            {
        //                var update = Dogma_Attribute_UpdateInfo(update_item.attribute_id);
        //                update_item.UpdateProperties(update);
        //            }

        //            // На добавление
        //            ids.RemoveAll(x => to_update.Any(xx => xx.attribute_id == x));
        //            if (ids.Count > 0)
        //            {
        //                _dbContext.Eveonline_DogmaAttributes.AddRange(ids.Select(x => {
        //                    var dogmaAttr = new EveOnlineDogmaAttribute() { attribute_id = x };
        //                    var update = Dogma_Attribute_UpdateInfo(x);
        //                    dogmaAttr.UpdateProperties(update);
        //                    return dogmaAttr;
        //                }));
        //            }

        //            _dbContext.SaveChanges();
        //        }
        //    }
        //}

        //DogmaAttributeInfoResult Dogma_Attribute_UpdateInfo(int attribute_id)
        //{
        //    string reqName = "Dogma_Attribute_UpdateInfo";

        //    // Запрос к коннектору
        //    var dogmaAttrReq = _eveOnlineGeneric.ExecuteRequest<DogmaAttributeInfoResult>(connector.Dogma.GetAttributeInformation(attribute_id).ExecuteAsync, folder, DogmaAttributeInfoResult.TimeExpire(), DogmaAttributeInfoResult.GetArgs(attribute_id)).GetAwaiter().GetResult();

        //    return dogmaAttrReq.value;
        //}

        ///// <summary>
        ///// Обновление информации о аттребуте догмы
        ///// </summary>
        ////void Dogma_UpdateAttributeInfo(bool useParallel = false)
        ////{
        ////    string reqName = "Dogma_UpdateAttributeInfo";
        ////    ERequestFolder folder = ERequestFolder.Dogma;

        ////    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        ////    {
        ////        var allDogmaAttrs = _dbContext.EveOnlineDogmaAttributes.Select(x => new { guid = x.Id, id = x.attribute_id }).ToList();

        ////        Action<Guid> action = new Action<Guid>((guid) =>
        ////        {
        ////            _eveOnlineGeneric.Dogma_Attribute_UpdateInfo(guid);
        ////        });

        ////        if (useParallel)
        ////        {
        ////            Parallel.ForEach(allDogmaAttrs, value =>
        ////            {
        ////                //if (_eveOnlineGeneric.ЭкспирировалосьЛи(folder, reqName, DogmaAttributeInfoResult.GetArgs(value.id)))
        ////                //{
        ////                    _eveOnlineGeneric.Generic_СтандартныйМетодОбновления(value.guid, reqName, folder, DogmaAttributeInfoResult.GetArgs(value.id), DogmaAttributeInfoResult.TimeExpire(), action, _dbContext);
        ////                //}
        ////            });
        ////        }
        ////        else
        ////        {
        ////            allDogmaAttrs.ForEach(value =>
        ////            {
        ////                //if (_eveOnlineGeneric.ЭкспирировалосьЛи(folder, reqName, DogmaAttributeInfoResult.GetArgs(value.id)))
        ////                //{
        ////                    _eveOnlineGeneric.Generic_СтандартныйМетодОбновления(value.guid, reqName, folder, DogmaAttributeInfoResult.GetArgs(value.id), DogmaAttributeInfoResult.TimeExpire(), action, _dbContext);
        ////                //}
        ////            });
        ////        }
        ////    }
        ////}
    }
}
