using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace eveDirect.Translation.Web.Controllers
{
    [Route("root")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RootController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public object Get()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Where(
                ad => ad.AttributeRouteInfo != null).Select(ad => new 
                {
                    Name = ad.AttributeRouteInfo.Template,
                    Method = ad.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First(),
                }).ToList();

            var res = new 
            {
                Routes = routes
            };

            return res;
        }
    }
}