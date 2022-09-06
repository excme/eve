using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Clients.Web.Services;

namespace eveDirect.Clients.Web.Areas.Market.Controllers
{
    [Area("Market")]
    public class ContractsController : Controller
    {
        ICheckExistService CheckExistService { get; set; }
        public ContractsController(ICheckExistService checkExistService)
        {
            CheckExistService = checkExistService
                ?? throw new ArgumentNullException(nameof(checkExistService));
        }
        [HttpGet(Routes.MarketContracts)]
        public IActionResult contracts()
        {
            return View("contracts");
        }
        [HttpGet("market/contracts/{type_id}")]
        public IActionResult Contracts_byType(int type_id)
        {
            return View("contracts");
        }
        [HttpGet("market/contract/{contract_id}")]
        public async Task<IActionResult> Details(int contract_id)
        {
            if (!await CheckExistService.Contract_Exist(contract_id))
                return NotFound();

            return View("contract");
        }
    }
}