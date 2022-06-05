using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.Service;
using HospitalProject.View.Secretary.SecretaryV;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class PatientProfileVM : BaseViewModel
    {
        private Patient _patient;
        private MedicalRecord _medicalRecord;
        private Allergies _selectedAllergy;
        private MedicalRecordController _medicalRecordController;
        
        private RelayCommand _editProfileCommand;
        public PatientProfileVM(MedicalRecord medicalRecord)
        {
            _medicalRecord = medicalRecord;
        }

        public PatientProfileVM(Patient patient)
        {
            
            var app = System.Windows.Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;
            _patient = patient;
            _medicalRecord = _medicalRecordController.GetMedicalRecordByPatient(_patient);
        }

        public Patient Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }
        public Allergies SelectedAllergy
        {
            get
            {
                return _selectedAllergy;
            }
            set
            {
                _selectedAllergy = value;
                OnPropertyChanged(nameof(SelectedAllergy));
            }
        }


        public MedicalRecord MedicalRecord
        {
            get
            {
                return _medicalRecord;
            }
            set
            {
                _medicalRecord = value;
                OnPropertyChanged(nameof(MedicalRecord));
            }
        }
        
        public RelayCommand EditProfile
        {
            get
            {
                return _editProfileCommand ?? (_editProfileCommand = new RelayCommand(param => EditProfileCommandExecute()));
            }
        }

        private void EditProfileCommandExecute()
        {
            EditPatientProfileV view = new EditPatientProfileV();
            view.DataContext = new EditProfileVM(Patient);
            SecretaryMainViewVM.Instance.CurrentView = view;
        }
    }
}


