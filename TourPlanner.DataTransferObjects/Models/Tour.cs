
namespace TourPlanner.DataTransferObjects.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public int Popularity { get; set; }
        public int ChildFriendliness { get; set; }
        
        [ForeignKey(nameof(LocationStartId))]
        public Location Start { get; set; }

        [ForeignKey(nameof(LocationDestinationId))]
        public Location Destination { get; set; }

        [ForeignKey(nameof(RouteId))]
        public Route Route { get; set; }

        public List<TourLog> TourLogs { get; set; }
    }
}
