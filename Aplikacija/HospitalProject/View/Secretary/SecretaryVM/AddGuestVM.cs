using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private Patient _patient;
        private bool modalResult;

        private Window window;
        private int _id;
        private int _idMR;

        public AddGuestVM(Window window)
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll().ToList());
            modalResult = false;
            this.window = window;
        }


        public bool ModalResult
        {
            get
            {
                return modalResult;
            }
            set
            {
                modalResult = value;
                OnPropertyChanged(nameof(ModalResult));
            }
        }



        // private bool CanExecute() {
        /* if (!string.IsNullOrEmpty(p.FirstName) &&
                  !string.IsNullOrEmpty(p.LastName) &&
                  !string.IsNullOrEmpty(p.Jmbg))


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
            Patient P = _patientController.Create(new Patient(_id, _idMR, FirstName, LastName, Jmbg));

            Patients.Add(P);
           

            ModalResult = true;
            window.Close();
        }


    }
}
