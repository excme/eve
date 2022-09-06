using eveDirect.Api.Public.Areas.Web.Models;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using eveDirect.Repo.PublicReadOnly.Models;

namespace eveDirect.Api.Public.Areas.Web.Controllers
{
    [Route(ApiRoutes.Web.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("web")]
    [ApiExplorerSettings(GroupName = "Web")]
    public class IndexController : ControllerBase
    {
        [HttpGet(ApiRoutes.Web.RootValues)]
        public IActionResult ValuesV1() {
            RootModel sampleData = new RootModel()
            {
                wf = new RootModel.Warface() {
                    topCharacters = new List<KeyValue<int, int>>() {
                        new KeyValue<int, int>(94206202, 1000),
                        new KeyValue<int, int>(1366440976, 2000),
                        new KeyValue<int, int>(92499643, 2200),
                        new KeyValue<int, int>(1288710652, 2500),
                        new KeyValue<int, int>(1292964949, 3000),
                        new KeyValue<int, int>(91578428, 4800),
                    },
                    topCorporations = new List<KeyValue<int, int>>() {
                        new KeyValue<int, int>(98370861, 13000),
                        new KeyValue<int, int>(98388312, 22000),
                        new KeyValue<int, int>(98558506, 28000),
                        new KeyValue<int, int>(98409330, 65500),
                        new KeyValue<int, int>(98343297, 73000),
                        new KeyValue<int, int>(306830202, 84800),
                    },
                    topAlliances = new List<KeyValue<int, int>>() {
                        new KeyValue<int, int>(1354830081, 130000),
                        new KeyValue<int, int>(99005338, 220000),
                        new KeyValue<int, int>(1900696668, 280000),
                        new KeyValue<int, int>(498125261, 655000),
                        new KeyValue<int, int>(99003581, 730000),
                        new KeyValue<int, int>(99004804, 848000),
                    },
                }
            };

            return Ok(sampleData);
        }
    }
}
