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

        private RelayCommand addCommand;
        private RelayCommand createAnamnesisCommand;
        private RelayCommand medicalRecordCommand;
        private RelayCommand deleteCommand;

        public ObservableCollection<Appointment> AppointmentItems { get; set; }
        public ObservableCollection<int> PatientIds { get; set; }

        AppointmentController _appointmentController;
        PatientController _patientController;
        DoctorController _doctorController;

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
        }

        private void InstantiateData()
        {
            AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAll().ToList());
            _patients = _patientController.GetAll().ToList();
            _doctor = _doctorController.Get(3);
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

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(param => AddCommandExecute(), param => CanAddCommandExecute()));
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

        private bool CanDeleteCommandExecute()
        {
            return SelectedItem != null;
        }

        private void DeleteCommandExecute()
        {
            _appointmentController.Delete(SelectedItem.Id);
            AppointmentItems.Remove(SelectedItem);
        }

        private bool CanMedicalRecordCommandExecute()
        {
            return SelectedItem != null;
        }

        private void MedicalRecordCommandExecute()
        {
            MedicalCardView view = new MedicalCardView();
            view.DataContext = new MedicalCardViewModel(SelectedItem.Patient);
            view.ShowDialog();
        }

        private bool CanCreateAnamnesisCommandExecute()
        {
            return SelectedItem != null && SelectedItem.Date.Subtract(DateTime.Now) < TimeSpan.Zero;
        }

        private void CreateAnamnesisCommandExecute()
        {
            AnamnesisView view = new AnamnesisView();
            view.DataContext = new AnamnesisViewModel(SelectedItem);
            view.ShowDialog();
        }

        private bool CanAddCommandExecute()
        {
            return true;
        }

        private void AddCommandExecute()
        {
            if(!CanCreate())
            {
                MessageBox.Show("Appointment is already taken");
                return;
            }

            Appointment appointment = new Appointment(parseTime(), _duration, _doctor, PatientData);
            _appointmentController.Create(appointment);
            AppointmentItems.Add(appointment);
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


        private DateTime parseTime()
        {
            String[] hoursAndMinutes = _time.Split(':');
            int hours = int.Parse(hoursAndMinutes[0]);
            int minutes = int.Parse(hoursAndMinutes[1]);
            return new DateTime(_date.Year, _date.Month, _date.Day, hours, minutes, 0);
        }

        

        private void EditEvent_Handler(object sender, RoutedEventArgs e)
        {

          /*  if (Appointments.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an appointment", "Warning", MessageBoxButton.OK);
            }
            else
            {*/
                EditAppointment();
            //}
        }

        private void EditAppointment()
        {
            try
            {
               // AppointmentViewModel updateappointment = (AppointmentViewModel)Appointments.SelectedItem;
               // _appointmentController.Update(AppointmentConverter.ConvertAppointmentViewToAppointment(updateappointment));
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }

       private bool CanCreate()
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
        }
    }
}
