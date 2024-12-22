using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Validations
{
    public class CompareTimeSpanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public CompareTimeSpanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (TimeSpan?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                return new ValidationResult($"Propriedade {_comparisonProperty} não encontrada.");

            var comparisonValue = (TimeSpan?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
