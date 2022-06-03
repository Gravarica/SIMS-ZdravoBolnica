using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Exception;
using HospitalProject.View.Converter;
using HospitalProject.View.DoctorView.Model;
using HospitalProject.View.DoctorView.Views;
using HospitalProject.View.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalProject.View.DoctorView.Model
{
    public class MainDoctorViewModel : BaseViewModel
    {
        private Appointment selectedItem;

        private IList<Patient> _patients;
        private DateTime _date;
        private int _duration;
        private String _time;
        private Doctor _doctor;

        private Patient _patient;
        private RelayCommand createAnamnesisCommand;
        private RelayCommand medicalRecordCommand;
        private RelayCommand deleteCommand;
        private RelayCommand newAppointmentCommand;
        private RelayCommand editAppointmentCommand;

        public ObservableCollection<Appointment> AppointmentItems { get; set; }
        public ObservableCollection<int> PatientIds { get; set; }

        AppointmentController _appointmentController;
        PatientController _patientController;
        DoctorController _doctorController;
        UserController _userController;

        private List<ComboBoxData<Patient>> patientComboBox = new List<ComboBoxData<Patient>>(); 

        public List<ComboBoxData<Patient>> PatientComboBox {

            get 
            {
                return patientComboBox;
            }
            set
            {
                patientComboBox = value;
                OnPropertyChanged(nameof(PatientComboBox));
            }
        }

        public MainDoctorViewModel()
        {

            InstantiateControllers();
            InstantiateData();
            FillComboData();

        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
            _userController = app.UserController;
        }

        private void InstantiateData()
        {
            _doctor = _doctorController.GetLoggedDoctor(_userController.GetLoggedUser().Username);
            AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAllUnifinishedAppointmentsForDoctor(_doctor.Id).ToList());
            _patients = _patientController.GetAll().ToList();
            
        }

        private void FillComboData()
        {

            foreach (Patient p in _patients)
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = p.FirstName + " " + p.LastName, Value = p });
            }

        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => DeleteCommandExecute(), param => CanDeleteCommandExecute()));
            }
        }

        public RelayCommand CreateAnamnesisCommand
        {
            get
            {
                return createAnamnesisCommand ?? (createAnamnesisCommand = new RelayCommand(param => CreateAnamnesisCommandExecute(),
                                                                                            param => CanCreateAnamnesisCommandExecute()));
            }
        }

        public RelayCommand MedicalRecordCommand
        {
            get
            {
                return medicalRecordCommand ?? (medicalRecordCommand = new RelayCommand(param => MedicalRecordCommandExecute(), param => CanMedicalRecordCommandExecute()));
            }
        }

        public RelayCommand NewAppointmentCommand
        {
            get
            {
                return newAppointmentCommand ?? (newAppointmentCommand = new RelayCommand(param => NewAppointmentCommandExecute(), param => CanNewAppointmentCommandExecute()));
            }
        }

        public RelayCommand EditAppointmentCommand
        {
            get
            {
                return editAppointmentCommand ?? (editAppointmentCommand = new RelayCommand(param => EditAppointmentCommandExecute(),
                                                                                            param => CanEditAppointmentCommandExecute()));
            }
        }

        private bool CanEditAppointmentCommandExecute()
        {
            return SelectedItem != null;
        }

        private void EditAppointmentCommandExecute()
        {
            EditAppointmentViewModel vm = new EditAppointmentViewModel(SelectedItem, AppointmentItems);
            MainViewModel.Instance.CurrentView = vm;
        }

        private bool CanNewAppointmentCommandExecute()
        {
            return true;
        }

        private void NewAppointmentCommandExecute()
        {
            NewAppointmentViewModel vm = new NewAppointmentViewModel(AppointmentItems);
            MainViewModel.Instance.CurrentView = vm;
        }

        private bool CanDeleteCommandExecute()
        {
            return SelectedItem != null;
        }

        private void DeleteCommandExecute()
        {
            if(MessageBox.Show("Are you sure you want to cancel an appointment?", "Cancel an appointment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _appointmentController.Delete(SelectedItem.Id);
                AppointmentItems.Remove(SelectedItem);
            }
            
        }

        private bool CanMedicalRecordCommandExecute()
        {
            return SelectedItem != null;
        }

        private void MedicalRecordCommandExecute()
        {
            MedicalCardViewModel vm = new MedicalCardViewModel(SelectedItem.Patient, ReturnFlag.APPOINTMENT_VIEW);
            MainViewModel.Instance.CurrentView = vm;
        }

        private bool CanCreateAnamnesisCommandExecute()
        {
            return SelectedItem != null && SelectedItem.Date.Subtract(DateTime.Now) < TimeSpan.Zero;
        }

        private void CreateAnamnesisCommandExecute()
        {
            AnamnesisViewModel avm = new AnamnesisViewModel(SelectedItem);
            MainViewModel.Instance.CurrentView = avm;
            if(avm.ModalResult)
            {
                AppointmentItems.Remove(avm.ShowItem);
            }
        }

        public Appointment SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public Patient PatientData
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(PatientData));
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
            }
        }

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
            }
        }

        public String Time
        {
            get
            {
                return _time;
            }
            set
            {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
            }
        }


       
    }
}
