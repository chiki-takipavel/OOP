using System;
using System.Windows.Controls;

namespace LR1_OOP
{
    public class RangeValidationRule : ValidationRule
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public override ValidationResult Validate(
          object value, System.Globalization.CultureInfo cultureInfo)
        {
            string text = String.Format("Должно быть между {0} и {1}",
                           MinValue, MaxValue);
            if (!Int32.TryParse(value.ToString(), out int intValue))
                return new ValidationResult(false, "Не целое число");
            if (intValue < MinValue)
                return new ValidationResult(false, "Слишком маленькое. " + text);
            if (intValue > MaxValue)
                return new ValidationResult(false, "Слишком большое. " + text);
            return ValidationResult.ValidResult;
        }
    }
}
