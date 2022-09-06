using eveDirect.BaseRepo;
using eveDirect.Clients.Web.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eveDirect.Databases.Contexts;

namespace eveDirect.Clients.Web.Services
{
    public class CheckExistService : ICheckExistService
    {
        private string remoteServiceBaseUrl { get; }
        private DbContextOptions<PublicContext> options { get; }

        public CheckExistService(IConfiguration configuration, IWebHostEnvironment environment, DbContextOptions<PublicContext> options)
        {
            this.options = options;
            remoteServiceBaseUrl = API.CommonPrivate.baseUri(configuration, environment);
        }

        IdRanges AlliancesIdsRanges { get;  set; }
        IdRanges CharactersIdsRanges { get;  set; }
        IdRanges CorporationsIdsRanges { get;  set; }
        IdRanges ContractsIdRange { get; set; }
        //IdRanges OrdersIdRange { get; set; }

        /// <summary>
        /// Проверка наличия альянса в базе. Сначала проверяется кэш
        /// </summary>
        public async Task<bool> Alliance_Exist(int alliance_id)
        {
            if(!CheckInRanges(alliance_id, AlliancesIdsRanges))
            {
                using PublicContext context = new PublicContext(options);
                return await context.EveOnline_Alliances.AnyAsync(x => x.alliance_id == alliance_id);
            }

            return true;
        }

        /// <summary>
        /// Проверка наличия персонажа в базе. Сначала проверяется кэш
        /// </summary>
        public async Task<bool> Character_Exist(int character_id)
        {
            if (!CheckInRanges(character_id, CharactersIdsRanges)) {
                using PublicContext context = new PublicContext(options);
                return await context.EveOnline_Characters.AnyAsync(x => x.character_id == character_id);
            }

            return true;
        }

        /// <summary>
        /// Проверка наличия корпорации в базе. Сначала проверяется кэш
        /// </summary>
        public async Task<bool> Corporation_Exist(int corporation_id)
        {
            if (!CheckInRanges(corporation_id, CorporationsIdsRanges)) {
                using PublicContext context = new PublicContext(options);
                return await context.EveOnline_Corporations.AnyAsync(x => x.corporation_id == corporation_id);
            }

            return true;
        }

        /// <summary>
        /// Проверка наличия контракта в базе. Сначала проверяется кэш
        /// </summary>
        public async Task<bool> Contract_Exist(int contract_id)
        {
            if (!CheckInRanges(contract_id, ContractsIdRange)) {
                using PublicContext context = new PublicContext(options);
                return await context.Eveonline_Contracts.AnyAsync(x => x.contract_id == contract_id);
            }

            return true;
        }

        /// <summary>
        /// Проверка наличия ордера в базе. Сначала проверяется кэш
        /// </summary>
        //public async Task<bool> Order_Exist(int order_id)
        //{
        //    if (!CheckInRanges(order_id, OrdersIdRange))
        //    {
        //        using PublicContext context = new PublicContext(options);
        //        return await context.Eveonline_MarketOrders.AnyAsync(x => x.order_id == order_id);
        //    }

        //    return true;
        //}

        /// <summary>
        /// Поиск диапозона по внетреннему зн-ю
        /// </summary>
        bool CheckInRanges(int id, IdRanges idRanges)
        {
            var inRange = idRanges?.FirstOrDefault(x => x.to >= id);
            if (inRange?.from <= id)
                return true;

            return false;
        }

        public async Task AlliancesIdsRanges_Update()
        {
            AlliancesIdsRanges = await getRange(API.CommonPrivate.GetAllianceIdRanges(remoteServiceBaseUrl));
        }

        public async Task CorporationsIdsRanges_Update()
        {
            CorporationsIdsRanges = await getRange(API.CommonPrivate.GetCorporationIdRanges(remoteServiceBaseUrl));
        }

        public async Task CharactersIdsRanges_Update()
        {
            CharactersIdsRanges = await getRange(API.CommonPrivate.GetCharacterIdRanges(remoteServiceBaseUrl));
        }

        public async Task ContractsIdsRanges_Update()
        {
            ContractsIdRange = await getRange(API.CommonPrivate.GetContractIdRanges(remoteServiceBaseUrl));
        }

        //public async Task OrdersIdsRanges_Update()
        //{
        //    OrdersIdRange = await getRange(API.CommonPrivate.GetOrderIdRanges(remoteServiceBaseUrl));
        //}
        
        async Task<IdRanges> getRange(string uri)
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(uri);
            IdRanges idRangeItems = JsonSerializer.Deserialize<IdRanges>(responseString);
            return idRangeItems;
        }
    }
}
