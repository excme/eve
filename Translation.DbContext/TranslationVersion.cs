namespace eveDirect.Translation.DbContext
{
    using System.ComponentModel.DataAnnotations;
    public class TranslationVersion
    {
        [Key]
        public string lang { get; set; }

        public int version { get; set; }
    }
}
