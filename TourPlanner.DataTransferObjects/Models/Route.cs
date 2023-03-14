using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models
{
    [Table("routes")]
    public class Route
    {
        public int Id { get; set; }

        [Required]
        public string TravellingType { get; set; }
        [Required]
        public double EstimatedTimeInSeconds { get; set; }
        [Required]
        public double DistanceInKm { get; set; }
    }
}
