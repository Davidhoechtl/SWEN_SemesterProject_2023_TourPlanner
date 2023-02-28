using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;

        public Location Start { get; set; }
        public Location Destination { get; set; }
        public string TravellingType { get; set; }
    }
}
