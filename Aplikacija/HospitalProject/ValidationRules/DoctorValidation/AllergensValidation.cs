using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.ValidationRules.DoctorValidation
{
    public class AllergensValidation
    {
        public static Allergies CheckIfPatientIsAllergicToMedicine(List<Allergies> allergies, Equipement medicine)
        {
            foreach (Allergies allergen in allergies)
            {
                foreach(Allergies medicineAllergen in medicine.Alergens)
                {
                    if(allergen.Name.Trim().Equals(medicineAllergen.Name.Trim()))
                    {
                        return allergen;
                    }
                }
            }

            return null;
        }
    }
}
