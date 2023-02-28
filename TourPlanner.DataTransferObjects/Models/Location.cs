using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string State { get; set; }
    }
}
