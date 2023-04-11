using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataTransferObjects.Models;
using TourPlannerFrontEnd.Infrastructure;
using TourPlannerFrontEnd.Infrastructure.Validation;

namespace TourPlannerFrontEnd.Modules.TourLogs
{
    internal class TourLogValidationViewModel : ValidatingViewModel<TourLog>
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

        public override ValidatorCollection SetupValidation()
        {
            ValidatorCollection validators = new ValidatorCollection();

            validators.Add(nameof(Date), () =>
            {
                if (Date < DateTime.Today)
                {
                    return "Date must be in the future";
                }

                return string.Empty;
            });
            validators.Add(nameof(Comment), () =>
            {
                if (string.IsNullOrEmpty(this.Comment))
                {
                    return "Comment must not be null or empty";
                }

                return string.Empty;
            });
            validators.Add(nameof(TakenTimeInMinutes), () =>
            {
                if (TakenTimeInMinutes <= 0)
                {
                    return "Taken Time in Minutes must be greater than 0";
                }

                return string.Empty;
            });
            validators.Add(nameof(Difficulty), () =>
            {
                if(Difficulty < 1 || Difficulty > 10)
                {
                    return "Difficulty may only be between 1 and 10.";
                }

                return string.Empty;
            });
            validators.Add(nameof(Rating), () =>
            {
                if (Rating < 1 || Rating > 5)
                {
                    return "Difficulty may only be between 1 and 5.";
                }
                return string.Empty;
            });

            return validators;
        }
    }
}
