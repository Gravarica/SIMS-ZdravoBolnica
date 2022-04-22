using HospitalProject.View.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.Converter
{
    public class PatientConverter : AbstractConverter
    {
        public static PatientViewModel ConvertPatientToPatientView(Patient patient)
            => new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.FirstName + " " + patient.LastName,
            };

        public static IList<PatientViewModel> ConvertPatientListToPatientViewList(IList<Patient> patients)
            => ConvertEntityListToViewList(patients, ConvertPatientToPatientView);
    }
}
