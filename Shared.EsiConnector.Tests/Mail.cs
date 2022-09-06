using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Mail : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/mail/
        /// </summary>
        [Fact]
        public void CharacterMailsResult()
        {
            ExecuteAndOutput(connector.Mail.Headers());
        }

        /// <summary>
        /// GET /characters/{character_id}/mail/labels/
        /// </summary>
        [Fact]
        public void CharacterMailLabelsResult()
        {
            ExecuteAndOutput(connector.Mail.Labels());
        }

        /// <summary>
        /// GET /characters/{character_id}/mail/lists/
        /// </summary>
        [Fact]
        public void CharacterMailListsResult()
        {
            ExecuteAndOutput(connector.Mail.MailingLists());
        }

        /// <summary>
        /// GET /characters/{character_id}/mail/{mail_id}/
        /// </summary>
        [Fact]
        public void CharacterMailInfoResult()
        {
            var mailId = 369762959;
            ExecuteAndOutput(connector.Mail.Info(mailId));
        }


    }
}
