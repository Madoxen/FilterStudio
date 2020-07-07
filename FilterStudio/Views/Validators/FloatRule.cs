
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
                if (((string)value).Length > 0 )
                {
                    if (((string)value)[((string)value).Length - 1] == '.')
                    {
                        return new ValidationResult(false, "Expression ends with dot");
                    }
                    val = double.Parse((String)value, CultureInfo.InvariantCulture);
                }
                else
                {
                    return new ValidationResult(false, "Length == 0!");
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

         
            return ValidationResult.ValidResult;
        }
    }
}