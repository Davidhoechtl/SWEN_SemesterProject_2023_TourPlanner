using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Infrastructure.Validation
{
    internal class PropertyValidationContext
    {
        public string PropertyName { get; set; }
        public Func<string> ValidationAction { get; set; }

        public PropertyValidationContext(string propertyName, Func<string> validationAction)
        {
            if (propertyName == null) throw new ArgumentNullException();
            if (validationAction == null) throw new ArgumentNullException();

            PropertyName = propertyName;
            ValidationAction = validationAction;
        }
    }
}
