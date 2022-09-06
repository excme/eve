using eveDirect.Shared.EsiConnector.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineMarketRegionHistoryPriceInfo : MarketsHistoryResult.MarketsHistoryItem
    {
        public EveOnlineMarketRegionHistoryPriceInfo()
        {

        }
        public EveOnlineMarketRegionHistoryPriceInfo(MarketsHistoryResult.MarketsHistoryItem data)
        {
            UpdateValues(data);
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public int region_id { get; set; }
        public int type_id { get; set; }

        public void UpdateValues(MarketsHistoryResult.MarketsHistoryItem data)
        {
            date = data.date;
            order_count = data.order_count;
            volume = data.volume;
            highest = data.highest;
            average = data.average;
            lowest = data.lowest;
        }
    }
}