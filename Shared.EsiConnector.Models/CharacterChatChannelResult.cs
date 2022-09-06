using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/chat_channels/
    /// </summary>
    public class CharacterChatChannelResult:List<CharacterChatChannelResult.CharacterChatChannelItem>
    {
        public class CharacterChatChannelItem {
            public int channel_id { get; set; }
            public string name { get; set; }
            public string owner_id { get; set; }
            public bool has_password { get; set; }
            public string motd { get; set; }
            public List<CharacterChatChannelItemAO> allowed { get; set; }
            public List<CharacterChatChannelItemAO> operators { get; set; }
            public List<CharacterChatChannelItemMB> blocked { get; set; }
            public List<CharacterChatChannelItemMB> muted { get; set; }

        }

        public class CharacterChatChannelItemAO
        {
            public string accessor_id { get; set; }
            public string accessor_type { get; set; }
        }

        public class CharacterChatChannelItemMB: CharacterChatChannelItemAO
        {
            public string end_at { get; set; }
            public string reason { get; set; }
        }
    }
}
