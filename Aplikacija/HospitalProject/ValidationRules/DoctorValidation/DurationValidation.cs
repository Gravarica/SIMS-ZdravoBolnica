using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HospitalProject.ValidationRules.DoctorValidation
{
    public class DurationValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int number))
            {
                if (number > 0) {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Duration must be greater than zero");
                }
                
            } 
            else
            {
                return new ValidationResult(false, "Duration must be a number");
            }
        }
    }
}
