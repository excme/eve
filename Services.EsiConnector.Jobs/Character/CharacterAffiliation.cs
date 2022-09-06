using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using eveDirect.Shared.Helper;
using System.Linq;
using Microsoft.Extensions.Logging;
using eveDirect.Services.Jobs.Core;
using eveDirect.Repo.ReadWrite;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterAffiliation : ConnectorJob
    {
        private readonly int Part_of;
        public CharacterAffiliation(IReadWrite repoPublicCommon, 
            ILogger<CharacterAffiliation> logger, int part_of = 1000) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            Part_of = part_of;
        }
        public override void Execute()
        {
            // Клатбище
            Expression<Func<EveOnlineCharacter, bool>> not_Doomheim = x => x.corporation_id != 1000001 && x.name != null;

            var to_updating = _repoPublicCommon.Character_Ids(
                where : not_Doomheim,
                part_of: Part_of
            );

            if (to_updating?.Any() ?? false) {

                // Разделение на отрезки по 1000
                // округление в большую сторону
                var parts = Math.Ceiling((decimal)to_updating.Count / 1000).ToInt32();

                var list = AttachProgressBarToList(Enumerable.Range(0, parts));
                //await list.ParallelForEachAsync(async index =>
                //foreach(var index in list)
                Parallel.ForEach(list, index =>
                {
                    var _index = index * 1000;
                    var av_count = to_updating.Count - _index;

                    var tempResult = SimpleTask(to_updating.GetRange(_index, av_count >= 1000 ? 1000 : av_count));

                    _jobResult.Value += tempResult.Value;
                });
            }
        }

        public JobResult SimpleTask(List<int> character_ids)
        {
            // Выполнение запроса
            var request = EsiConnector(esiClient.Character.Affiliation, character_ids);

            if (request.isSuccess)
            {
                int countUpdated = _repoPublicCommon.Character_UpdateAffiliation(request.Data, request.Date ?? DateTime.UtcNow);
                ToConsole($"R: {countUpdated}/{character_ids.Count}");

                return new JobResult() { Value = countUpdated };
            }

            return new JobResult();
        }
    }
}
