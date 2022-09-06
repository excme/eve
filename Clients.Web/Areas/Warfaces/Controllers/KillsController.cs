using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Clients.Web.Areas.Warfaces.Controllers
{
    [Area("Warfaces")]
    public class KillsController : Controller
    {
        [Route(Routes.WarfaceKills)]
        public IActionResult kills()
        {
            return View();
        }
    }
}
