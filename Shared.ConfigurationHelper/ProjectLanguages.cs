using System.Collections.Generic;

namespace eveDirect.Shared.ConfigurationHelper
{
    public static class ProjectLanguages
    {
        public static List<string> List { get; } = new List<string>() {
             "en", "ru", "fr", "ge", "zh", "ko", "ja"
        };
    }

    //public enum ELanguage : byte
    //{
    //    en = 0,
    //    de = 1,
    //    fr = 2,
    //    ja = 3,
    //    ru = 4,
    //    zh = 5,
    //    ko = 6
    //}
}
