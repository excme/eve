using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterCorpHistory : ConnectorJob
    {
        private int Part_of;
        public CharacterCorpHistory(IReadWrite repoPublicCommon,
            ILogger<CharacterCorpHistory> logger, int part_of = 1000, int max_Items_To_Request = 6000) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            Part_of = part_of;
            Max_Items_To_Request = max_Items_To_Request;
        }
        public override void Execute()
        {
            // Клатбище
            Expression<Func<EveOnlineCharacter, bool>> not_Doomheim = x => x.corporation_id != 1000001;

            // Запрашиваем только тех, у кого нет истории
            // TODO: Изменить, чтобы без историйКорпораций обновлялись полностью. а остальные по part_of
            var to_updating = _repoPublicCommon.Character_Ids(
                where: x => x.corpHistoryCount == 0,
                part_of: Part_of
            );

            _jobResult.subValues.Add(new JobResult.Item() { Name = "count", Value = to_updating.Count });

            var list = AttachProgressBarToList(to_updating);
            //await list.ParallelForEachAsync(async character_id =>
            //foreach (var character_id in list)
            Parallel.ForEach(list, character_id =>
            {
                var b = SimpleCharacter(character_id);
                if (b)
                    _jobResult.Value++;
            });

            
        }
        public bool SimpleCharacter(int character_id)
        {
            // Выполнение запроса
            var result = EsiConnector(esiClient.Character.CorporationHistory, character_id);
            if (result.isSuccess)
                _repoPublicCommon.Character_UpdateCorporationHistory(character_id, result.Data);

            return result.isSuccess;
        }
    }
}
