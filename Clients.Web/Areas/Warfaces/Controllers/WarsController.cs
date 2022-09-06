using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Clients.Web.Areas.Warfaces.Controllers
{
    [Area("Warfaces")]
    public class WarsController : Controller
    {
        [Route(Routes.WarfaceWars)]
        public IActionResult wars()
        {
            return View();
        }
    }
}
