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
    internal class DoctorAppointmentsVM : BaseViewModel
    {
        private Appointment selectedItem;

        private IList<Patient> _patients;
        private IList<Doctor> _doctors;
        private DateTime _date;
        private int _duration;
        private String _time;
        private Doctor _doctor;

        private Patient _patient;
        private RelayCommand medicalRecordCommand;
        private RelayCommand deleteCommand;
        private RelayCommand newAppointmentCommand;
        private RelayCommand editAppointmentCommand;

        public ObservableCollection<Appointment> AppointmentItems { get; set; }
        //public ObservableCollection<int> PatientIds { get; set; }

        AppointmentController _appointmentController;
        AppointmentRepository _appointmentRepository;
        PatientController _patientController;
        DoctorController _doctorController;

        private List<ComboBoxData<Patient>> patientComboBox = new List<ComboBoxData<Patient>>();
        private List<ComboBoxData<Doctor>> doctorComboBox = new List<ComboBoxData<Doctor>>();

        public List<ComboBoxData<Patient>> PatientComboBox
        {

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

        public DoctorAppointmentsVM()
        {

            InstantiateControllers();
            InstantiateData();
            FillComboData();
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


            ObservableCollection<Appointment> AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAll().ToList()) ;

            _patients = (IList<Patient>)_patientController.GetAll();  //ToList()
            _doctors = _doctorController.GetAll().ToList();

        }

        private void FillComboData()
        {

            foreach (Patient p in _patients)
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = p.FirstName + " " + p.LastName, Value = p });
            }

        }
        private void FillComboData2()
        {

            foreach (Doctor p in _doctors)
            {
                doctorComboBox.Add(new ComboBoxData<Doctor> { Name = p.FirstName + " " + p.LastName, Value = p });
            }

        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => DeleteCommandExecute(), param => CanDeleteCommandExecute()));
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
            EditAppview view = new EditAppview();
            view.DataContext = new EditAppVM(SelectedItem, AppointmentItems);
            view.ShowDialog();
        }

        private bool CanNewAppointmentCommandExecute()
        {
            return true;
        }

        private void NewAppointmentCommandExecute()
        {
            NewAppointment view = new NewAppointment();
            view.DataContext = new NewAppointmentVM(AppointmentItems);
            view.ShowDialog();
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
                AppointmentItems.Remove(SelectedItem);
            }

        }

        private bool CanMedicalRecordCommandExecute()
        {
            return SelectedItem != null;
        }

        private void MedicalRecordCommandExecute()
        {
            PatientProfile view = new PatientProfile();
            view.DataContext = new PatientProfileVM(SelectedItem.Patient);
            view.ShowDialog();
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


        private DateTime parseTime()
        {
            String[] hoursAndMinutes = _time.Split(':');
            int hours = int.Parse(hoursAndMinutes[0]);
            int minutes = int.Parse(hoursAndMinutes[1]);
            return new DateTime(_date.Year, _date.Month, _date.Day, hours, minutes, 0);
        }

       /* private bool CanCreate()
        {
            TimeSpan timeSpan = new TimeSpan(0, _duration, 0); ;
            DateTime newAppEndDate = _date + timeSpan;
            DateTime existingAppointmentEndDate;
            foreach (Appointment appointment in AppointmentItems)
            {
                existingAppointmentEndDate = appointment.Date + new TimeSpan(0, Duration, 0);
                if (_date.Year == appointment.Date.Year && _date.Month == appointment.Date.Month && _date.Day == appointment.Date.Day)
                {
                    if (_date <= appointment.Date && newAppEndDate >= existingAppointmentEndDate)
                    {
                        return false;
                    }
                    else if (_date >= appointment.Date && newAppEndDate <= existingAppointmentEndDate)
                    {
                        return false;
                    }
                    else if (_date < appointment.Date && newAppEndDate < existingAppointmentEndDate && newAppEndDate > appointment.Date)
                    {
                        return false;
                    }
                    else if (_date > appointment.Date && _date < existingAppointmentEndDate && newAppEndDate > existingAppointmentEndDate)
                    {
                        return false;
                    }


                }

            }

            return true;
        }*/
    }
}
