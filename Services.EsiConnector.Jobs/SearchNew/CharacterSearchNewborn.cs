using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using eveDirect.Services.Jobs.Core;
using eveDirect.Services.EsiConnector;
using eveDirect.Repo.ReadWrite;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Поиск новорожденных персонажей
    /// </summary>
    public class CharacterSearchNewborn : ConnectorJob
    {
        public CharacterSearchNewborn(
            IReadWrite repoPublicCommon,
            ILogger<CharacterSearchNewborn> logger,
            IConfiguration configuration) 
            : base(logger, configuration["Proxy:Addr"], configuration["Proxy:Port"], configuration["Proxy:User"], configuration["Proxy:Pass"])
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }

        public override void Execute()
        {
            TaskSimple();
        }

        public JobResult TaskSimple()
        {
            var search = new _GenericSearch(_repoPublicCommon, esiClient.Universe.Names,
                EsiConnector, 10);

            var result = search.TaskSimple(
                checkpointName: nameof(CharacterSearchNewborn)
            );

            // Результат
            _jobResult.Value = result.founded;
            _jobResult.subValues.Add(new JobResult.Item() { Name = nameof(result.start_id), Value = result.start_id });
            _jobResult.subValues.Add(new JobResult.Item() { Name = nameof(result.last_id), Value = result.last_id });

            return _jobResult;
        }
    }
}
