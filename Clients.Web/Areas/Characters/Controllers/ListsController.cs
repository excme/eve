using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Clients.Web.Areas.Characters.Controllers
{
    [Area("Characters")]
    public class ListsController : Controller
    {
        [Route(Routes.CharactersRoot)]
        public IActionResult characters()
        {
            return View();
        }

        [Route(Routes.CharacterMigrations)]
        public IActionResult migrations()
        {
            return View();
        }

        [Route(Routes.CharacterNewBorns)]
        public IActionResult newborns()
        {
            return View();
        }
    }
}
