
using System;
using System.Globalization;
using System.Windows.Controls;

namespace FilterStudio.Views.Validators
{
    public class IntRule : ValidationRule
    {

        public IntRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int val = 0;

            try
            {
                if (((string)value).Length > 0)
                    val = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

         
            return ValidationResult.ValidResult;
        }
    }
}