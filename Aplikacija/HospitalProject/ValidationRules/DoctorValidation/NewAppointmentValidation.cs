using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.ValidationRules.DoctorValidation
{
    public class NewAppointmentValidation
    {


        public static bool IsStartBeforeEnd(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue) {
                return false;
            }    
            return startDate <= endDate;
        }

        public static bool IsStartBeforeEnd(DateOnly startDate, DateOnly endDate)
        {
            return endDate <= startDate;
        }

        public static bool IsDateAfterNow(DateTime startDate, DateTime endDate)
        {
            return startDate >= DateTime.Now && endDate >= DateTime.Now;
        }

        public static bool IsComboBoxChecked(Patient SelectedValue)
        {
            return SelectedValue != null;
        }

        public static bool IsComboBoxCheckedDoctor(Doctor SelectedValue)
        {
            return SelectedValue != null;
        }
    }
}
