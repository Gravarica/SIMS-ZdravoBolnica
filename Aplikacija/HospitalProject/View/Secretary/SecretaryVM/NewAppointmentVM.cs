using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.ValidationRules.DoctorValidation;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class NewAppointmentVM : BaseViewModel
    {

        private AppointmentController appointmentController;
        private DoctorController doctorController;
        private PatientController patientController;
        private RoomControoler roomControoler;
        private DateTime startDate;
        private DateTime endDate;
        private Patient patient;
        private Doctor doctor;
        private ObservableCollection<Appointment> _generatedAppointments;
        private Appointment selectedItem;
        private ObservableCollection<Appointment> _appointmentItems;

        private List<ComboBoxData<Patient>> patientComboBox = new List<ComboBoxData<Patient>>();

        private List<ComboBoxData<Doctor>> doctorComboBox = new List<ComboBoxData<Doctor>>();
        private List<ComboBoxData<ExaminationType>> examinationTypeComboBox = new List<ComboBoxData<ExaminationType>>();
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
        private RelayCommand resetCommand;
        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;

        public NewAppointmentVM(ObservableCollection<Appointment> AppointmentItems)
        {
            _appointmentItems = AppointmentItems;
            InitializeControllers();
            InitializeData();
        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;

            appointmentController = app.AppointmentController;
            patientController = app.PatientController;
            doctorController = app.DoctorController;
            roomControoler = app.RoomController;
        }

        private void InitializeData()
        {
            FillComboData();
            FillComboData2();
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

        public Doctor DoctorData
        {
            get
            {
                return doctor;
            }
    set
            {
                doctor = value;
                OnPropertyChanged(nameof(DoctorData));
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
            return NewAppointmentValidation.IsStartBeforeEnd(StartDate, EndDate) &&
                   NewAppointmentValidation.IsComboBoxChecked(PatientData);
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            GeneratedAppointments = new ObservableCollection<Appointment>(appointmentController.GenerateAvailableAppointments(startDateOnly,
                                                                                                                              endDateOnly,
                                                                                                                              doctor,
                                                                                                                              PatientData,
                                                                                                                              ExaminationType.GENERAL,
                                                                                                                              roomControoler.Get(3), 0));
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveCommandExecute(), param => CanSaveCommandExecute()));
            }
        }

        private bool CanSaveCommandExecute()
        {
            return SelectedItem != null;
        }

        public virtual void SaveCommandExecute()
        {
            _appointmentItems.Add(appointmentController.Create(SelectedItem));
            _generatedAppointments.Remove(SelectedItem);
        }

        // INTERNAL PRIVATE METHODS

        private void FillComboData()
        {

            foreach (Patient p in patientController.GetAll())
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = p.FirstName + " " + p.LastName, Value = p });
            }

        }
        private void FillComboData2()
        {

            foreach (Doctor p in doctorController.GetAll())
            {
                doctorComboBox.Add(new ComboBoxData<Doctor> { Name = p.FirstName + " " + p.LastName, Value = p });
            }

        }
    }
}
