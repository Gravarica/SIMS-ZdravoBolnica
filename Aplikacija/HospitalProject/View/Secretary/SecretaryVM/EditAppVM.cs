using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.ValidationRules.DoctorValidation;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class EditAppVM : BaseViewModel
    {

        private AppointmentController appointmentController;
        private DoctorController doctorController;
        private PatientController patientController;
        private UserController userController;

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
        private RelayCommand returnCommand;

        public EditAppVM(Appointment appointment, ObservableCollection<Appointment> appointmentItems)
        {
            InitializeControllers();
           // InitializeData();
            showItem = appointment;
            _appointmentItems = appointmentItems;
            PatientData = showItem.Patient;
            DoctorData = showItem.Doctor;
        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;

            appointmentController = app.AppointmentController;
            patientController = app.PatientController;
            doctorController = app.DoctorController;
            userController = app.UserController;
        }

       /* private void InitializeData()
        {
            doctor = doctorController.Get(id);   // IZMENITI KAD BUDE LOGIN
        }*/

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
                                                                                                                              ShowItem.ExaminationType,
                                                                                                                              ShowItem.Room));
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
