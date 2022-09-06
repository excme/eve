using System;
using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Get /contracts/public/items/{contract_id}/
    /// </summary>
    public class ContractsItemsResult:List<ContractsItemsResult.ContractsItem>, ISsoResult
    {
        public class ContractsItem: BaseContractsItems
        {
            public bool is_blueprint_copy { get; set; }
            public long item_id { get; set; }
            public int material_efficiency { get; set; }
            public int runs { get; set; }
            public int time_efficiency { get; set; }
        }
        public class BaseContractsItems
        {
            public long record_id { get; set; }
            public int quantity { get; set; }
            public int type_id { get; set; }
            public bool is_included { get; set; }
        }
    }
    /// <summary>
    /// GET /characters/{character_id}/contracts/{contract_id}/items/
    /// </summary>
    public class CharacterContractsItemsResult : List<CharacterContractsItemsResult.ContractsItem>, ISsoResult
    {
        public class ContractsItem: ContractsItemsResult.BaseContractsItems
        {
            public bool is_singleton { get; set; }
            public int raw_quantity { get; set; }
        }
    }
    /// <summary>
    /// GET /corporations/{corporation_id}/contracts/{contract_id}/items/
    /// </summary>
    public class CorporationContractsItemsRresult : List<CorporationContractsItemsRresult.ContractsItem>, ISsoResult
    {
        public class ContractsItem : ContractsItemsResult.BaseContractsItems
        {
            public bool is_singleton { get; set; }
            public int raw_quantity { get; set; }
        }
    }

}
