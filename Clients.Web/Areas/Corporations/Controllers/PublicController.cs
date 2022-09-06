using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Clients.Web.Services;

namespace eveDirect.Clients.Web.Areas.Corporations.Controllers
{
    [Area("Corporations")]
    public class PublicController : Controller
    {
        ICheckExistService CheckExistService { get; set; }
        public PublicController(ICheckExistService checkExistService)
        {
            CheckExistService = checkExistService ?? throw new ArgumentNullException(nameof(checkExistService));
        }

        [Route(Routes.CorporationMigrations)]
        public IActionResult migrations()
        {
            return View();
        }

        [Route("corp{corporation_id}")]
        public async Task<IActionResult> corporation(int corporation_id)
        {
            if (!await CheckExistService.Corporation_Exist(corporation_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("corp{corporation_id}/allianceHistory")]
        public async Task<IActionResult> allianceHistory(int corporation_id)
        {
            if (!await CheckExistService.Corporation_Exist(corporation_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("corp{corporation_id}/membersMigrations")]
        public async Task<IActionResult> membersMigrations(int corporation_id)
        {
            if (!await CheckExistService.Corporation_Exist(corporation_id))
                return NotFound("Not found.");

            return View();
        }
    }
}