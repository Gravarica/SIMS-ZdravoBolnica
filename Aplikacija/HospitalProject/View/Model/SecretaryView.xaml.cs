using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller;
using HospitalProject;
using HospitalProject.Controller;
using HospitalProject.Exception;
using HospitalProject.View.Converter;
using Model;

namespace HospitalProject.View.Model
{
    /// <summary>
    /// Interaction logic for SecretaryView.xaml
    /// </summary>
    public partial class SecretaryView : Window
    {
        private IList<Patient> _patients;
        private int _id; //
        private int _medicalRecordId;
        private string _username; //
        private string _firstname; //
        private string _lastname; //
        private string _adress; //
        private int _jmbg; //
        private int _phonenumber; //
        private BloodType _bloodtype; //
        private bool _guest;//
        private UserType _usertype;//
        private string _email; //
        private DateTime _dateofbirth;
        private Gender _gender; //
        
        
        public ObservableCollection<PatientViewModel> PatientItems { get; set; }
        PatientController _patientController;
        
        public SecretaryView()
        {
            InitializeComponent();
            PatientController _patientController = (Application.Current as App).PatientController;
            PatientItems = new ObservableCollection<PatientViewModel>(PatientConverter.ConvertPatientListToPatientViewList(_patientController.GetAll().ToList()));

        }
        
        
        //int _medicalRecordId
        public int Medicalrecordid
        {
            get
            {
                return _medicalRecordId;
            }
            set
            {
                if(_medicalRecordId != value)
                {
                    _medicalRecordId = value;
                    OnPropertyChanged(nameof(Medicalrecordid));
                }
            }
        }
        
        public int Id 
        {
            get
            {
                return _id;
            }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        public Gender Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (value != _gender)
                {
                    _gender = value;
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }
        //DateTime _dateofbirth
        public DateTime Dateofbirth
        {
            get
            {
                return _dateofbirth;
            }
            set
            {
                if (value != _dateofbirth)
                {
                    _dateofbirth = value;
                    OnPropertyChanged(nameof(Dateofbirth));
                }
            }
        }
        
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public bool Guest
        {
            get
            {
                return _guest;
            }
            set
            {
                if (value != _guest)
                {
                    _guest = value;
                    OnPropertyChanged(nameof(Guest));
                }
            }
        }
        
        public UserType Usertype
        {
            get
            {
                return _usertype;
            }
            set
            {
                if (value != _usertype)
                {
                    _usertype = value;
                    OnPropertyChanged(nameof(Usertype));
                }
            }
        }
        
        public BloodType Bloodtype
        {
            get
            {
                return _bloodtype;
            }
            set
            {
                if (value != _bloodtype)
                {
                    _bloodtype = value;
                    OnPropertyChanged(nameof(Bloodtype));
                }
            }
        }
        
        
       


        public String Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }


        public String Firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                if (value != _firstname)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Firstname));
                }
            }
        }



        public String Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                if (value != _lastname)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Lastname));
                }
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
                if (value != _adress)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Adress));
                }
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
                if (value != _jmbg)
                {
                    _jmbg = value;
                    OnPropertyChanged(nameof(Jmbg));
                }
            }
        }


        public int Phonenumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                if (value != _phonenumber)
                {
                    _phonenumber = value;
                    OnPropertyChanged(nameof(Phonenumber));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }





        private void AddEvent_Handler(object sender, RoutedEventArgs e)
        {

            UpdateDataViewAdd(AddPatient());
        }



        private Patient AddPatient()
        {
            try
            {
                return _patientController.Create(new Patient(
                    _id,
                    _medicalRecordId, 
                    _guest,
                    _username,
                    _firstname,
                    _lastname,
                    _jmbg,
                    _phonenumber,
                    _email,
                    _adress,
                    _dateofbirth,
                    _gender));
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }

        public void UpdateDataViewAdd(Patient patient)
        {
            PatientItems.Add(PatientConverter.ConvertPatientToPatientView(patient));
        }

       
        
        
        
        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if(Patients.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient", "Warning", MessageBoxButton.OK);
            } 
            else
            {
                DeletePatient();
            }
        }
        private void DeletePatient()
        {
            PatientViewModel rvm = (PatientViewModel)Patients.SelectedItem;
            _patientController.Delete(rvm.Id);
            PatientItems.Remove(rvm);
        }
        
        private void EditEvent_Handler(object sender, RoutedEventArgs e)
        {
            if (Patients.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient", "Warning", MessageBoxButton.OK);
            }
            else
            {
           //     EditPatient();
            }
        }
     /*   private void EditPatient()
        {
            try
            {
                
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }*/
    }
}
