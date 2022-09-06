using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineContract : ContractsResult.Contract
    {
        // Если появится желание запихнуть в эту таблицу приватные контракты персонажей и корпораций, то это будет нарушать установку из ТЗ, что персонажи и корпорации должны ъранить свои данные в отдельных базах данных
        [Key]
        public new int contract_id { get; set; }

        public bool actual { get; set; }
        public int region_id { get; set; }

        public virtual List<EveOnlineContractItem> items { get; set; }
        public virtual List<EveOnlineContractBid> bids { get; set; }

        public EveOnlineContract()
        {

        }
        public EveOnlineContract(ContractsResult.Contract data)
        {
            buyout = data.buyout;
            collateral = data.collateral;
            contract_id = data.contract_id;
            date_expired = data.date_expired;
            date_issued = data.date_issued;
            days_to_complete = data.days_to_complete;
            duration_days = (data.date_expired - data.date_issued).Value.TotalDays.ToInt32();
            end_location_id = data.end_location_id;
            for_corporation = data.for_corporation;
            issuer_corporation_id = data.issuer_corporation_id;
            issuer_id = data.issuer_id;
            price = data.price;
            reward = data.reward;
            start_location_id = data.start_location_id;
            title = data.title;
            type = data.type;
            volume = data.volume;
        }
    }
}