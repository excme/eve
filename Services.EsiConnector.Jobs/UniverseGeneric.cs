using eveDirect.Services.EsiConnector;
using eveDirect.Shared;
using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.EsiConnector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public static class UniverseGeneric
    {
        public static List<EntityT> MakeListUpdate<TResponseList, EntityT, BaseT>(
            List<int> repoList,
            Func<ELanguages, EsiResponse<TResponseList>> esi_request,
            List<string> multLangProperties
            //Func<BaseT, int> selector
            )
            where TResponseList : List<BaseT>, ISsoResult
            where BaseT : class
            where EntityT : class, BaseT, new()
        {
            List<EntityT> items_to_add = new List<EntityT>();
            var lang_list = Enum.GetValues(typeof(ELanguages)).Cast<ELanguages>();

            var a_reqResult = EsiConnector(esi_request, ELanguages.en_us);
            if (a_reqResult.isSuccess)
            {
                //var new_items = a_reqResult.Data.Where(x => !repoList.Contains(x)).ToList();

                foreach (var new_item in a_reqResult.Data)
                {
                    var db_value = new EntityT();bool keySetted=false;

                    // Обновление ключа
                    PropertyInfo[] properties = typeof(EntityT).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        KeyAttribute attribute =
                        Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;

                        if (attribute != null) {
                            db_value.UpdateProperties(new_item);

                            Type restype = typeof(BaseT);
                            PropertyInfo resPi = restype.GetProperty(property.Name);
                            var item_key = resPi.GetValue(new_item);


                            property.SetValue(db_value, Convert.ChangeType(item_key, property.PropertyType), null);
                            keySetted = true;
                            break;
                        }
                    }

                    if (keySetted)
                    {
                        foreach (ELanguages lang in lang_list)
                        {
                            var reqResult = EsiConnector(esi_request, lang);
                            if (reqResult.isSuccess)
                            {
                                // Перебор мультиязычных свойств
                                foreach (string propName in multLangProperties)
                                {
                                    string langPref = lang.ToString().Substring(0, 2);

                                    try
                                    {
                                        // зн-е из запроса
                                        Type restype = typeof(BaseT);
                                        PropertyInfo resPi = restype.GetProperty(propName.ToLower());
                                        var resValue = resPi.GetValue(reqResult.Data[a_reqResult.Data.IndexOf(new_item)]);

                                        // обновление зн-я в ьазу
                                        Type type = typeof(EntityT);
                                        PropertyInfo pi = type.GetProperty(langPref + propName);
                                        pi.SetValue(db_value, Convert.ChangeType(resValue, pi.PropertyType), null);

                                    }
                                    catch (Exception ex)
                                    {
                                        var msg = ex.Message;
                                    }
                                }
                            }
                        }

                        items_to_add.Add(db_value);
                    }
                }
            }

            return items_to_add;
        }

        public static List<EntityT> MakeListUpdate<TResponseList, EntityT, BaseT>(
            List<int> repoList,
            Func<EsiResponse<TResponseList>> esi_request,
            List<string> multLangProperties,
            Func<int, ELanguages, EsiResponse<BaseT>> esi_requestItem
            )
            where TResponseList : List<int>, ISsoResult
            where BaseT : class, ISsoResult
            where EntityT : class, BaseT, new()
        {
            var new_types = Universe_Ids(repoList, esi_request);
            return Universe_ItemUpdates<EntityT, BaseT>(new_types, multLangProperties, esi_requestItem);
        }

        static List<int> Universe_Ids<TResponse>(
            List<int> cur_type_ids,
            Func<EsiResponse<TResponse>> esi_request
            )
            where TResponse : List<int>, ISsoResult
        {
            // Запрос к esi
            var requests = esi_request();

            // Запрос с types из БД и сравнение
            //  НЕ перезакачивать имеющиеся записи
            //List<int> types_to_add = requests.Data.Where(x => !cur_type_ids.Contains(x)).ToList();
            // Перезакачивать имеющиеся записи
            List<int> types_to_add = requests.Data.ToList();


            return types_to_add;
        }

        public static List<EntityT> MakeListUpdate<TResponseList, EntityT, BaseT>(
            List<int> repoList,
            Func<int, EsiResponse<TResponseList>> esi_request,
            List<string> multLangProperties,
            Func<int, ELanguages, EsiResponse<BaseT>> esi_requestItem
            )
            where TResponseList : List<int>, ISsoResult
            where BaseT : class, ISsoResult
            where EntityT : class, BaseT, new()
        {
            var new_types = Universe_ListIds(repoList, esi_request);
            return Universe_ItemUpdates<EntityT, BaseT>(new_types, multLangProperties, esi_requestItem);
        }

        static List<int> Universe_ListIds<TResponse>(
            List<int> cur_type_ids,
            Func<int, EsiResponse<TResponse>> esi_request
            )
            where TResponse : List<int>, ISsoResult
        {
            // Запрос к esi
            var requests = EsiConnector_AutoPaging(esi_request);

            // Запрос с types из БД и сравнение
            List<int> types_to_add = requests.Where(x => x.isSuccess).SelectMany(x => x.Data).Where(x => !cur_type_ids.Contains(x)).ToList();

            return types_to_add;
        }

        static List<RequestResult<T>> EsiConnector_AutoPaging<T>(
            Func<int, EsiResponse<T>> esi_request
            )
            where T : List<int>, ISsoResult
        {
            var result_requests = EsiConnectorGeneric.Auto_Paging(esi_request);

            var results = new List<RequestResult<T>>();
            foreach (var esi_result in result_requests)
            {
                results.Add(
                    new RequestResult<T>(esi_result.isSuccess,
                    esi_result.StatusCode,
                    esi_result.Data,
                    esi_result.LastModified,
                    esi_result.Date)
                );
            }
            return results;
        }
        static RequestResult<T> EsiConnector<T>(Func<ELanguages, EsiResponse<T>> esi_request, ELanguages lang)
            where T : ISsoResult
        {
            var esi_result = esi_request(lang);
            return new RequestResult<T>(esi_result.isSuccess,
                    esi_result.StatusCode,
                    esi_result.Data,
                    esi_result.LastModified,
                    esi_result.Date);
        }
        static RequestResult<T> EsiConnector<T>(Func<EsiResponse<T>> esi_request)
            where T : ISsoResult
        {
            var esi_result = esi_request();
            return new RequestResult<T>(esi_result.isSuccess,
                    esi_result.StatusCode,
                    esi_result.Data,
                    esi_result.LastModified,
                    esi_result.Date);
        }
        static List<EntityT> Universe_ItemUpdates<EntityT, BaseT>(
            List<int> new_types,
            List<string> multLangProperties,
            Func<int, ELanguages, EsiResponse<BaseT>> esi_request
            )
            where BaseT : class, ISsoResult
            where EntityT : class, BaseT, new()
        {
            var types_to_add = new List<EntityT>();

            Parallel.ForEach(new_types, type_id =>
            {
                var typeInfo = Generic.ItemWithMultiLangPropertiesAsync<EntityT, BaseT>(esi_request, type_id, multLangProperties);

                if (typeInfo != null)
                    types_to_add.Add(typeInfo);

            });

            return types_to_add;
        }
    }
}
