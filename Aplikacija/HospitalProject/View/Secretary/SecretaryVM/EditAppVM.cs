using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.ValidationRules.DoctorValidation;
using HospitalProject.ValidationRules.PatientValidation;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class EditAppVM : BaseViewModel
    {

        private AppointmentController _appointmentController;
        private DoctorController _doctorController;
        private PatientController _patientController;
        private UserController _userController;
        private RoomControoler _roomController;
        private Window window;


        private NotificationController notificationController;

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

        public EditAppVM(Appointment appointment, ObservableCollection<Appointment> appointmentItems)
        {
            InitializeControllers();
            showItem = appointment;
            _appointmentItems = appointmentItems;
            DoctorData = showItem.Doctor;
            PatientData = showItem.Patient;
        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;

            notificationController = app.NotificationController;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
            _userController = app.UserController;
            _roomController = app.RoomController;

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
                IsTimeFrameValid() &&
                NewAppointmentValidation.IsDateAfterNow(StartDate, endDate)
                ;
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            GeneratedAppointments = new ObservableCollection<Appointment>(_appointmentController.GenerateAvailableAppointments(startDateOnly,
                endDateOnly,
                DoctorData,
                patient,
                ExaminationType.GENERAL, _roomController.Get(3),1));
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
            _appointmentController.Update(SelectedItem);
            ShowItem.Date = SelectedItem.Date;
            window.Close();

            /* DoctorData = SelectedItem.Doctor;
            List<Notification> notifications = new List<Notification>();
            notifications = notificationController.GetNotificationsByDoctor(Doctor.Id);
            
            Notification notification = new Notification("Appointment with id " + SelectedItem.Id + "is updated", DateTime.Now);

            notificationController.Insert(notification);
           */
        }

        

        private bool IsTimeFrameValid()
        {
            return EditAppointmentValidation.MoreThanFourDaysCheck(StartDate, endDate, showItem.Date);

        }

    }
}
