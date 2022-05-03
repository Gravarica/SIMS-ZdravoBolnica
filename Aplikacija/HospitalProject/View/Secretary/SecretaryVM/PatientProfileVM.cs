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
using HospitalProject.View.Secretary.SecretaryV;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class PatientProfileVM : BaseViewModel
    {
       

        MedicalRecord _medicalRecord;
        private Patient _patient;
        private RelayCommand showAllergiesCommand;
        public PatientProfileVM(MedicalRecord medicalRecord)
        {
            _medicalRecord = medicalRecord;
        }

        public PatientProfileVM(Patient patient)
        {
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
            view.DataContext = new PatientAllergiesVM(Patient);
            view.ShowDialog();
        }


    }
}


