
namespace TourPlannerFrontEnd.Infrastructure
{
    using System.ComponentModel;

    internal abstract class ValidatingViewModel<T> : ViewModel<T>, IDataErrorInfo
        where T : class
    {
        public string this[string columnName] => Validate(columnName);

        public string Error => null;

        public abstract string Validate(string columnName);
    }
}
