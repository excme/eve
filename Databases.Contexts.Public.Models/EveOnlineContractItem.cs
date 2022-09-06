using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineContractItem : ContractsItemsResult.ContractsItem
    {
        [Key]
        public new long record_id { get; set; }

        public int contract_id { get; set; }
        public virtual EveOnlineContract contract { get; set; }
        //public bool isDisable { get; set; }

        public EveOnlineContractItem()
        {

        }
        public EveOnlineContractItem(ContractsItemsResult.ContractsItem data)
        {
            is_blueprint_copy = data.is_blueprint_copy;
            item_id = data.item_id;
            material_efficiency = data.material_efficiency;
            runs = data.runs;
            time_efficiency = data.time_efficiency;
            record_id = data.record_id;
            quantity = data.quantity;
            type_id = data.type_id;
            is_included = data.is_included;
        }
    }
}