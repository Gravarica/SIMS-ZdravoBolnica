using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HospitalProject.ValidationRules.DoctorValidation
{
    public class TimeFormatValidation : ValidationRule
    {
        private static readonly Regex _timeRegex = new Regex("[0-9][0-9]:[0-9][0-9]");
        private const string TIME_FORMAT_ERROR_MESSAGE = "Invalid time format. Valid format is: 15:03. !";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(IsTimeCorrect(value.ToString()))
            {
                return ValidationResult.ValidResult;
            } 
            else
            {
                return new ValidationResult(false, TIME_FORMAT_ERROR_MESSAGE);
            }
        }

        private bool IsTimeCorrect(string input) => _timeRegex.IsMatch(input);
    }
}
