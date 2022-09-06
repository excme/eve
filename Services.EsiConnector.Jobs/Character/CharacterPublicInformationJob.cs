using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Обновление публичной информации персонажей
    /// </summary>
    public class CharacterPublicInformationJob : ConnectorJob
    {
        private int Part_of;

        public CharacterPublicInformationJob(
            IReadWrite repoPublicCommon,
            ILogger<CharacterPublicInformationJob> logger,
            int part_of = 3000): this(repoPublicCommon, logger, null, null, null, null, part_of){ }

        public CharacterPublicInformationJob(
            IReadWrite repoPublicCommon, 
            ILogger<CharacterPublicInformationJob> logger,
            string proxy_addr,
            string proxy_port,
            string proxy_user,
            string proxy_pass,
            int part_of = 3000)
            : base(logger, proxy_addr, proxy_port, proxy_user, proxy_pass)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));

            Part_of = part_of;
        }
        public override void Execute()
        {
            // не Клатбище
            var to_updating = _repoPublicCommon.Character_Ids(
                where: x => x.corporation_id != 1000001, 
                part_of: Part_of
            );

            // Если нет имени
            var not_name = _repoPublicCommon.Character_Ids(where: x => x.name == null);
            if (not_name?.Any() ?? false)
                to_updating.AddRange(not_name);

            _jobResult.subValues.Add(new JobResult.Item() { Name = "count", Value = to_updating.Count });

            //var list = AttachProgressBarToList(to_updating);
            var list = AttachProgressBarToList(not_name);
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
            var request = EsiConnector(esiClient.Character.Information, character_id);

            if (request.isSuccess)
            {
                bool updated = _repoPublicCommon.Character_PublicInformation_Update(character_id, request.Data);
                return updated;
            }
            else if((int)request.StatusCode == 404)
            {
                _repoPublicCommon.Character_Delete(character_id);
                LogInfo($"Удален: {character_id}");
            }

            return false;
        }
    }
}
