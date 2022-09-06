using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Clients.Web.Services;

namespace eveDirect.Clients.Web.Areas.Alliances.Controllers
{
    [Area("Alliances")]
    public class PublicController : Controller
    {
        ICheckExistService CheckExistService { get; set; }
        public PublicController(ICheckExistService checkExistService)
        {
            CheckExistService = checkExistService ?? throw new ArgumentNullException(nameof(checkExistService));
        }

        [Route("alliance/{alliance_id}")]
        public async Task<IActionResult> alliance(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("alliance/{alliance_id}/characters")]
        public async Task<IActionResult> characters(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("alliance/{alliance_id}/characters-analytics")]
        public async Task<IActionResult> characters_analytics(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("alliance/{alliance_id}/corporations")]
        public async Task<IActionResult> corporations(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("alliance/{alliance_id}/corporations-analytics")]
        public async Task<IActionResult> corporations_analytics(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("alliance/{alliance_id}/kills")]
        public async Task<IActionResult> kills(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("alliance/{alliance_id}/wars")]
        public async Task<IActionResult> wars(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("alliance/{alliance_id}/contracts")]
        public async Task<IActionResult> contracts(int alliance_id)
        {
            if (!await CheckExistService.Alliance_Exist(alliance_id))
                return NotFound("Not found.");

            return View();
        }
    }
}