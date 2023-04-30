using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerFrontEnd.Infrastructure.Validation;

namespace TourPlannerTests
{
    internal class ValidationLogicTests
    {
        [Test]
        public void ValidateForAnyError_Error()
        {
            validatorCollection = new ValidatorCollection();
            string test = string.Empty;

            validatorCollection.Add("Property1", () =>
            {
                if (string.IsNullOrEmpty(test))
                {
                    return "error";
                }

                return null;
            });

            bool noErrors = string.IsNullOrEmpty(validatorCollection.HasError());
            Assert.That(!noErrors);
        }

        [Test]
        public void ValidateForAnyError_NoError()
        {
            validatorCollection = new ValidatorCollection();
            string test = "test";

            validatorCollection.Add("Property1", () =>
            {
                if (string.IsNullOrEmpty(test))
                {
                    return "error";
                }

                return null;
            });

            bool noErrors = string.IsNullOrEmpty(validatorCollection.HasError());
            Assert.That(noErrors);
        }


        [Test]
        public void ValidationForSpecificProperty_Error()
        {
            validatorCollection = new ValidatorCollection();
            string test = string.Empty;

            validatorCollection.Add("Property1", () =>
            {
                if (string.IsNullOrEmpty(test))
                {
                    return "error";
                }

                return null;
            });

            bool noErrors = string.IsNullOrEmpty(validatorCollection.HasError());
            Assert.That(!noErrors);
        }

        [Test]
        public void ValidationForSpecificProperty_NoError()
        {
            validatorCollection = new ValidatorCollection();
            string test = "test";

            validatorCollection.Add("Property1", () =>
            {
                if (string.IsNullOrEmpty(test))
                {
                    return "error";
                }

                return null;
            });

            bool noErrors = string.IsNullOrEmpty(validatorCollection.HasError());
            Assert.That(noErrors);
        }

        private ValidatorCollection validatorCollection;
    }
}
