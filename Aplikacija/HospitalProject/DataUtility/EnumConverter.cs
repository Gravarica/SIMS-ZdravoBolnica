using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.DataUtility
{
    public static class EnumConverter
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

        public static UserType ConvertStringToUserType(string userType)
        {
            if (userType.Equals("PATIENT"))
            {
                return UserType.PATIENT;
            }
            else if (userType.Equals("DOCTOR"))
            {
                return UserType.DOCTOR;
            }
            else if (userType.Equals("SECRETARY"))
            {
                return UserType.SECRETARY;
            }

            return UserType.WARDEN;
        }

        public static Category ConvertStringToCategory(string _category)
        {
            if (_category.Equals("DOCTOR_SURVEY"))
            {
                return Category.DOCTOR_SURVEY;
            }
            else if (_category.Equals("HOSPITAL_SURVEY"))
            {
                return Category.HOSPITAL_SURVEY;
            }
            
            return Category.APPLICATION_SURVEY;
            
        }
    }
}
