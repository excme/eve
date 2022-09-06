using System.Runtime.Serialization;

namespace eveDirect.Shared.WebHost
{
    public enum EJobsCategories : byte
    {
        public_characters = 1,
        public_corp = 2,
        public_universe = 3,
        public_warface = 4,
        public_ally = 8,

        common_view = 5,
        public_market = 6,
        identity = 7,

        search_ids = 9,

        [EnumMember(Value = "default")]
        _default = 10,
        processing = 11
    }
}
