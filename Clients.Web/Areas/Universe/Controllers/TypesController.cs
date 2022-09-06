using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Clients.Web.Areas.Universe.Controllers
{
    [Area("Universe")]
    public class TypesController : Controller
    {
        [Route(Routes.UniverseTypes)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
