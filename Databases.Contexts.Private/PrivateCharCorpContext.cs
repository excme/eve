using eveDirect.Databases.Contexts.Private.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace eveDirect.Databases.Contexts
{
    public class PrivateCharCorpContext : PrivateContext//, IPublicContractsContext
    {
        // Assets
        public DbSet<EveOnlineAssetSpanshotItem> Eveonline_AssetSpanshotItems { get; set; }
        public DbSet<EveOnlineAssetChangeItem> Eveonline_AssetChangeItems { get; set; }
        public DbSet<EveOnlineAssetsReport> Eveonline_AssetReports { get; set; }

        // Bookmarks
        public DbSet<EveOnlineBookmark> Eveonline_Bookmarks { get; set; }
        public DbSet<EveOnlineBookmarksFolder> Eveonline_BookmarksFolders { get; set; }

        // Character
        // Corporation
        public DbSet<EveOnlineBlueprint> Eveonline_Blueprints { get; set; }

        // Contracts
        //public DbSet<EveOnlineContract> Eveonline_Contracts { get; set; }
        //public DbSet<EveOnlineContractRef> Eveonline_ContractRefs { get; set; }
        //public DbSet<EveOnlineContractItem> Eveonline_ContractItems { get; set; }
        //public DbSet<EveOnlineContractBid> Eveonline_ContractBids { get; set; }

        // Faction Warfare
        // TODO: Проверить свойства моделей для персонажа и корпорации
        public DbSet<EveOnlineCharacterFactionWarfareStat> Eveonline_CharacterFactionWarfareStats { get; set; }

        // Industry
        public DbSet<EveOnlineIndustryJob> Eveonline_IndustryJobs { get; set; }

        // Market
        public DbSet<EveOnlineMarketOrder> Eveonline_MarketOrders { get; set; }

        // Wallet
        public DbSet<EveOnlineWalletBalance> Eveonline_WalletBalances { get; set; }
    }
}
