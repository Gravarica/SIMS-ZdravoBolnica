using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class DoctorProfileVM:BaseViewModel
    {
        private IList<Patient> _patients;
        private DateTime _date;
        private int _duration;
        private String _time;
        private Doctor _doctor;

        private Patient _patient;
        public ObservableCollection<Appointment> AppointmentItems { get; set; }
        public ObservableCollection<int> PatientIds { get; set; }

        AppointmentController _appointmentController;
        PatientController _patientController;
        DoctorController _doctorController;
        UserController _userController;

        
        public DoctorProfileVM(Doctor d)
        {
            _doctor = d;
            InstantiateControllers();
            InstantiateData();
           

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
            AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAllUnifinishedAppointmentsForDoctor(_doctor.Id).ToList());
            _patients = _patientController.GetAll().ToList();

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

        public Doctor Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                OnPropertyChanged(nameof(Doctor));
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

       
    }
}
