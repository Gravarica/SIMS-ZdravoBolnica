using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HospitalProject.View.Converter
{
    public class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Prescription prescription = (Prescription)value;

            if(prescription == null)
            {
                return null;
            }

            int days;

            if(prescription.StartDate.Month == prescription.EndDate.Month)
            {
                days = prescription.EndDate.Day - prescription.StartDate.Day;
            } 
            else if(prescription.StartDate.Month == 4 || prescription.StartDate.Month == 6 || prescription.StartDate.Month == 9 || prescription.StartDate.Month == 11)
            {
                days = prescription.EndDate.Day - prescription.StartDate.Day + 30;
            } 
            else if(prescription.StartDate.Month == 2)
            {
                days = prescription.EndDate.Day - prescription.StartDate.Day + 28;
            }
            else
            {
                days = prescription.EndDate.Day - prescription.StartDate.Day + 31;
            }

            return days.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
