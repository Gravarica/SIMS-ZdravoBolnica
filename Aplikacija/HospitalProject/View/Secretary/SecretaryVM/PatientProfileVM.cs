using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class PatientProfileVM : BaseViewModel
    {
        /* MedicalRecord _medicalRecord;

        private int id;
        
        private PatientController _patientController;

       private DateTime _dateofbirth;
        private int _jmbg;
        private string _firstname;
        private string _username;
        private string _lastname;
        private string _email;
        private string _adress;
        private string _password;
        private int _phonenumber;
        private BloodType _bloodtype;
        private bool _guest;
        private Patient _patient;
        private string _gender;
       
        public RelayCommand editPatientCommand { get; set; }
        public PatientProfileVM(Patient Patien) {


            InstantiateControllers();
            InstantiateData(Patien);
  }


        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;

           
            editPatientCommand = new RelayCommand(param => ExecuteEditPatientComand());
            

        }

        private void ExecuteEditPatientComand()
        {


          _patientController.Update(new Patient(32, false, _username, _firstname, _lastname, _jmbg, _phonenumber, _email, _adress, _dateofbirth, StringToGender(_gender)));
        }


        private void InstantiateData(Patient Patien)
        {


            Patient Patient = _patientController.Get(Patien.Id);

            FirstName = Patient.FirstName;
            LastName = Patient.LastName;
            //DateOfBirth = Patient.DateOfBirth;
            Jmbg = Patient.Jmbg;

            UserName = Patient.Username;

            Email = Patient.Email;
            Adress = Patient.Adress;
            Password = Patient.Password;
            PhoneNumber = Patient.PhoneNumber;
            BloodType = Patient.BloodType;
            Guest = Patient.Guest;

            //Gender = Patient.Gender;


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

        public BloodType BloodType
        {
            get { return _bloodtype; }
            set { _bloodtype = value; OnPropertyChanged(nameof(BloodType)); }
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

        public Gender StringToGender(string str)
        {
            switch (str)
            {
                case "male":
                    return global::Model.Gender.male;

                default:
                    return global::Model.Gender.female;
            }
        }*/

        MedicalRecord _medicalRecord;
        private Patient patient;

        public PatientProfileVM(MedicalRecord medicalRecord)
        {
            _medicalRecord = medicalRecord;
        }

        public PatientProfileVM(Patient patient)
        {
            this.patient = patient;
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
    }
}


