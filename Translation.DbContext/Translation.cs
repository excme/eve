using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eveDirect.Translation.DbContext
{
    public class Translation
    {
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Ключ-перевод
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// Ссылка
        /// </summary>
        public string reference { get; set; }
        /// <summary>
        /// Перевод
        /// </summary>
        public string description { get; set; }

        public TranslationItem ru { get; set; }
        public TranslationItem en { get; set; }
        public TranslationItem ge { get; set; }
        public TranslationItem fr { get; set; }
        public TranslationItem ja { get; set; }
        public TranslationItem ko { get; set; }
        public TranslationItem zh { get; set; }
    }
    public class TranslationItem
    {
        public string val { get; set; }
        /// <summary>
        /// Проверено
        /// </summary>
        public bool approval { get; set; }
        /// <summary>
        /// История изменений
        /// </summary>
        //public List<ChangeRecord> changes { get; set; }
    }
}
