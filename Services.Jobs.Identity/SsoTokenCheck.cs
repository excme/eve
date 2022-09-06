using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using eveDirect.Repo.ReadWrite;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Services.EsiConnector;

namespace eveDirect.Services.Jobs.Identity
{
    public class SsoTokenCheck : ConnectorJob
    {
        public SsoTokenCheck(IReadWrite repoPublicCommon, ILogger<SsoTokenCheck> logger, string client_id, string secret) 
            : base(logger, client_id, secret)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public SsoTokenCheck(IReadWrite repoPublicCommon, ILogger<SsoTokenCheck> logger, IConfiguration configuration)
            : this(repoPublicCommon, logger, configuration["SSO:ClientId"], configuration["SSO:Secret"]) { }

        public override void Execute()
        {
            // Все sso 
            List<IdentitySso> ssos = _repoPublicCommon.Sso_Get(where: x => x.status == ESsoStatus.Active);

            //await ssos.ParallelForEachAsync(async sso => {
            Parallel.ForEach(ssos, sso =>
            { 
                Simple(sso);
            });
        }
        public void Simple(IdentitySso sso)
        {
            //var url = esiClient.SSO.CreateAuthenticationUrl(Scope.AllScopeNames().Select(x => x.Name).ToList());
            // Navigate Url - get code
            SsoToken model = esiClient.SSO.GetToken(GrantType.RefreshToken, sso.refresh_token);
            var characterData = esiClient.SSO.Verify(model);
        }
    }
}
