using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Industry:BaseConnector
    {

        /// <summary>
        /// GET /industry/facilities/
        /// </summary>
        [Fact]
        public void IndustryFacilitiesResult()
        {
            ExecuteAndOutput(connector.Industry.Facilities());
        }
        /// <summary>
        /// GET /industry/systems/
        /// </summary>
        [Fact]
        public void IndustrySystemsResult()
        {
            ExecuteAndOutput(connector.Industry.Systems());
        }
        /// <summary>
        /// GET /characters/{character_id}/industry/jobs/
        /// </summary>
        [Fact]
        public void CharacterIndustryJobsResult()
        {
            ExecuteAndOutput(connector.Industry.JobsForCharacter(true));
        }
        /// <summary>
        /// GET /characters/{character_id}/mining/
        /// </summary>
        [Fact]
        public void CharacterIndustryMiningResult()
        {
            ExecuteAndOutput(connector.Industry.MiningLedger());
        }
        /// <summary>
        /// GET /corporation/{corporation_id}/mining/observers/
        /// </summary>
        [Fact]
        public void CorporationIndustryMiningObserverResult()
        {
            ExecuteAndOutput(connector.Industry.Observers());
        }
        /// <summary>
        /// GET /corporation/{corporation_id}/mining/observers/{observer_id}/
        /// </summary>
        [Fact]
        public void CorporationIndustryMiningObserverItemResult()
        {
            var observerId = 1025989077959;
            ExecuteAndOutput(connector.Industry.ObservedMining(observerId));
        }
        /// <summary>
        /// GET /corporations/{corporation_id}/industry/jobs/
        /// </summary>
        [Fact]
        public void CorporationIndustryJobsResult()
        {
            ExecuteAndOutput(connector.Industry.JobsForCorporation(true));
        }
        /// <summary>
        /// GET /corporation/{corporation_id}/mining/extractions/
        /// </summary>
        [Fact]
        public void CorporationMiningExtractionsResult()
        {
            ExecuteAndOutput(connector.Industry.Extractions());
        }
    }
}
