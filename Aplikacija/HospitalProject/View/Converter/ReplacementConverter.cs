using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HospitalProject.Model;

namespace HospitalProject.View.Converter
{
    public class ReplacementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Equipement> equipements = (List<Equipement>)value;
            List<String> equipementNames = new List<string>();

            foreach (Equipement equipement in equipements)
            {
                equipementNames.Add(equipement.Name);
            }

            return equipementNames;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
