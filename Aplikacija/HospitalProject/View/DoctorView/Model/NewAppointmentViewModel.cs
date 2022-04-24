using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class NewAppointmentViewModel : BaseViewModel
    {

        private AppointmentController appointmentController;
        private DoctorController doctorController;
        private PatientController patientController;

        private DateTime startDate;
        private DateTime endDate;
        private Patient patient;
        private Doctor doctor;
        private ObservableCollection<Appointment> _generatedAppointments;

        private List<ComboBoxData<Patient>> patientComboBox = new List<ComboBoxData<Patient>>();
        public ObservableCollection<Appointment> GeneratedAppointments 
        { 
            get
            {
                return _generatedAppointments;
            }
            set
            {
                _generatedAppointments = value;
                OnPropertyChanged(nameof(GeneratedAppointments));
            }
        }

        private RelayCommand submitCommand;
        private RelayCommand cancelCommand;
        private RelayCommand acceptCommand;
        private RelayCommand returnCommand;

        public NewAppointmentViewModel()
        {
            InitializeControllers();
            InitializeData();
        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;

            appointmentController = app.AppointmentController;
            patientController = app.PatientController;
            doctorController = app.DoctorController;
        }

        private void InitializeData()
        {
            doctor = doctorController.Get(3);   // IZMENITI KAD BUDE LOGIN
            FillComboData();
        }

        // PROPERTY DEFINITIONS

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

        public DateTime StartDate 
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                //OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public Patient PatientData
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged(nameof(PatientData));
            }
        }

        // RELAY COMMAND DEFINITONS

        public RelayCommand SubmitCommand
        {
            
            get
            {
                return submitCommand ?? (submitCommand = new RelayCommand(param => SubmitCommandExecute(), param => CanSubmitCommandExecute()));
            }

        }
    

        private bool CanSubmitCommandExecute()
        {
            return true;
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);  
            GeneratedAppointments = new ObservableCollection<Appointment>(appointmentController.GenerateAvailableAppointments(startDateOnly, 
                                                                                                                              endDateOnly,
                                                                                                                              doctor,
                                                                                                                              PatientData));
        }

        // INTERNAL PRIVATE METHODS

        private void FillComboData()
        {

            foreach (Patient p in patientController.GetAll())
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = p.FirstName + " " + p.LastName, Value = p });
            }

        }
    }
}
