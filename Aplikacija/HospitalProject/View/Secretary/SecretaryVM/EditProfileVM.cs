using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using HospitalProject.Core;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class EditProfileVM : BaseViewModel
    {
        private Patient _patient;
        
        private RelayCommand _saveCommand;
        
        private PatientController patientController;
        
        public ObservableCollection<Patient> Patients { get; set; }
        public EditProfileVM(Patient patient)
        {
            _patient = patient;
            var app = System.Windows.Application.Current as App;
            patientController = app.PatientController;
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
 
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => ExecuteSaveCommand(), param => CanExecute()));
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        private void ExecuteSaveCommand()
        {   
            patientController.Update(Patient);
        }

   
    }
}
