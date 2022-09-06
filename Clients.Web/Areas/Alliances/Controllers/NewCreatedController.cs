using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Clients.Web.Areas.Alliances.Controllers
{
    [Area("Alliances")]
    public class NewCreatedController : Controller
    {
        [Route(Routes.AlliancesNew)]
        public IActionResult newcreated()
        {
            return View();
        }
    }
}
