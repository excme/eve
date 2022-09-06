using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using eveDirect.BaseRepo;
using eveDirect.Repo.PublicReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eveDirect.Repo.PublicReadOnly
{
    public interface IReadOnly
    {
        //Task<bool> Character_Exist(int character_id);

        Task<CharacterPublicModel> Character_PublicInfo(int character_id);
        Task<List<CharacterPublicModel>> Character_PublicInfos(int[] character_id);

        Task<List<NameModel<int>>> Character_Name(int character_id);
        Task<List<NameModel<int>>> Character_Names(int[] character_ids);

        Task<CorporationPublicModel> Corporation_PublicInfo(int corporations_id);
        Task<List<CorporationPublicModel>> Corporation_PublicInfos(int[] corporations_ids);

        Task<List<CharacterCorporationHistoryModel.CorporationHistoryItem>> Character_CorporationHistory(int character_id);
        Task<List<CharacterCorporationHistoryModel>> Characters_CorporationHistory(int[] character_ids);

        Task<LoadResult> Character_MarketContracts(int character_id, DataSourceLoadOptionsBase lo);

        Task<NameModel<int>> Corporation_Name(int corporation_id);
        Task<List<NameModel<int>>> Corporation_Names(int corporation_id);
        Task<List<NameModel<int>>> Corporation_Names(int[] corporations_ids);

        Task<IdRanges> Character_IdRanges();
        Task<IdRanges> Corporation_IdRanges();
        Task<IdRanges> Alliances_IdRanges();
        Task<IdRanges> Contracts_IdRanges();
        Task<IdRanges> Orders_IdRanges();

        Task Characters_CalcIdRanges();
        Task Corporations_CalcIdRanges();
        Task Alliances_CalcIdRanges();
        Task Contracts_CalcIdRanges();
        Task Orders_CalcIdRanges();

        Task<NameModel<int>> Alliance_Name(int alliance_id);
        Task<List<NameModel<int>>> Alliance_Names(int alliance_id);
        Task<List<NameModel<int>>> Alliance_Names(int[] alliance_ids);
        Task<LoadResult> Alliance_CurrentCharacters(int alliance_id, DataSourceLoadOptionsBase lo);

        Task<CharacterPreview> Character_Preview(int character_id);
        Task<CorporationPreview> Corporation_Preview(int corporation_id);

        Task<List<CorporationMembersMigrationItem>> Corporation_MembersMigrations(int corporation_id, int page);
        Task<List<CorporationAllianceHistory.AllianceHistoryItem>> Corporation_AllianceHistory(int corporation_id);
        Task<List<CorporationAllianceHistory>> Corporation_AllianceHistory(int[] corporation_ids);

        Task<List<MarketRegionModel>> Market_AllRegionsAndSystems();
        Task<List<NameModel<long>>> Market_AllRegions();
        Task<LoadResult> Market_Orders(int type_id, bool is_buy, int[] systems, DataSourceLoadOptionsBase options);
        Task<List<int>> Market_ActiveOrdersRegionsAndSystems();
        Task<List<MarketGroupModel>> Market_Groups(string lang);

        Task<LoadResult> Contracts_List(int type_id, int[] regions, DataSourceLoadOptionsBase lo);
        Task<ContractDetail> Contracts_Detail(int contract_id);
        Task<List<ContractBid>> Contracts_Bids(int contract_id);
        Task<List<ContractItem>> Contracts_Items(int contract_id);

        Task<List<UniverseLocationName>> Universe_Location_Names(Expression<Func<UniverseLocationName, bool>> where = null);
        Task<List<UniverseTypeDetail>> Universe_Types_Names(Expression<Func<UniverseTypeDetail, bool>> where = null, string lang = "en");
        Task<List<MarketGroupModel>> Universe_Type_Groups(string lang);

        Task<List<CharacterNewbornItemModel>> CharacterNewbornItems();
        Task<AlliancePreview> Alliance_Preview(int i);
        Task CharacterNewbornItems_Calc();
        Task<List<CharacterAllianceHistoryModel.AllianceHistoryItem>> Character_AllianceHistory(int character_id);

        //Task<List<KeyValuePair<int, int>>> CharacterNewbornChartItems();
        Task CharacterNewbornItems_Add(int character_id);
        //Task<List<CharacterNewbornItemModel>> CharacterCCPNewbornItems();
        //Task CharacterCCPNewbornItems_Calc();

        Task<LoadResult> Characters_MigrationsRoot_Items(DataSourceLoadOptions loadOptions);
        //void Characters_MigrationsRoot_UpdateTotalCount(int total_Count);
        //Task Characters_MigrationsRoot_UpdateTotalCount();

        Task<List<SearchItemModel>> Search_BySubValue(string subString);
    }
}
