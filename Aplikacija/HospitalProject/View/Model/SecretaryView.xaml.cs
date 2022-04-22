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

        private int _id;
        private string _username;
        private string _firstname;
        private string _lastname;
        private string _mail;
        private string _adress;
        private string _jmbg;
        private string _phonenumber;

        public ObservableCollection<PatientViewModel> PatientItems { get; set; }
        PatientController _patientController;
        
        public SecretaryView()
        {
            InitializeComponent();
            PatientController _patientController = (Application.Current as App).PatientController;
            PatientItems = new ObservableCollection<PatientViewModel>(PatientConverter.ConvertPatientListToPatientViewList(_patientController.GetAll().ToList()));

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


        public String FirstName
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
                    OnPropertyChanged(nameof(FirstName));
                }
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
                if (value != _lastname)
                {
                    _username = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public String Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (value != _mail)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Mail));
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
        public String Jmbg
        {
            get
            {
                return _jmbg;
            }
            set
            {
                if (value != _jmbg)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Jmbg));
                }
            }
        }


        public String PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                if (value != _phonenumber)
                {
                    _username = value;
                    OnPropertyChanged(nameof(PhoneNumber));
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
                return _patientController.Add(new Patient(_username, _firstname, _lastname, _mail, _adress, _jmbg, 0635558291));
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


    }
}
