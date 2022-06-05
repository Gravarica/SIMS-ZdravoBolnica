using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    internal class AddPatientVM : BaseViewModel
    {
        
        
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
        private string _dateofbirth;
        private string _gender;
        private bool _guest;
        private Gender selectedGender;
        
        
        private UserRepository _userRepository;
        private PatientController _patientController;
        private Patient _patient;
        private RelayCommand saveCommand;
        public ObservableCollection<Patient> Patients { get; set; }
        List<ComboBoxData<Gender>> genders = new List<ComboBoxData<Gender>>();

        public AddPatientVM(Window window)
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll().ToList());
            FillComboData();
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
        
        public Gender SelectedGender
        {
            get
            {
                return selectedGender;
            }
            set
            {
                selectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        public List<ComboBoxData<Gender>> Genders
        {
            get
            {
                return genders;
            }
            set
            {
                genders = value;
                OnPropertyChanged(nameof(Genders));
            }
        }

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
                return saveCommand ?? (saveCommand = new RelayCommand(param => ExecuteSaveCommand(), param => CanExecute()));
            }
        }

        private bool CanExecute()
        {
            return CheckIfUsernameIsTaken(UserName);
        }

        private bool CheckIfUsernameIsTaken(String username)
        {
            foreach (Patient patient in Patients)
            {
                if (username == patient.Username)
                {
                    MessageBox.Show("username already taken", "Error", MessageBoxButton.OK);
                    return false;
                }
                
            }
            return true;
        }

        private void ExecuteSaveCommand()
        {

            Patients.Add(_patientController.Create(new Patient(_id, _medicalrecordid, false, UserName,Password, FirstName, LastName, Jmbg, PhoneNumber, Email, Adress, Convert.ToDateTime(Date), SelectedGender)));
    
         }

        private void FillComboData()
        {
            foreach(Gender g in Enum.GetValues(typeof(Gender)))
            {
                genders.Add(new ComboBoxData<Gender> { Name = g.ToString(), Value = g });
            }
        }

    }
}
