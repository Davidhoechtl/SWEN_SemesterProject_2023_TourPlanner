
namespace TourPlannerFrontEnd.Infrastructure
{
    using System.ComponentModel;
    using TourPlannerFrontEnd.Infrastructure.Validation;

    internal abstract class ValidatingViewModel<T> : ViewModel<T>, IDataErrorInfo
        where T : class
    {
        public ValidatorCollection Validators { get; set; }

        public string this[string columnName] => Validators.Validate(columnName);

        public string Error => null;

        public ValidatingViewModel()
        {
            Validators = SetupValidation();
        }

        public abstract ValidatorCollection SetupValidation();
    }
}
