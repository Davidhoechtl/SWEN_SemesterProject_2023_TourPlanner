using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Infrastructure.Validation
{
    public class ValidatorCollection
    {
        public void Add(string propertyName, Func<string> validationAction)
        {
            if (!validationContexts.Any(vc => vc.PropertyName == propertyName))
            {
                validationContexts.Add(new PropertyValidationContext(propertyName, validationAction));
            }
            else
            {
                throw new InvalidOperationException($"There is already a validation context for the property {propertyName}");
            }
        }

        public string Validate(string propertyName)
        {
            PropertyValidationContext foundValidationContext = validationContexts
                .FirstOrDefault(vc => vc.PropertyName == propertyName);
            if(foundValidationContext != null)
            {
                return foundValidationContext.ValidationAction.Invoke();
            }

            return null;
        }

        public string HasError()
        {
            foreach(PropertyValidationContext propertyValidation in validationContexts)
            {
                string validationError = propertyValidation.ValidationAction.Invoke();
                if (!string.IsNullOrEmpty(validationError))
                {
                    return validationError;
                }
            }

            return null;
        }

        private List<PropertyValidationContext> validationContexts = new();
    }
}
