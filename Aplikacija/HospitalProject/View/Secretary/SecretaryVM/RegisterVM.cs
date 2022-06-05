using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using HospitalProject.Core;
using HospitalProject.Repository;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class RegisterVM:BaseViewModel
    {
        
        private int _intValue = 0;
        private int _jmbg;
        private int _id;
        private int _medicalrecordid;
        private int _phonenumber;
        private string _firstname;
        private string _username;
        private string _lastname;
        private string _email;
        private string _adress;
        private string _password;
        private DateTime _dateofbirth;
        private string _gender;
        private bool _guest;
        
        private Patient _patient;
        private Gender selectedGender;
        
        
        private UserRepository _userRepository;
        private RelayCommand _saveCommand;
        
        private PatientController _patientController;
        public RegisterVM()
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
          
        }

        public bool Female
        {
            get
            {
                return (_intValue == 1) ? true : false;;
            }
            set
            {
                _intValue = 1 ;
                OnPropertyChanged(nameof(Male));
            }
        }

        public bool Male
        {
            get
            {
                return (_intValue == 2) ? true : false;
            }
            set
            {
                _intValue = 2 ;
                OnPropertyChanged(nameof(Female));
            }
        }

        public DateTime Date
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

        private bool CanExecute()
        {

          return  (!string.IsNullOrEmpty(FirstName) &&
                  !string.IsNullOrEmpty(UserName) &&
                  !string.IsNullOrEmpty(LastName) &&
                  !string.IsNullOrEmpty(Email) &&
                  !string.IsNullOrEmpty(Adress) &&
                  !string.IsNullOrEmpty(Password));

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

   


        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => ExecuteSaveCommand(), param => CanExecute()));
            }
        }

        


        private void ExecuteSaveCommand()
        {

               
            if (_intValue == 1)
            {
                _patientController.Create(new Patient(_id,
                    _medicalrecordid,
                    false,
                    UserName, 
                    Password,
                    FirstName,
                    LastName,
                    Jmbg,
                    PhoneNumber, 
                    Email, 
                    Adress, 
                    Date,
                    Gender.female));}
            else if (_intValue == 2)
            {
                _patientController.Create(new Patient(_id,
                    _medicalrecordid,
                    false, 
                    UserName,
                    Password,
                    FirstName,
                    LastName,
                    Jmbg,
                    PhoneNumber, 
                    Email, 
                    Adress, 
                    Date,
                    Gender.male)); }
         

        }
        

    }
}

