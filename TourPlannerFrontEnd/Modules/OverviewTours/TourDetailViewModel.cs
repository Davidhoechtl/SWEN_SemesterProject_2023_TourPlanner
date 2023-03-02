
namespace TourPlannerFrontEnd.Modules.OverviewTours
{
    using System;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerFrontEnd.Infrastructure;

    internal class TourDetailViewModel : ViewModel<Tour>
    {
        public string TourName => this.Model?.Name;

        public DateTime StartDate => this.Model?.StartDate ?? DateTime.MinValue;

        public string TravellingType => this.Model?.TravellingType;

        public string StartStreet => this.Model?.Start?.Street;
        public string StartCity => this.Model?.Start?.City;
        public int? StartPostCode => this.Model?.Start?.PostCode;
        public string StartState => this.Model?.Start?.State;    
        
        public string EndStreet => this.Model?.Destination?.Street;
        public string EndCity => this.Model?.Destination?.City;
        public int? EndPostCode => this.Model?.Destination?.PostCode;
        public string EndState => this.Model?.Destination?.State;

    }
}
