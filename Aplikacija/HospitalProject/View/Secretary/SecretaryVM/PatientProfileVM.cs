using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Controller;
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
        private MedicalRecordService _medicalRecordService;
        private RelayCommand showAllergiesCommand;
        private RelayCommand edit;
        MedicalRecord _medicalRecord;
        
        public PatientProfileVM(MedicalRecord medicalRecord)
        {
            _medicalRecord = medicalRecord;
        }

        public PatientProfileVM(Patient patient, MedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
            _patient = patient;
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
        public RelayCommand ShowAllergiesCommand
        {
            get
            {
                return showAllergiesCommand ?? (showAllergiesCommand = new RelayCommand(param => ExecuteShowAllergiesCommand()));
            }
        }

        public void ExecuteShowAllergiesCommand()
        {
            PatientAllergies view = new PatientAllergies();
            view.DataContext = new PatientAllergiesVM(Patient, _medicalRecordService);
            view.ShowDialog();
        }

        public RelayCommand Edit
        {
            get
            {
                return edit ?? (edit = new RelayCommand(param => ExecuteEditCommand()));
            }
        }

        public void ExecuteEditCommand()
        {
            EditProfile view = new EditProfile();
            view.DataContext = new EditProfileVM(Patient);
            view.ShowDialog();
        }


    }
}


