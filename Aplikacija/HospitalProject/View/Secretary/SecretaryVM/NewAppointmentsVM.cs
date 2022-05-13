using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.Secretary.SecretaryV;
using HospitalProject.View.Util;
using Model;
using Repository;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class NewAppointmentsVM : BaseViewModel
    {

        private Appointment selectedItem;

        private IList<Doctor> _doctors;

        private IList<Patient> _patients;
        private DateTime _date;
        private int _duration;
        private String _time;
        private Doctor _doctor;
        private Patient _patient;

        private RelayCommand addCommand;
        private RelayCommand deleteCommand;
        private RelayCommand newAppointmentCommand;
        private RelayCommand editAppointmentCommand;

        private Window window;
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<int> DoctorIds { get; set; }

        AppointmentController _appointmentController;
        PatientController _patientController;
        DoctorController _doctorController;
        UserController _userController;
        NotificationController _notificationController;


        private List<ComboBoxData<Doctor>> doctorComboBox = new List<ComboBoxData<Doctor>>();


        private List<ComboBoxData<Patient>> patientComboBox = new List<ComboBoxData<Patient>>();
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

        public Doctor DoctorData
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                OnPropertyChanged(nameof(DoctorData));
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

        public List<ComboBoxData<Doctor>> DoctorComboBox
        {

            get
            {
                return doctorComboBox;
            }
            set
            {
                doctorComboBox = value;
                OnPropertyChanged(nameof(DoctorComboBox));
            }
        }



        public NewAppointmentsVM()
        {

            InstantiateControllers();
            InstantiateData();
            FillComboData1();
            FillComboData2();

        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
        }

        private void InstantiateData()
        {
            _patients = _patientController.GetAll().ToList();
            Appointments = new ObservableCollection<Appointment>(_appointmentController.GetAllUnfinishedAppointments().ToList());
            _doctors = _doctorController.GetAll().ToList();

        }

        private void FillComboData1()
        {

            foreach (Doctor d in _doctors)
            {
                doctorComboBox.Add(new ComboBoxData<Doctor> { Name = d.FirstName + " " + d.LastName, Value = d });
            }

        }
        private void FillComboData2()
        {

            foreach (Patient d in _patients)
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = d.FirstName + " " + d.LastName, Value = d });
            }

        }
        private bool CanExecute()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => DeleteCommandExecute(), param => CanDeleteCommandExecute()));
            }
        }

        private bool CanDeleteCommandExecute()
        {
            return SelectedItem != null;
        }

        private void DeleteCommandExecute()
        {
            if (MessageBox.Show("Are you sure you want to cancel an appointment?", "Cancel an appointment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _appointmentController.Delete(SelectedItem.Id);
                Appointments.Remove(SelectedItem);
            }

        }
        public RelayCommand NewAppointmentCommand
        {
            get
            {
                return newAppointmentCommand ?? (newAppointmentCommand = new RelayCommand(param => NewAppointmentCommandExecute(), param => CanNewAppointmentCommandExecute()));
            }
        }

        private bool CanNewAppointmentCommandExecute()
        {
            return true;
        }

        private void NewAppointmentCommandExecute()
        {
            AddNewAppointmentV view = new AddNewAppointmentV();
            view.DataContext = new AddNewAppointmentVM(Appointments, view);
            view.ShowDialog();
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
            EditAppV view = new EditAppV();
            view.DataContext = new EditAppVM(SelectedItem, Appointments, view);
            view.ShowDialog();
        }

    }
}