using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using HospitalProject.Core;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class AddGuestVM : BaseViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }
        private RelayCommand saveCommand;

        PatientController _patientController;

        private int _jmbg;
       
        private string _firstname;
      
        private string _lastname;

        //public string Guest = true;

        private Patient _patient;

        private int _id;


        public AddGuestVM()
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll().ToList());

        }






        // private bool CanExecute() {
        /* if (!string.IsNullOrEmpty(p.FirstName) &&
                  !string.IsNullOrEmpty(p.Username) &&
                  !string.IsNullOrEmpty(p.LastName) &&
                  !string.IsNullOrEmpty(p.Email) &&
                  !string.IsNullOrEmpty(p.Adress) &&
                  !string.IsNullOrEmpty(p.Password))


          { return true; }*/

        //   return ;
        // }


        
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
                return saveCommand ?? (saveCommand = new RelayCommand(param => ExecuteSaveCommand()));
            }
        }

        private void ExecuteSaveCommand()
        {

            Patients.Add(_patientController.Create(new Patient(_id, FirstName, LastName, Jmbg)));

        }


    }
}
