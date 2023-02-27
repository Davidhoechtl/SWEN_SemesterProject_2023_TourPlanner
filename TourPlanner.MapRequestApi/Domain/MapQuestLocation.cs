using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MapQuestApi.Domain
{
    public class MapQuestLocation
    {
        public string Street { get; set; }
        public string PostalCode { get; set; }

        // Neighborhood
        public string AdminArea6 { get; set; }
        public string AdminArea6Type { get; set; }

        // City
        public string AdminArea5 { get; set; }
        public string AdminArea5Type { get; set; }
        
        // County
        public string AdminArea4 { get; set; }
        public string AdminArea4Type { get; set; }
        
        // State
        public string AdminArea3 { get; set; }
        public string AdminArea3Type { get; set; }
        
        // Country
        public string AdminArea1 { get; set; }
        public string AdminArea1Type { get; set; }

        public string GeocodeQualityCode { get; set; }
        public string GeocodeQuality { get; set; }
        public bool DragPoint { get; set; }
        public string SideOfStreet { get; set; }
        public int LinkId { get; set; }
        public string UnknownInput { get; set; }
        public string Type { get; set; }
    }
}
