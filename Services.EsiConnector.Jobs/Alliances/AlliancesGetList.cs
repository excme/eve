using System;
using System.Collections.Generic;
using System.Linq;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Запрос списка всех альянсов
    /// </summary>
    public class AlliancesGetList : ConnectorJob
    {
        public AlliancesGetList(IReadWrite repoPublicCommon, 
            ILogger<AlliancesGetList> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));

        }
        public override void Execute()
        {
           
            List<int> nowAlliances = new List<int>();

            // Запрос
            var request = EsiConnector(esiClient.Alliance.All);
            
            if (request.isSuccess)
            {
                nowAlliances = request.Data;

                // Альянсы, которых нет в БД
                var new_AlliacesCount = _repoPublicCommon.Alliance_AddNew(nowAlliances);

                _jobResult.Value = new_AlliacesCount;
                _jobResult.subValues.Add(new JobResult.Item() { 
                    Value = nowAlliances.Count,
                    Name = "All"
                });
            }
        }
    }
}
