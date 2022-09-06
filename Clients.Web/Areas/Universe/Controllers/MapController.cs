using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Clients.Web.Areas.Universe.Controllers
{
    [Area("Universe")]
    public class MapController : Controller
    {
        [Route(Routes.UniverseMap)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
