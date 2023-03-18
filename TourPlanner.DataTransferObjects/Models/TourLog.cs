
namespace TourPlanner.DataTransferObjects.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    [Table("tourLogs")]
    public class TourLog
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public int Difficulty { get; set; }

        [Required]
        public double TakenTimeInSeconds { get; set; }

        [Required]
        public int Rating { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
