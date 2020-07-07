
using System;
using System.Globalization;
using System.Windows.Controls;

namespace FilterStudio.Views.Validators
{
    public class FloatRule : ValidationRule
    {

        public FloatRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double val = 0;

            try
            {
                if (((string)value).Length > 0)
                    val = double.Parse((String)value,CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

         
            return ValidationResult.ValidResult;
        }
    }
}