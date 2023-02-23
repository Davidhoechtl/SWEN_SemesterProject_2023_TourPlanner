﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerBackEnd.Models
{
    public class Tour
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;

        public Route route { get; set; }
    }
}
