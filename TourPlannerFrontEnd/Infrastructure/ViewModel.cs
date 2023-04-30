using Caliburn.Micro;

namespace TourPlannerFrontEnd.Infrastructure
{
    public class ViewModel<T> : PropertyChangedBase
        where T : class
    {
        public T Model 
        {
            get => model;
            set
            {
                model = value;
                OnModelChanged();
            } 
        }

        protected virtual void OnModelChanged()
        {
            // tells the view to update all controls
            Refresh();
        }

        private T model;
    }
}
