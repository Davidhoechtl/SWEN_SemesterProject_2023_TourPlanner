using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MapQuestApi.Domain
{
    public class MapQuestLocation
    {
        public string street { get; set; }
        public string postalCode { get; set; }

        // Neighborhood
        public string adminArea6 { get; set; }
        public string adminArea6Type { get; set; }

        // City
        public string adminArea5 { get; set; }
        public string adminArea5Type { get; set; }
        
        // County
        public string adminArea4 { get; set; }
        public string adminArea4Type { get; set; }
        
        // State
        public string adminArea3 { get; set; }
        public string adminArea3Type { get; set; }
        
        // Country
        public string adminArea1 { get; set; }
        public string adminArea1Type { get; set; }

        public string geocodeQualityCode { get; set; }
        public string geocodeQuality { get; set; }
        public bool dragPoint { get; set; }
        public string sideOfStreet { get; set; }
        public string linkId { get; set; }
        public string unknownInput { get; set; }
        public string type { get; set; }
    }
}
