using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.ValidationRules.DoctorValidation;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class AddNewAppointmentVM : BaseViewModel
    {

        private Window _window;

        private DateTime _startDate;
        private DateTime _endDate;
        private Patient _patient;
        private Doctor _doctor;
        private Appointment _selectedItem;
        private int _priority = 0;
        
        private AppointmentController _appointmentController; 
        private DoctorController _doctorController;
        private PatientController _patientController;
        
        private RelayCommand _submitCommand;
        private RelayCommand _resetCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _cancelCommand;

        private List<ComboBoxData<Doctor>> _doctorComboBox = new List<ComboBoxData<Doctor>>();
        private List<ComboBoxData<Patient>> _patientComboBox = new List<ComboBoxData<Patient>>();
        
        
        private ObservableCollection<Appointment> _generatedAppointments;
        private ObservableCollection<Appointment> _appointmentItems;

        public AddNewAppointmentVM(ObservableCollection<Appointment> AppointmentItems)
        {
            _appointmentItems = AppointmentItems;
            InitializeControllers();
            InitializeData();
        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
        }

        private void InitializeData()
        {
            FillComboData();
        }

        private void FillComboData()
        {

            foreach (Doctor doctor in _doctorController.GetAll())
            {
                    _doctorComboBox.Add(new ComboBoxData<Doctor> 
                    { Name = doctor.FirstName + " " + doctor.LastName, Value = doctor });
            }

            foreach (Patient patient in _patientController.GetAll())
            {
                    _patientComboBox.Add(new ComboBoxData<Patient> 
                    { Name = patient.FirstName + " " + patient.LastName, Value = patient });
            }
        }


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


        public List<ComboBoxData<Doctor>> DoctorComboBox
        {
            get
            {
                return _doctorComboBox;

            }
            set
            {
                _doctorComboBox = value;
                OnPropertyChanged(nameof(DoctorComboBox));
            }
        }

        public List<ComboBoxData<Patient>> PatientComboBox
        {
            get
            {
                return _patientComboBox;

            }
            set
            {
                _patientComboBox = value;
                OnPropertyChanged(nameof(PatientComboBox));
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
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

        public Appointment SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand(param => SubmitCommandExecute(), param => CanSubmitCommandExecute()));
            }
        }

        private bool CanSubmitCommandExecute()
        {
            return NewAppointmentValidation.IsStartBeforeEnd(StartDate, EndDate) &&
                   NewAppointmentValidation.IsDateAfterNow(StartDate, EndDate) &&
                   (_priority == 1 || _priority == 2);
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);

            {
                GenerateAppointmentsForPriority(startDateOnly, endDateOnly);
            }

        }

        private void GenerateAppointmentsForPriority(DateOnly startDateOnly, DateOnly endDateOnly)
        {
            
            if (_priority == 1)
            {
                GeneratedAppointments = new ObservableCollection<Appointment>(DoctorIsPriority(startDateOnly, endDateOnly, DoctorData, PatientData));
            }
            else if (_priority == 2)
            {
                GeneratedAppointments = new ObservableCollection<Appointment>(DateIsPriority(startDateOnly, endDateOnly, PatientData));
            }
            
        }
        

        private IEnumerable<Appointment> DoctorIsPriority(DateOnly startDate, DateOnly endDate, Doctor doctor, Patient patient)
        {
            return _appointmentController.GenerateAppointmentsPriorityDoctor(startDate, endDate, DoctorData, PatientData);
        }

        private IEnumerable<Appointment> DateIsPriority(DateOnly startDate, DateOnly endDate, Patient patient)
        {
            return _appointmentController.GenerateAppointmentsPriorityDate(startDate, endDate, PatientData, DoctorData);
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => SaveCommandExecute(), param => CanSaveCommandExecute()));
            }
        }

        private bool CanSaveCommandExecute()
        {
            return SelectedItem != null;
        }

        public virtual void SaveCommandExecute()
        {
            _appointmentItems.Add(_appointmentController.Create(SelectedItem));
            _generatedAppointments.Remove(SelectedItem);
        }

        private bool CanCancelCommandExecute()
        {
            return true;
        }
        

        public bool FlagForValue1
        {
            get
            {
                return (_priority == 1) ? true : false;
            }
            set
            {
                _priority = 1;
                OnPropertyChanged(nameof(FlagForValue2));

            }
        }

        public bool FlagForValue2
        {
            get
            {
                return (_priority == 2) ? true : false;
            }
            set
            {
                _priority = 2;
                OnPropertyChanged(nameof(FlagForValue1));

            }
        }
    }

}
