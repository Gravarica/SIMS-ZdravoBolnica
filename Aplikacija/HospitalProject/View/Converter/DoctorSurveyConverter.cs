using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HospitalProject.View.Converter
{
    public class DoctorSurveyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Doctor doctor = (Doctor)value;

            if (doctor == null) return null;

            return "Doctor: " + doctor.FirstName + " " + doctor.LastName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
