using System.Threading.Tasks;

namespace eveDirect.Clients.Web.Services
{
    public interface ICheckExistService
    {
        /// <summary>
        /// Существует ли персонаж
        /// </summary>
        Task<bool> Character_Exist(int character_id);
        /// <summary>
        /// Существует ли корпорация
        /// </summary>
        Task<bool> Corporation_Exist(int corporation_id);
        /// <summary>
        /// Существует ли альнс
        /// </summary>
        Task<bool> Alliance_Exist(int alliance_id);
        /// <summary>
        /// Существует ли контракт
        /// </summary>
        Task<bool> Contract_Exist(int contract_id);
        /// <summary>
        /// Существует ли ордер
        /// </summary>
        //Task<bool> Order_Exist(int order_id);

        Task AlliancesIdsRanges_Update();
        Task CorporationsIdsRanges_Update();
        Task CharactersIdsRanges_Update();
        Task ContractsIdsRanges_Update();
        //Task OrdersIdsRanges_Update();
    }
}
