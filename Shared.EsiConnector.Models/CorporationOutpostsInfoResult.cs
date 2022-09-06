using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/outposts/{outpost_id}/
    /// </summary>
    public class CorporationOutpostsInfoResult
    {
        public int owner_id { get; set; }
        public int system_id { get; set; }
        public int docking_cost_per_ship_volume { get; set; }
        public int office_rental_cost { get; set; }
        public int type_id { get; set; }
        public double reprocessing_efficiency { get; set; }
        public double reprocessing_station_take { get; set; }
        public int standing_owner_id { get; set; }
        public Coordinates coordinates { get; set; }
        public List<Service> services { get; set; }
        
        public class Coordinates
        {
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
        }

        public class Service
        {
            public string service_name { get; set; }
            public int owner_id { get; set; }
            public int minimum_standing { get; set; }
            public int surcharge_per_bad_standing { get; set; }
            public int discount_per_good_standing { get; set; }
        }
    }
}
