using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/wallets/{division}/transactions/
    /// </summary>
    public class WalletTransactionsCorporationResult : List<WalletTransactionsCorporationResult.WalletTransactionsItem>, ISsoResult
    {
        public class WalletTransactionsItem
        {
            public int client_id { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime date { get; set; }
            public bool is_buy { get; set; }
            public long journal_ref_id { get; set; }
            public long location_id { get; set; }
            public int quantity { get; set; }
            public long transaction_id { get; set; }
            public int type_id { get; set; }
            public double unit_price { get; set; }
        }
    }
    /// <summary>
    /// GET /characters/{character_id}/wallet/transactions/
    /// </summary>
    public class WalletTransactionsCharacterResult : List<WalletTransactionsCharacterResult.WalletTransactionsCharacterItem>, ISsoResult
    {
        public class WalletTransactionsCharacterItem : WalletTransactionsCorporationResult.WalletTransactionsItem
        {
            public bool is_personal { get; set; }
        }
    }
}
