using eveDirect.Shared.EsiConnector.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class DogmaLogic : BaseLogic
    {

        public DogmaLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /dogma/attributes/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<DogmaAttributesResult> Attributes()
            =>  Execute<DogmaAttributesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/dogma/attributes/");

        /// <summary>
        /// /dogma/attributes/{attribute_id}/
        /// </summary>
        /// <param name="attribute_id"></param>
        /// <returns></returns>
        public EsiResponse<DogmaAttributeInfoResult> Attribute(int attribute_id)
            =>  Execute<DogmaAttributeInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/dogma/attributes/{attribute_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "attribute_id", attribute_id.ToString() }
                });

        /// <summary>
        /// /dogma/effects/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<DogmaEffectsResult> Effects()
            =>  Execute<DogmaEffectsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/dogma/effects/");

        /// <summary>
        /// /dogma/effects/{effect_id}/
        /// </summary>
        /// <param name="effect_id"></param>
        /// <returns></returns>
        public EsiResponse<DogmaEffectInfoResult> Effect(int effect_id)
            =>  Execute<DogmaEffectInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/dogma/effects/{effect_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "effect_id", effect_id.ToString() }
                });

        /// <summary>
        /// /dogma/dynamic/items/{type_id}/{item_id}/
        /// </summary>
        /// <param name="type_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public EsiResponse<DogmaDynamicItemResult> DynamicItem(int type_id, long item_id)
            =>  Execute<DogmaDynamicItemResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/dogma/dynamic/items/{type_id}/{item_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "type_id", type_id.ToString() },
                    { "item_id", item_id.ToString() }
                });
    }
}