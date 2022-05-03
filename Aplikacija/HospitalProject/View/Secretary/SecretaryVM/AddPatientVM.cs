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
    internal class AddPatientVM : BaseViewModel
    {

        public ObservableCollection<Patient> Patients { get; set; }
        private RelayCommand saveCommand;
        
        PatientController _patientController;

        private string _dateofbirth;
        private int _jmbg;
        private int _id;
        private int _medicalrecordid;
        private string _firstname;
        private string _username;
        private string _lastname;
        private string _email;
        private string _adress;
        private string _password;
        private int _phonenumber;
        private bool _guest;
        private Patient _patient;
        private string _gender;
        

        public AddPatientVM()
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
        

        public string Date
        {
            get
            {
                return _dateofbirth;
            }
            set
            {
                _dateofbirth = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
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

        public String UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public String Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }

        public int PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }


        public bool Guest
        {
            get { return _guest; }
            set { _guest = value; }
        }

        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public BloodType StringToBloodType(string str)
        {
            switch (str)
            {
                case "a":
                    return global::Model.BloodType.a;
                case "b":
                    return global::Model.BloodType.b;
                case "ab":
                    return global::Model.BloodType.ab;

                default:
                    return global::Model.BloodType.o;
            }
        }

       


       /* public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => ExecuteSaveCommand()));
            }
        }
       */
        


    }
}
