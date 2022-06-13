using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Controller;
using HospitalProject.Core;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class AddGuestVM : BaseViewModel
    {
        private int _jmbg;
        private string _firstname;
        private string _lastname;
        private int _id;
        private int _medicalRecordId;
        
        private PatientController _patientController;
        private RelayCommand _saveCommand;
        public ObservableCollection<Patient> Patients { get; set; }
       
        
        public AddGuestVM(ObservableCollection<Patient> patients)
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
            Patients = patients;
        }


        public int Jmbg
        {
            get
            {
                return _jmbg;
            }
            set
            {
                _jmbg = value;
                OnPropertyChanged(nameof(Jmbg));
            }
        }

        public String FirstName
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public String LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(LastName));
            }
        }


        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => ExecuteSaveCommand(), param =>CanExecute()));
            }
        }

        private void ExecuteSaveCommand()
        { 
            if ( Jmbg==0)
            {
                MessageBox.Show("Jmbg must be integer number!", "warning", MessageBoxButton.OK);
            }
            else
            {
                Patient patient = _patientController.Create(new Patient(_id, _medicalRecordId, FirstName, LastName, Jmbg));
                Patients.Add(patient);
                MessageBox.Show("Guest profile created!", "note", MessageBoxButton.OK);
                FirstName = null;
                LastName = null;
                Jmbg = 0;
            }
        }

        private bool CanExecute()
        {
            return (Jmbg!=null && FirstName!= null && LastName!= null ) ;
        }

    }
}
