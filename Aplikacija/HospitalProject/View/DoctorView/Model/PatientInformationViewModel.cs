using HospitalProject.Core;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class PatientInformationViewModel : BaseViewModel
    {

        MedicalRecord _medicalRecord;

        public PatientInformationViewModel(MedicalRecord medicalRecord)
        {
            _medicalRecord=medicalRecord;
        }

        public MedicalRecord MedicalRecord
        {
            get 
            { 
                return _medicalRecord; 
            }
            set
            {
                _medicalRecord=value;
                OnPropertyChanged(nameof(MedicalRecord));
            }
        }
    }
}
