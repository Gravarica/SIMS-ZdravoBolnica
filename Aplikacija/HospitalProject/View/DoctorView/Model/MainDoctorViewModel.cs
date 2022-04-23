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
    public class MainDoctorViewModel
    {
        private static readonly Regex _timeRegex = new Regex("[0-9][0-9]:[0-9][0-9]");
        private const string TIME_FORMAT_ERROR_MESSAGE = "Invalid time format. Valid format is: 15:03. !";

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
            
            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;

            AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAll().ToList());
            _patients = _patientController.GetAll().ToList();
            _doctor = _doctorController.Get(3);                         // Ovde sam postavio privremeno na 3 da je id doktora, IZMENITI KAD BUDE LOGIN!!!!!!!
            FillComboData();

            PatientIds = new ObservableCollection<int>(FindPatientIdFromPatients());

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

        private void FillComboData() 
        {
            
            foreach(Patient p in _patients)
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = p.FirstName + " " + p.LastName, Value = p});
            }

        }

       /* private void AddEvent_Handler(object sender, RoutedEventArgs e)
        {

            if (!isTimeTextBoxFilled())
            {
                ShowError("Please fill out all the fields");
            }
            else if (IsTimeCorrect(_time))
            {
                ShowError(TIME_FORMAT_ERROR_MESSAGE);
            }
            else if (!isDurationTextBoxFilled())
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

        }*/

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
               // AppointmentViewModel updateAVM = (AppointmentViewModel)Appointments.SelectedItem;
               // _appointmentController.Update(AppointmentConverter.ConvertAppointmentViewToAppointment(updateAVM));
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }

        private void CancelAppointment()
        {
            //AppointmentViewModel avm = (AppointmentViewModel)Appointments.SelectedItem;
           /// _appointmentController.Delete(avm.AppointmentId);
            //AppointmentItems.Remove(avm);
        }

       /* private void UpdateDataViewAdd(Appointment appointment)
        {
            AppointmentItems.Add(AppointmentConverter.ConvertAppointmentToAppointmentView(appointment));
        }*/

       /* private Appointment CreateAppointment()
        {
            try
            {
                return _appointmentController.Create(new Appointment(_date, _duration, _doctor, FindPatientFromPatientId(int.Parse(PatientID.SelectedItem.ToString()))));
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }*/

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
                CancelAppointment();
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

       /* private bool canCreate()
        {
            TimeSpan timeSpan = new TimeSpan(0, _duration, 0); ;
            DateTime newAppEndDate = _date + timeSpan;
            DateTime existingAppointmentEndDate;
            foreach (AppointmentViewModel avm in AppointmentItems)
            {
                existingAppointmentEndDate = avm.Date + new TimeSpan(0, Duration, 0);
                if (_date.Year == avm.Date.Year && _date.Month == avm.Date.Month && _date.Day == avm.Date.Day)
                {
                    if (_date <= avm.Date && newAppEndDate >= existingAppointmentEndDate)
                    {
                        return false;
                    }
                    else if (_date >= avm.Date && newAppEndDate <= existingAppointmentEndDate)
                    {
                        return false;
                    }
                    else if (_date < avm.Date && newAppEndDate < existingAppointmentEndDate && newAppEndDate > avm.Date)
                    {
                        return false;
                    }
                    else if (_date > avm.Date && _date < existingAppointmentEndDate && newAppEndDate > existingAppointmentEndDate)
                    {
                        return false;
                    }


                }

            }
            return true;
        }*/
    }
}
