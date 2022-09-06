using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Clients.Web.Services;

namespace eveDirect.Clients.Web.Areas.Characters.Controllers
{
    [Area("Characters")]
    public class ProfileController : Controller
    {
        ICheckExistService CheckExistService { get; set; }
        public ProfileController(ICheckExistService checkExistService)
        {
            CheckExistService = checkExistService
                ?? throw new ArgumentNullException(nameof(checkExistService));
        }

        [Route("character/{character_id}")]
        public async Task<IActionResult> character(int character_id)
        {
            if (!await CheckExistService.Character_Exist(character_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("character/{character_id}/corporation-history")]
        public async Task<IActionResult> corpHistory(int character_id)
        {
            if (!await CheckExistService.Character_Exist(character_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("character/{character_id}/alliance-history")]
        public async Task<IActionResult> allianceHistory(int character_id)
        {
            if (!await CheckExistService.Character_Exist(character_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("character/{character_id}/orders")]
        public async Task<IActionResult> marketOrders(int character_id)
        {
            if (!await CheckExistService.Character_Exist(character_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("character/{character_id}/contracts")]
        public async Task<IActionResult> marketContracts(int character_id)
        {
            if (!await CheckExistService.Character_Exist(character_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("character/{character_id}/kills")]
        public async Task<IActionResult> warfaceKills(int character_id)
        {
            if (!await CheckExistService.Character_Exist(character_id))
                return NotFound("Not found.");

            return View();
        }

        [Route("character/{character_id}/wars")]
        public async Task<IActionResult> warfaceWars(int character_id)
        {
            if (!await CheckExistService.Character_Exist(character_id))
                return NotFound("Not found.");

            return View();
        }
    }
}