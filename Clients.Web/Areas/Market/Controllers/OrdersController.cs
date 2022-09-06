using Microsoft.AspNetCore.Mvc;
using eveDirect.Clients.Web.Services;
namespace eveDirect.Clients.Web.Areas.Market.Controllers
{
    [Area("Market")]
    public class OrdersController : Controller
    {
        //ICheckExistService CheckExistService { get; set; }
        public OrdersController(ICheckExistService checkExistService)
        {
            //CheckExistService = checkExistService ?? throw new ArgumentNullException(nameof(checkExistService));
        }
        [HttpGet(Routes.MarketOrders)]
        public IActionResult orders()
        {
            return View();
        }
        [HttpGet("market/orders/{type_id}")]
        public IActionResult Type(int type_id)
        {
            return View();
        }
    }
}