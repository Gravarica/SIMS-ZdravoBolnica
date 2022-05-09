using System;
using System.Globalization;
using System.Windows.Data;
using HospitalProject.Model;

namespace HospitalProject.View.Converter
{
    public class EquipmentRelocationHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Equipement equipement = value as Equipement;
            if (equipement == null)
            {
                return null;
            }

            return "Relocation of " + equipement.Name.ToLower()+"s";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

