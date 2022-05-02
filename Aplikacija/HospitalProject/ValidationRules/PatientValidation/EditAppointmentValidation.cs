using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.ValidationRules.PatientValidation
{
    public class EditAppointmentValidation
    {

        public static bool LessThank24HoursCheck(DateTime date) {

            TimeSpan ts = date - DateTime.Now;

            if (ts.TotalHours > 24)
            {

                return true;
            }
            else
                return false;
                        
            
        }

        public static bool MoreThanFourDaysCheck(DateTime startDate, DateTime endDate, DateTime appointmentDate) {

            TimeSpan ts1 = startDate - appointmentDate;
            TimeSpan ts2 = endDate - appointmentDate;

            if (Math.Abs(ts1.TotalDays) < 4 && Math.Abs(ts2.TotalDays) < 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            

    }
}
