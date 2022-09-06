using System;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationNpcs : ConnectorJob
    {
        public CorporationNpcs(IReadWrite repoPublicCommon, 
            ILogger<CorporationNpcs> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            var ncp_request = EsiConnector(esiClient.Corporation.NpcCorps);

            if (ncp_request.isSuccess)
            {
                var updated = _repoPublicCommon.Corporation_Update_Ncp(ncp_request.Data);
                if(updated)
                    _jobResult.Value = ncp_request.Data.Count;
            }

            
        }
    }
}
