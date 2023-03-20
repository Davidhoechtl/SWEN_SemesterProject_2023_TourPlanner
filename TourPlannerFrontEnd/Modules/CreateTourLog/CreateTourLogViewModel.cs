﻿
namespace TourPlannerFrontEnd.Modules.RateTour
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using TourPlanner.DataTransferObjects.Models;
    using TourPlannerBackEnd.Repositories;
    using TourPlannerFrontEnd.Infrastructure;

    internal class CreateTourLogViewModel : ViewModel<TourLog>
    {
        public DateTime Date
        {
            get => this.Model.Date;
            set
            {
                this.Model.Date = value;
                NotifyOfPropertyChange(nameof(Date));
            }
        }

        public string Comment
        {
            get => this.Model.Comment;
            set
            {
                this.Model.Comment = value;
                NotifyOfPropertyChange(nameof(Comment));
            }
        }

        public double TakenTimeInMinutes
        {
            get => this.Model.TakenTimeInSeconds;
            set
            {
                this.Model.TakenTimeInSeconds = value;
                NotifyOfPropertyChange(nameof(TakenTimeInMinutes));
            }
        }

        public int Difficulty
        {
            get => this.Model.Difficulty;
            set
            {
                this.Model.Difficulty = value;
                NotifyOfPropertyChange(nameof(Difficulty));
            }
        }

        public int Rating
        {
            get => this.Model.Rating;
            set
            {
                this.Model.Rating = value;
                NotifyOfPropertyChange(nameof(Rating));
            }
        }

        public CreateTourLogViewModel(TourLogRepository tourLogRepository)
        {
            this.tourLogRepository = tourLogRepository;
        }

        public async Task Save()
        {
            await Task.Run(() =>
            {
                tourLogRepository.SaveTourLog(this.Model);
            });

            MessageBox.Show("Saved successful");
        }

        private readonly TourLogRepository tourLogRepository;
    }
}
