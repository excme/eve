//using eveDirect.Databases;
using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using System.Collections.Generic;
using System.Linq;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        /// <summary>
        /// Завершение участия персонажа в альянсе
        /// </summary>
        public void Characters_UpdateEndAllianceMembering(EveOnlineCorporationAllianceHistory allianceHistory_Item)
        {
            using var dbContext = new PublicContext(_options);

            // Записи истории альянса у персонажей
            var characters_allyHistories = dbContext.EveDirect_CharacterAllianceHistory
                .Where(x => x.allyHistory_recordId == allianceHistory_Item.record_id)
                .ToList();

            if (characters_allyHistories.Any())
            {
                // Устанавливаем дату завершения участия в альянсе
                characters_allyHistories.ForEach(item =>
                {
                    item.end = allianceHistory_Item.end_date;
                });
                dbContext.SaveChanges();

                // Уведомляем подписчиков
                characters_allyHistories.ForEach(item => {
                    _eventBus.Publish(new CharacterBeforeChangeAllianceIntegrationEvent(item.character_id, item.alliance_id));
                });
            }
        }

        /// <summary>
        /// История альянсов у персонажа, когда он был в корпорации
        /// </summary>
        public void Character_AllianceHistoryInCorporation(int charater_id, EveOnlineCharacterCorpHistory corpHistoryItem, List<EveOnlineCorporationAllianceHistory> allianceHistoriesItems)
        {
            using var dbContext = new PublicContext(_options);


        }
    }
}
