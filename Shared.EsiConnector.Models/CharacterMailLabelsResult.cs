using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/mail/labels/
    /// </summary>
    public class CharacterMailLabelsResult
    {
        public int total_unread_count { get; set; }
        public List<Label> labels { get; set; }

        public class Label
        {
            public int unread_count { get; set; }
            public int label_id { get; set; }
            public string name { get; set; }
            public EColor color { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EColor : byte
        {
            _0000fe = 1,
            _006634 = 2,
            _0099ff = 3,
            _00ff33 = 4,
            _01ffff = 5,
            _349800 = 6,
            _660066 = 7,
            _666666 = 8,
            _999999 = 9,
            _99ffff = 10,
            _9a0000 = 11,
            _ccff9a = 12,
            _e6e6e6 = 13,
            _fe0000 = 14,
            _ff6600 = 15,
            _ffff01 = 16,
            _ffffcd = 17,
            _ffffff = 18,
        }
    }
}
