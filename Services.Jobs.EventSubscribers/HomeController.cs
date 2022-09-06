using eveDirect.Shared.EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Jobs.EventSubscribers
{
    public class HomeController : Controller
    {
        IEventBus _eventBus;
        public HomeController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IActionResult Index()
        {
            //_eventBus.Publish(new CharacterAddNewIntegrationEvent(111));
            //return Content("!");
            var strings = _eventBus.Statistic_Read();
            return Json(strings);
        }
    }
}
