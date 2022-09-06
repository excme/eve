using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using eveDirect.Shared;
using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.EsiConnector.Models;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public static class Generic
    {
        public static EntityT ItemWithMultiLangPropertiesAsync<EntityT, BaseT>(
            Func<int, ELanguages, EsiResponse<BaseT>> esi_request,
            int arg1,
            List<string> multiLangProps)
            where BaseT : class, ISsoResult
            where EntityT : class, BaseT, new()
        {
            RequestResult<BaseT> base_info_request = null;
            do
            {
                base_info_request = EsiConnector(esi_request, arg1, ELanguages.en_us);
                if (base_info_request.isSuccess)
                    break;
            } while (true);

            if (base_info_request.isSuccess)
            {
                var db_value = new EntityT();
                // Обновление ключа
                PropertyInfo[] properties = typeof(EntityT).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    KeyAttribute attribute =
                        Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;

                    if (attribute != null)
                    {
                        property.SetValue(db_value, Convert.ChangeType(arg1, property.PropertyType), null);
                        // Указание параметров не-multiLang
                        db_value.UpdateProperties(base_info_request.Data);

                        // Перебор текущих языков EsiConnector
                        var lang_list = Enum.GetValues(typeof(ELanguages)).Cast<ELanguages>();
                        foreach (ELanguages lang in lang_list)
                        {
                            RequestResult<BaseT> reqResult = null;
                            do
                            {
                                reqResult = EsiConnector(esi_request, arg1, lang);
                            } while (!reqResult.isSuccess);

                            if (reqResult.isSuccess)
                            {
                                // Перебор мультиязычных свойств
                                foreach (string propName in multiLangProps)
                                {
                                    string langPref = lang.ToString().Substring(0, 2);

                                    try
                                    {
                                        // зн-е из запроса
                                        Type restype = typeof(BaseT);
                                        PropertyInfo resPi = restype.GetProperty(propName.ToLower());
                                        var resValue = resPi.GetValue(reqResult.Data);

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
                    }
                }

                return db_value;
            }
            return default;
        }

        //public static EntityT ItemWithMultiLangPropertiesAsync<EntityT, BaseT>(
        //    Func< ELanguages, EsiResponse<BaseT>> esi_request,
        //    int arg1,
        //    List<string> multiLangProps)
        //    where BaseT : class, ISsoResult
        //    where EntityT : class, BaseT, new()
        //{
        //    var base_info_request = EsiConnector(esi_request, ELanguages.en_us);

        //    if (base_info_request.isSuccess)
        //    {
        //        var db_value = new EntityT();
        //        // Обновление ключа
        //        PropertyInfo[] properties = typeof(EntityT).GetProperties();
        //        foreach (PropertyInfo property in properties)
        //        {
        //            KeyAttribute attribute =
        //                Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;

        //            if (attribute != null)
        //            {
        //                property.SetValue(db_value, Convert.ChangeType(arg1, property.PropertyType), null);
        //                // Указание параметров не-multiLang
        //                db_value.UpdateProperties(base_info_request.Data);

        //                // Перебор текущих языков EsiConnector
        //                var lang_list = Enum.GetValues(typeof(ELanguages)).Cast<ELanguages>();
        //                foreach (ELanguages lang in lang_list)
        //                {
        //                    var reqResult = EsiConnector(esi_request, lang);

        //                    // Перебор мультиязычных свойств
        //                    foreach (string propName in multiLangProps)
        //                    {
        //                        string langPref = lang.ToString().Substring(0, 2);

        //                        try
        //                        {
        //                            // зн-е из запроса
        //                            Type restype = typeof(BaseT);
        //                            PropertyInfo resPi = restype.GetProperty(propName);
        //                            string resValue = resPi.GetValue(reqResult).ToString();

        //                            // обновление зн-я в ьазу
        //                            Type type = typeof(BaseT);
        //                            PropertyInfo pi = type.GetProperty(langPref + propName);
        //                            pi.SetValue(db_value, Convert.ChangeType(resValue, pi.PropertyType), null);

        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            var msg = ex.Message;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        return db_value;
        //    }

        //    return default;
        //}

        static RequestResult<T> EsiConnector<T>(Func<int, ELanguages, EsiResponse<T>> esi_request,
            int arg1,
            ELanguages lang = ELanguages.en_us)
            where T : ISsoResult
        {
            EsiResponse<T> esi_result = esi_request(arg1, lang);
            return new RequestResult<T>(esi_result.isSuccess,
                    esi_result.StatusCode,
                    esi_result.Data,
                    esi_result.LastModified,
                    esi_result.Date);
        }
        static RequestResult<T> EsiConnector<T>(Func<ELanguages, EsiResponse<T>> esi_request,
            ELanguages lang = ELanguages.en_us)
            where T : ISsoResult
        {
            EsiResponse<T> esi_result = esi_request(lang);
            return new RequestResult<T>(esi_result.isSuccess,
                    esi_result.StatusCode,
                    esi_result.Data,
                    esi_result.LastModified,
                    esi_result.Date);
        }
    }
}
