using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.EsiConnector.Models;
using Xunit;

namespace eveDirect.Databases.Tests.Compare
{
    public class PublicContextTest : BaseCompare
    {
        [Fact(DisplayName = "EveOnlineCharacter.UpdateProperties()")]
        public void EveOnlineCharacter()
        {
            var _old = new EveOnlineCharacter() { character_id = 23001 };
            var infoResult = new CharacterInfoResult() { };
            MakeCompare(_old, infoResult, true, false, "Сравнение по игнорируемому аттрибуту и == properties");

            GenerateValues(ref _old, ref infoResult);
            MakeCompare(_old, infoResult, userMsg1: "Сравнение всех != properties");
        }

        [Fact(DisplayName = "EveOnlineCorporation.UpdateProperties()")]
        public void EveOnlineCorporation()
        {
            var _old = new EveOnlineCorporation() { corporation_id = 23001 };
            var infoResult = new CorporationInfoResult() { };
            MakeCompare(_old, infoResult, true, false, "Сравнение по игнорируемому аттрибуту и == properties");

            GenerateValues(ref _old, ref infoResult);
            MakeCompare(_old, infoResult, userMsg1: "Сравнение всех != properties");
        }
    }
}
