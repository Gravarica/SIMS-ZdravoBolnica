using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.ValidationRules.DoctorValidation;
using HospitalProject.ValidationRules.PatientValidation;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
     public class EditAppointmentPatientViewModel : BaseViewModel
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
        private ObservableCollection<Appointment> _generatedAppointments;
        private Appointment showItem;
        private Appointment selectedItem;
        private ObservableCollection<Appointment> _appointmentItems;
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

        public EditAppointmentPatientViewModel(Appointment appointment, ObservableCollection<Appointment> appointmentItems, Window window)
        {
            this.window = window;
            InitializeControllers();
            InitializeData();
            showItem = appointment;
            _appointmentItems = appointmentItems;
            DoctorData = showItem.Doctor;
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
            patient = patientController.GetLoggedPatient(userController.GetLoggedUser().Username);   
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

        public Patient LoggedPatient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged(nameof(LoggedPatient));
            }
        }

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
                //LessThanADayRemainingUntillAppointmentCheck() &&
                IsTimeFrameValid() &&
                NewAppointmentValidation.IsDateAfterNow(StartDate,endDate)
                ; 
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            GeneratedAppointments = new ObservableCollection<Appointment>(appointmentController.GenerateAvailableAppointments(startDateOnly,
                                                                                                                              endDateOnly,
                                                                                                                              DoctorData,
                                                                                                                              patient,
                                                                                                                              ExaminationType.GENERAL, roomController.Get(3)));
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
            window.Close();
        }

        private bool LessThanADayRemainingUntillAppointmentCheck() {

            DateTime date = showItem.Date;
            return EditAppointmentValidation.LessThank24HoursCheck(date);
        }

        private bool IsTimeFrameValid()
        {
            return EditAppointmentValidation.MoreThanFourDaysCheck(StartDate, endDate, showItem.Date);
            
        }

    }
}
