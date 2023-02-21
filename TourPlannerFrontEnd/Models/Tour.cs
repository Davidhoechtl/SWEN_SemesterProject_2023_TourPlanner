using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Models
{
    internal class Tour
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
    }
}
