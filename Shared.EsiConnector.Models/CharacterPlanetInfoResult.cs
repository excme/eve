using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/planets/{planet_id}/
    /// </summary>
    public class CharactersPlanetColonyResult:ISsoResult
    {
        public List<Link> links { get; set; }
        public List<Pin> pins { get; set; }
        public List<Route> routes { get; set; }
        public class Link
        {
            public long source_pin_id { get; set; }
            public long destination_pin_id { get; set; }
            public int link_level { get; set; }
        }

        public class Content
        {
            public int type_id { get; set; }
            public long amount { get; set; }
        }

        public class Head
        {
            public int head_id { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class ExtractorDetails
        {
            public List<Head> heads { get; set; }
            public int product_type_id { get; set; }
            public int cycle_time { get; set; }
            public float head_radius { get; set; }
            public int qty_per_cycle { get; set; }
        }

        public class Pin
        {
            public long pin_id { get; set; }
            public int type_id { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int schematic_id { get; set; }
            public List<Content> contents { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime last_cycle_start { get; set; }
            public ExtractorDetails extractor_details { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime? install_time { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime? expiry_time { get; set; }
            public int factory_details { get; set; }
        }

        public class Route
        {
            public long route_id { get; set; }
            public long source_pin_id { get; set; }
            public long destination_pin_id { get; set; }
            public int content_type_id { get; set; }
            public float quantity { get; set; }
            public List<long> waypoints { get; set; }
        }
    }
}
