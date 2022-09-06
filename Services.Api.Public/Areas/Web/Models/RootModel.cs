using eveDirect.Repo.PublicReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Api.Public.Areas.Web.Models
{
    public class RootModel
    {
        public Warface wf { get; set; }

        public class Warface
        {
            public List<KeyValue<int, int>> topCharacters { get; set; }
            public List<KeyValue<int, int>> topCorporations { get; set; }
            public List<KeyValue<int, int>> topAlliances { get; set; }
        }
    }
}
