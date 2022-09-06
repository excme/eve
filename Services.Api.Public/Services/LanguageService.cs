using eveDirect.Translation.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace eveDirect.Api.Public.Services
{
    public class LanguageService : ILanguageService
    {
        DbContextOptions<ApplicationDbContext> _options { get; }

        public LanguageService(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        /// <summary>
        /// Версии перевода по языкам
        /// </summary>
        Dictionary<string, (int version, Dictionary<string, string> values)> langVersions { get; set; } = new Dictionary<string, (int version, Dictionary<string, string> values)>();

        public void UpdateLangVersion(string lang)
        {
            using var context = new ApplicationDbContext(_options);
            var dbItem = context.TranslationVersions.FirstOrDefault(x => x.lang == lang);

            if (dbItem != null) {

                // Обновление перевода и версии
                Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
                switch (lang)
                {
                    case "ru":
                        tempDictionary = context.Translations
                            .Where(x => x.ru.val.Length > 0)
                            .Select(x => new { x.key, x.ru.val })
                            .ToDictionary(kvp => kvp.key, kvp => kvp.val);

                        break;
                    case "en":
                        tempDictionary = context.Translations
                            .Where(x => x.en.val.Length > 0)
                            .Select(x => new { x.key, x.en.val })
                            .ToDictionary(kvp => kvp.key, kvp => kvp.val);

                        break;
                    case "fr":
                        tempDictionary = context.Translations
                            .Where(x => x.fr.val.Length > 0 || x.en.val.Length > 0)
                            .Select(x => new { x.key, en = x.en.val, fr = x.fr.val })
                            .ToDictionary(kvp => kvp.key, kvp => kvp.fr != null && kvp.fr.Length > 0 ? kvp.fr : kvp.en);
                        
                        break;
                    case "de":
                        tempDictionary = context.Translations
                            .Where(x => x.ge.val.Length > 0 || x.en.val.Length > 0)
                            .Select(x => new { x.key, en = x.en.val, ge = x.ge.val })
                            .ToDictionary(kvp => kvp.key, kvp => kvp.ge != null && kvp.ge.Length > 0 ? kvp.ge : kvp.en);

                        break;
                    case "zh":
                        tempDictionary = context.Translations
                            .Where(x => x.zh.val.Length > 0 || x.en.val.Length > 0)
                            .Select(x => new { x.key, en = x.en.val, zh = x.zh.val })
                            .ToDictionary(kvp => kvp.key, kvp => kvp.zh != null && kvp.zh.Length > 0 ? kvp.zh : kvp.en);

                        break;
                    case "ko":
                        tempDictionary = context.Translations
                            .Where(x => x.ko.val.Length > 0 || x.en.val.Length > 0)
                            .Select(x => new { x.key, en = x.en.val, ko = x.ko.val })
                            .ToDictionary(kvp => kvp.key, kvp => kvp.ko != null && kvp.ko.Length > 0 ? kvp.ko : kvp.en);

                        break;
                    case "ja":
                        tempDictionary = context.Translations
                            .Where(x => x.ja.val.Length > 0 || x.en.val.Length > 0)
                            .Select(x => new { x.key, en = x.en.val, ja = x.ja.val })
                            .ToDictionary(kvp => kvp.key, kvp => kvp.ja != null && kvp.ja.Length > 0 ? kvp.ja : kvp.en);

                        break;
                }

                if (langVersions.ContainsKey(lang))
                    langVersions[lang] = (dbItem.version, tempDictionary);
                else
                    langVersions.Add(lang, (dbItem.version, tempDictionary));
            }
        }

        public int GetVersion(string lang)
        {
            if (langVersions.ContainsKey(lang))
                return langVersions[lang].version;

            return -1;
        }

        public Dictionary<string, string> GetStrings(string lang)
        {
            if (langVersions.ContainsKey(lang))
                return langVersions[lang].values;

            return null;
        }
    }

    public interface ILanguageService
    {
        void UpdateLangVersion(string lang);
        int GetVersion(string lang);
        Dictionary<string, string> GetStrings(string lang);
    }
}
