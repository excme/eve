using eveDirect.Clients.Web.Models;
using System.Collections.Generic;

namespace eveDirect.Clients.Web.Services
{
    public interface ILastActionsService
    {
        void Add(LastActionModel item);
        IEnumerable<LastActionModel> All();
    }
}
