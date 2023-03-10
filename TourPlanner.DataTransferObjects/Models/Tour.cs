using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataTransferObjects.Models
{
    [Table("tours")]
    public class Tour
    {
        public int Id { get; set; }
        public int LocationStartId { get; set; }
        public int LocationDestinationId { get; set; }
        public int RouteId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TravellingType { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Today;


        [ForeignKey(nameof(LocationStartId))]
        public Location Start { get; set; }

        [ForeignKey(nameof(LocationDestinationId))]
        public Location Destination { get; set; }

        [ForeignKey(nameof(RouteId))]
        public Route Route { get; set; }
    }
}
