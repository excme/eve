using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Clients.Web.Areas.Corporations.Controllers
{
    [Area("Corporations")]
    public class NewCreatedController : Controller
    {
        [Route(Routes.CorporationNew)]
        public IActionResult newcreated()
        {
            return View();
        }
    }
}
