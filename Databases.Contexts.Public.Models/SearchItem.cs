namespace eveDirect.Databases.Contexts.Public.Models
{
    public class SearchItem
    {
        public int id { get; set; }
        public long item_id { get; set; }
        public string title { get; set; }
        public ESearchItemType type { get; set; }
    }
    public enum ESearchItemType : byte
    {
        unknown = 0,
        character = 1,
        corporation = 2,
        alliance = 3,
        type = 4,
        location = 5,
    }
}
