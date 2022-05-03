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
    public class AllergyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Allergies> allergies = (List<Allergies>)value;
            List<string> allergiesStringList = new List<string>();

            if(allergies == null)
            {
                return null;
            }

            foreach(Allergies allergies1 in allergies)
            {
                allergiesStringList.Add(allergies1.Name);
            }

            return allergiesStringList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
