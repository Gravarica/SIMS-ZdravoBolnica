using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.ValidationRules.DoctorValidation;
using HospitalProject.View.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class NewAppointmentViewModel : BaseViewModel
    {

        private AppointmentController appointmentController;
        private DoctorController doctorController;
        private PatientController patientController;
        private UserController userController;
        private RoomControoler roomController;

        private Window window;

        private DateTime startDate;
        private DateTime endDate;
        private Patient patient;
        private Doctor doctor;
        private Room room;
        private ObservableCollection<Appointment> _generatedAppointments;
        private Appointment selectedItem;
        private ObservableCollection<Appointment> _appointmentItems;
        private ExaminationType selectedExamination;

        private List<ComboBoxData<Patient>> patientComboBox = new List<ComboBoxData<Patient>>();
        private List<ComboBoxData<ExaminationType>> examinationTypeComboBox = new List<ComboBoxData<ExaminationType>>();
        private List<ComboBoxData<Room>> roomsComboBox = new List<ComboBoxData<Room>>();
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

        public NewAppointmentViewModel(ObservableCollection<Appointment> AppointmentItems, Window window)
        {
            this.window = window;
            InitializeControllers();
            InitializeData(AppointmentItems);
        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;

            appointmentController = app.AppointmentController;
            patientController = app.PatientController;
            doctorController = app.DoctorController;
            userController = app.UserController;
            roomController = app.RoomController;
        }

        private void InitializeData(ObservableCollection<Appointment> AppointmentItems)
        {
            _appointmentItems = AppointmentItems;
            doctor = doctorController.GetLoggedDoctor(userController.GetLoggedUser().Username);
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

        public Doctor LoggedDoctor 
        { 
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged(nameof(LoggedDoctor));
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
                OnPropertyChanged(nameof(StartDate));
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

        public List<ComboBoxData<ExaminationType>> ExaminationTypeComboBox
        {
            get
            {
                return examinationTypeComboBox;
            }
            set
            {
                examinationTypeComboBox = value;
                OnPropertyChanged(nameof(ExaminationTypeComboBox));
            }
        }

        public List<ComboBoxData<Room>> RoomComboBox
        { 
            get
            {
                return roomsComboBox;
            }
            set
            {
                roomsComboBox = value;
                OnPropertyChanged(nameof (RoomComboBox));
            }
        }

        public ExaminationType SelectedExamination
        { 
            get
            {
                return selectedExamination;
            }
            set
            {
                selectedExamination = value;
                OnPropertyChanged(nameof(SelectedExamination));
            }
        }

        public Room SelectedRoom
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
                OnPropertyChanged(nameof(SelectedRoom));    
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
                                                                                                                              SelectedExamination,
                                                                                                                              SelectedRoom));
            
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
            window.Close();
        }

        // INTERNAL PRIVATE METHODS

        private void FillComboData()
        {
            FillPatientComboData();
            FillExaminationTypeComboData();
            FillRoomComboData();
        }

        private void FillPatientComboData()
        {
            foreach (Patient p in patientController.GetAll())
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = p.FirstName + " " + p.LastName, Value = p });
            }
        }

        private void FillExaminationTypeComboData()
        {
            foreach (ExaminationType examType in Enum.GetValues(typeof(ExaminationType)))
            {
                examinationTypeComboBox.Add(new ComboBoxData<ExaminationType> { Name = examType.ToString(), Value = examType });
            }
        }

        private void FillRoomComboData()
        {
            foreach (Room room in roomController.GetAll())
            {
                roomsComboBox.Add(new ComboBoxData<Room> { Name = room.Number.ToString(), Value = room });
            }
        }
        
    }
}
