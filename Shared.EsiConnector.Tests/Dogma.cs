using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Dogma: BaseConnector
    {
        /// <summary>
        /// GET /dogma/attributes/
        /// </summary>
        [Fact]
        public void DogmaAttributesResult()
        {
            ExecuteAndOutput(connector.Dogma.Attributes());
        }
        /// <summary>
        /// GET /dogma/attributes/{attribute_id}/
        /// </summary>
        [Fact]
        public void DogmaAttributeInfoResult()
        {
            var arrtId = 2738;
            ExecuteAndOutput(connector.Dogma.Attribute(arrtId));
        }
        /// <summary>
        /// GET /dogma/effects/
        /// </summary>
        [Fact]
        public void DogmaEffectsResult()
        {
            ExecuteAndOutput(connector.Dogma.Effects());
        }
        /// <summary>
        /// GET /dogma/effects/{effect_id}/
        /// </summary>
        [Fact]
        public void DogmaEffectInfoResult()
        {
            var effectId = 7002;
            ExecuteAndOutput(connector.Dogma.Effect(effectId));
        }
        /// <summary>
        /// /dogma/dynamic/items/{type_id}/{item_id}/
        /// </summary>
        [Fact]
        public void DogmaDynamicItem()
        {
            int type_id = 34;
            long item_id = 1000;
            ExecuteAndOutput(connector.Dogma.DynamicItem(type_id, item_id));
        }
    }
}
