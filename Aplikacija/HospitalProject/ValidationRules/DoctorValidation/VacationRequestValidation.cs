using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.ValidationRules.DoctorValidation
{
    public class VacationRequestValidation
    {
        public static bool CanCreateNewVacationRequest(List<VacationRequest> vacationRequests)
        {
            if (vacationRequests.Count() >= 2)
            {
                return false;
            }

            return true;
        }

    }
}
