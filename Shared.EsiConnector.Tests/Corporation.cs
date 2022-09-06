using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Corporation:BaseConnector
    {
        /// <summary>
        /// GET /corporations/{corporation_id}/shareholders/
        /// </summary>
        [Fact]
        public void CorporationShareholdersResult()
        {
            ExecuteAndOutput(connector.Corporation.Shareholders());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/
        /// </summary>
        [Fact]
        public void CorporationResult()
        {
            ExecuteAndOutput(connector.Corporation.Information(corporationId));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/alliancehistory/
        /// </summary>
        [Fact]
        public void CorporationAllianceHistoryResult()
        {
            ExecuteAndOutput(connector.Corporation.AllianceHistory(corporationId));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/members/
        /// </summary>
        [Fact]
        public void CorporationMembersResult()
        {
            ExecuteAndOutput(connector.Corporation.Members());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/roles/
        /// </summary>
        [Fact]
        public void CorporationRolesResult()
        {
            ExecuteAndOutput(connector.Corporation.Roles());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/roles/history/
        /// </summary>
        [Fact]
        public void CorporationRolesHistoryResult()
        {
            ExecuteAndOutput(connector.Corporation.RolesHistory());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/icons/
        /// </summary>
        [Fact]
        public void CorporationIconsResult()
        {
            ExecuteAndOutput(connector.Corporation.Icons(corporationId));
        }

        /// <summary>
        /// GET /corporations/npccorps/
        /// </summary>
        [Fact]
        public void CorporationNpccorpsResult()
        {
            ExecuteAndOutput(connector.Corporation.NpcCorps());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/structures/
        /// </summary>
        [Fact]
        public void CorporationStructuresResult()
        {
            ExecuteAndOutput(connector.Corporation.Structures());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/divisions/
        /// </summary>
        [Fact]
        public void CorporationDivisionsResult()
        {
            ExecuteAndOutput(connector.Corporation.Divisions());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/membertracking/
        /// </summary>
        [Fact]
        public void CorporationMembertrackingResult()
        {
            ExecuteAndOutput(connector.Corporation.MemberTracking());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/members/limit/
        /// </summary>
        [Fact]
        public void CorporationMembersLimitResult()
        {
            //ExecuteAndOutput(connector.Corporation.MemberLimit());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/titles/
        /// </summary>
        [Fact]
        public void CorporationTitlesResult()
        {
            ExecuteAndOutput(connector.Corporation.Titles());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/members/titles/
        /// </summary>
        [Fact]
        public void CorporationMembersTitlesResult()
        {
            ExecuteAndOutput(connector.Corporation.MemberTitles());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/blueprints/
        /// </summary>
        [Fact]
        public void CorporationBlueprintsResult()
        {
            ExecuteAndOutput(connector.Corporation.Blueprints());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/standings/
        /// </summary>
        [Fact]
        public void CorporationStandingsResult()
        {
            ExecuteAndOutput(connector.Corporation.Standings());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/starbases/
        /// </summary>
        [Fact]
        public void CorporationStarbasesResult()
        {
            ExecuteAndOutput(connector.Corporation.Starbases());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/starbases/{starbase_id}/
        /// </summary>
        [Fact]
        public void CorporationStarbasesInfoResult()
        {
            var starbaseId = 1023069081871;
            var system_id = 30004174;
            ExecuteAndOutput(connector.Corporation.Starbase(starbaseId, system_id));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/containers/logs/
        /// </summary>
        [Fact]
        public void CorporationContainersLogResult()
        {
            ExecuteAndOutput(connector.Corporation.ContainerLogs());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/facilities/
        /// </summary>
        [Fact]
        public void CorporationFacilitiesResult()
        {
            ExecuteAndOutput(connector.Corporation.Facilities());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/medals/
        /// </summary>
        [Fact]
        public void CorporationMedalsResult()
        {
            ExecuteAndOutput(connector.Corporation.Medals());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/medals/issued/
        /// </summary>
        [Fact]
        public void CorporationMedalsIssuedResult()
        {
            ExecuteAndOutput(connector.Corporation.MedalsIssued());
        }

        ///// <summary>
        ///// GET /corporations/{corporation_id}/outposts/
        ///// </summary>
        //[Fact]
        //public void CorporationOutpostsResult()
        //{
        //    ExecuteAndOutput(connector.Corporation.Outposts());
        //}

        ///// <summary>
        ///// GET /corporations/{corporation_id}/outposts/{outpost_id}/
        ///// </summary>
        //[Fact]
        //public void CorporationOutpostsInfoResult()
        //{
        //    var outpostId = 0;
        //    ExecuteAndOutput(connector.Corporation.Outpost(outpostId));
        //}
    }
}
