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

    public partial class DoctorView : Window
    {

        private static readonly Regex _timeRegex = new Regex("[0-9][0-9]:[0-9][0-9]");
        private const string TIME_FORMAT_ERROR_MESSAGE = "Invalid time format. Valid format is: 15:03. !";

        private IList<Patient> _patients;
        private DateTime _date;
        private int _duration;
        private String _time;
        private Doctor _doctor;

        public ObservableCollection<AppointmentViewModel> AppointmentItems { get; set; }
        public ObservableCollection<int> PatientIds { get; set; }

        AppointmentController _appointmentController;
        PatientController _patientController;
        DoctorController _doctorController;

        public DoctorView()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;

            AppointmentItems = new ObservableCollection<AppointmentViewModel>(AppointmentConverter.ConvertAppointmentListToAppointmentViewList(_appointmentController.GetAll().ToList()));
            _patients = _patientController.GetAll().ToList();
            _doctor = _doctorController.Get(3);                         // Ovde sam postavio privremeno na 3 da je id doktora, IZMENITI KAD BUDE LOGIN!!!!!!!

            PatientIds = new ObservableCollection<int>(FindPatientIdFromPatients());

        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
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
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
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
                if (value != _time)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        private IList<int> FindPatientIdFromPatients()
        {
            return _patients
                .Select(patient => patient.Id)
                .ToList();
        }

        private Patient FindPatientFromPatientId(int id)
        {
            return _patientController.Get(id);
        }


        private DateTime parseTime()
        {
            String[] hoursAndMinutes = _time.Split(':');
            int hours = int.Parse(hoursAndMinutes[0]);
            int minutes = int.Parse(hoursAndMinutes[1]);
            return new DateTime(_date.Year, _date.Month, _date.Day, hours, minutes, 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddEvent_Handler(object sender, RoutedEventArgs e)
        {

            if (!isTimeTextBoxFilled() || !isCheckBoxSelected())
            {
                ShowError("Please fill out all the fields");
            }
            else if (IsTimeCorrect(_time))
            {
                ShowError(TIME_FORMAT_ERROR_MESSAGE);
            }
            else if(!isDurationTextBoxFilled())
            {
                ShowError("Duration must be greater than zero");
            } 
            else
            {
                _date = parseTime();
                if (!canCreate())
                {
                    ShowError("Appointment is already scheduled. Please select another one");
                    return;
                }
                UpdateDataViewAdd(CreateAppointment());
            }

        }

        private void EditEvent_Handler(object sender, RoutedEventArgs e)
        {

            if (Appointments.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an appointment", "Warning", MessageBoxButton.OK);
            }
            else
            {
                EditAppointment();
            }
        }

        private void EditAppointment()
        {
            try
            {
                AppointmentViewModel updateAVM = (AppointmentViewModel)Appointments.SelectedItem;
                _appointmentController.Update(AppointmentConverter.ConvertAppointmentViewToAppointment(updateAVM));
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }

        private void CancelAppointment()
        {
            AppointmentViewModel avm = (AppointmentViewModel)Appointments.SelectedItem;
            _appointmentController.Delete(avm.AppointmentId);
            AppointmentItems.Remove(avm);
        }

        private void UpdateDataViewAdd(Appointment appointment)
        {
            AppointmentItems.Add(AppointmentConverter.ConvertAppointmentToAppointmentView(appointment));
        }

        private Appointment CreateAppointment()
        {
            try
            {
                return _appointmentController.Create(new Appointment(_date, _duration, _doctor, FindPatientFromPatientId(int.Parse(PatientID.SelectedItem.ToString()))));
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (Appointments.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an appointment", "Warning", MessageBoxButton.OK);
            }
            else
            {
                CancelAppointment();
            }
        }

        private bool IsTimeCorrect(string input) => !_timeRegex.IsMatch(input);

        private void ShowError(string s)
        {
            MessageBox.Show(s, "Warning", MessageBoxButton.OK);
        }

        private bool isTimeTextBoxFilled()
        {
            return _time != null && _date != null;
        }

        private bool isDurationTextBoxFilled()
        {
            return _duration > 0;
        }

        private bool isCheckBoxSelected() {
            return PatientID.SelectedIndex != -1;
        }

        private bool canCreate()
        {
            TimeSpan timeSpan = new TimeSpan(0, _duration, 0);;
            DateTime newAppEndDate = _date + timeSpan;
            DateTime existingAppointmentEndDate;
            foreach(AppointmentViewModel avm in AppointmentItems)
            {
                existingAppointmentEndDate = avm.Date + new TimeSpan(0, Duration, 0);
                if (_date.Year == avm.Date.Year && _date.Month == avm.Date.Month && _date.Day == avm.Date.Day)
                { 
                    if(_date <= avm.Date && newAppEndDate >= existingAppointmentEndDate)
                    {
                        return false;
                    } else if (_date >= avm.Date && newAppEndDate <= existingAppointmentEndDate)
                    {
                        return false;
                    } else if (_date < avm.Date && newAppEndDate < existingAppointmentEndDate && newAppEndDate > avm.Date)
                    {
                        return false;
                    } else if (_date > avm.Date && _date < existingAppointmentEndDate && newAppEndDate > existingAppointmentEndDate)
                    {
                        return false;
                    }

                    
                }

         }
            return true;
        }
    }
}
