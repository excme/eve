using eveDirect.Clients.Web.Models;
using eveDirect.Clients.Web.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace eveDirect.Clients.Web.Hubs
{
    public class LastActionsHub : Hub
    {
        readonly ILastActionsService _lastActionsService;
        public LastActionsHub(ILastActionsService lastActionsService)
        {
            _lastActionsService = lastActionsService;
        }

        public IEnumerable<LastActionModel> GetAllActions()
        {
            return _lastActionsService.All();
        }
    }
}
