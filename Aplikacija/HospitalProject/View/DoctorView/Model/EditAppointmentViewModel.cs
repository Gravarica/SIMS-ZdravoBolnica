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

namespace HospitalProject.View.DoctorView.Model
{
    public class EditAppointmentViewModel : BaseViewModel
    {

        private AppointmentController appointmentController;
        private DoctorController doctorController;
        private PatientController patientController;
        private UserController userController;
        private RoomControoler roomController;

        private DateTime startDate;
        private DateTime endDate;
        private Patient patient;
        private Doctor doctor;
        private Room room;
        private ObservableCollection<Appointment> _generatedAppointments;
        private Appointment showItem;
        private Appointment selectedItem;
        private ExaminationType selectedExamination;

        private ObservableCollection<Appointment> _appointmentItems;
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
        private RelayCommand cancelCommand;
        private RelayCommand saveCommand;
        private RelayCommand returnCommand;

        public EditAppointmentViewModel(Appointment appointment, ObservableCollection<Appointment> appointmentItems)
        {
            InitializeControllers();
            InitializeData();
            showItem = appointment;
            _appointmentItems = appointmentItems;
            PatientData = showItem.Patient;
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

        private void InitializeData()
        {
            doctor = doctorController.GetLoggedDoctor(userController.GetLoggedUser().Username);   // IZMENITI KAD BUDE LOGIN
        }

        // PROPERTY DEFINITION

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

        public Appointment ShowItem 
        { 
            get
            {
                return showItem;
            }
            set
            {
                showItem = value;
                OnPropertyChanged(nameof(ShowItem));
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
                OnPropertyChanged(nameof(RoomComboBox));
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


        private bool CanSubmitCommandExecute()
        {
            return NewAppointmentValidation.IsStartBeforeEnd(StartDate, EndDate) && NewAppointmentValidation.IsComboBoxChecked(PatientData); ;
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
            SelectedItem.Id = showItem.Id;
            appointmentController.Update(SelectedItem);
            ShowItem.Date = SelectedItem.Date;
        }
    }
}
