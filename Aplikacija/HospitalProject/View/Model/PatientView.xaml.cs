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

    public partial class PatientView : Window
    {

        private Patient _patient;
        private DateTime _date;
        private int _duration;
        private String _time;
        private IList<Doctor> _doctors;

        public ObservableCollection<AppointmentViewModel> AppointmentItems { get; set; }
        public ObservableCollection<int> DoctorIds { get; set; }

        AppointmentController _appointmentController;
        PatientController _patientController;
        DoctorController _doctorController;

        public PatientView()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            _appointmentController = app.AppointmentControllerPatient;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;

            //AppointmentItems = new ObservableCollection<AppointmentViewModel>(AppointmentConverter.ConvertAppointmentListToAppointmentViewList(_appointmentController.GetAll().ToList()));
            _patient = _patientController.Get(1);
            _doctors = _doctorController.GetAll().ToList();                         // Ovde sam postavio privremeno na 3 da je id doktora, IZMENITI KAD BUDE LOGIN!!!!!!!

            DoctorIds = new ObservableCollection<int>(FindDoctorIdFromDoctors());

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

        private IList<int> FindDoctorIdFromDoctors()
        {
            return _doctors
                .Select(doctor => doctor.Id)
                .ToList();
        }

        private Doctor FindDoctorFromDoctorId(int id)
        {
            return _doctorController.Get(id);
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

        /*private void AddEvent_Handler(object sender, RoutedEventArgs e)
        {

            _date = parseTime();
            UpdateDataViewAdd(CreateAppointment());
        }*/

        /*private void EditEvent_Handler(object sender, RoutedEventArgs e)
        {

            _date = parseTime();
            UpdateDataViewEdit(EditAppointment());
        }*/

        /*private Appointment EditAppointment()
        {
            try
            {
                return _appointmentController.Update(new Appointment(_date, _duration, _doctor, );
            }
            catch(InvalidDateException)
            {
                throw;
            }
        }*/

        private void CancelAppointment()
        {
            AppointmentViewModel avm = (AppointmentViewModel)Appointments.SelectedItem;
            _appointmentController.Delete(avm.AppointmentId);
            AppointmentItems.Remove(avm);
        }

        private void UpdateDataViewEdit(Appointment appointment)
        {

            AppointmentViewModel editViewAppointment;

            foreach (AppointmentViewModel avm in AppointmentItems)
            {
                if (appointment.Id == avm.AppointmentId)
                {
                    editViewAppointment = avm;
                }
            }

            //editViewAppointment = AppointmentConverter.ConvertAppointmentToAppointmentView(appointment);
        }

        private void UpdateDataViewAdd(Appointment appointment)
        {
            //AppointmentItems.Add(AppointmentConverter.ConvertAppointmentToAppointmentView(appointment));
        }

        /*private Appointment CreateAppointment()
        {
            try
            {
                //return _appointmentController.Create(new Appointment(_date, _duration, FindDoctorFromDoctorId(int.Parse(DoctorID.SelectedItem.ToString())), _patient));
                //FindDoctorIdFromDoctors(int.Parse(DoctorIds.SelectedItem.ToString())))            
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }*/

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

    }
}
