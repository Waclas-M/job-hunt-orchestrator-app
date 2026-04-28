using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace JHOP.Validators
{


    public class RequiredIfTrueAttribute : ValidationAttribute
    {
        private readonly string _boolProperty;

        public RequiredIfTrueAttribute(string boolProperty)
        {
            _boolProperty = boolProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_boolProperty);

            if (property == null)
                return new ValidationResult($"Unknown property: {_boolProperty}");

            var propertyValue = property.GetValue(validationContext.ObjectInstance);

            if (propertyValue is bool isManual && isManual)
            {
                if (value == null)
                    return new ValidationResult(ErrorMessage);

                if (value is IEnumerable list && !list.Cast<object>().Any())
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
