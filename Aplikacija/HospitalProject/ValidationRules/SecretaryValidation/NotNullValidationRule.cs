using System;
using System.Text.Json.Serialization.Metadata;
using System.Windows.Controls;

namespace HospitalProject.ValidationRules.SecretaryValidation
{
    public class NotNullValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (String.IsNullOrWhiteSpace(text))
                    return new ValidationResult(false, "this field is necessary");

                return new ValidationResult(true, null);

            }
            catch
            {
                return new ValidationResult(false, "unknown error occured");
            }

        }


    }
}