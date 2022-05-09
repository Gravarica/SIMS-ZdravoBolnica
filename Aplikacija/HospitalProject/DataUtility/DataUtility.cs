using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.DataUtility
{
    public class DataUtility
    {
        public static Specialization ConvertTokenToSpecialization(string token)
        {
            if (token.Equals("CARDIOLOGY"))
            {
                return Specialization.CARDIOLOGY;
            }
            else if (token.Equals("GENERAL"))
            {
                return Specialization.GENERAL;
            }
            else if (token.Equals("NEUROLOGY"))
            {
                return Specialization.NEUROLOGY;
            }

            return Specialization.SURGERY;
        }

        public static RequestState ConvertTokenToRequestState(string token)
        {
            if (token.Equals("APPROVED"))
            {
                return RequestState.APPROVED;
            }
            else if (token.Equals("DENIED"))
            {
                return RequestState.DENIED;
            }

            return RequestState.PENDING;
        }
    }
}
