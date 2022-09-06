using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Killmails : BaseConnector
    {
        /// <summary>
        /// GET /killmails/{killmail_id}/{killmail_hash}/
        /// </summary>
        [Fact]
        public void KillMailInfoResult()
        {
            int killmailID = 65572415;
            string kKillmailHash = "af785b214a6e8950acad8e17b6dcc6ca24366c82";
            ExecuteAndOutput(connector.Killmails.Information(killmailID, kKillmailHash));
        }

        /// <summary>
        /// GET /characters/{character_id}/killmails/recent/
        /// </summary>
        [Fact]
        public void CharacterKillmailsRecentResult()
        {
            ExecuteAndOutput(connector.Killmails.ForCharacter());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/killmails/recent/
        /// </summary>
        [Fact]
        public void CorporationKillmailsRecentResult()
        {
            ExecuteAndOutput(connector.Killmails.ForCorporation());
        }
    }
}
