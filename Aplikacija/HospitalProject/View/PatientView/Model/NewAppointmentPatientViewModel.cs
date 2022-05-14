
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
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

namespace HospitalProject.View.PatientView.Model
{
    public class NewAppointmentPatientViewModel : BaseViewModel
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
        private Appointment selectedItem;
        private ObservableCollection<Appointment> _appointmentItems;
        private int _intValue = 0;
        

        private List<ComboBoxData<Doctor>> doctorComboBox = new List<ComboBoxData<Doctor>>();


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

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged(nameof(Patient));
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

        private RelayCommand submitCommand;
        private RelayCommand resetCommand;
        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;

        public NewAppointmentPatientViewModel(ObservableCollection<Appointment> AppointmentItems,Window window)
        {

            this.window = window;
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
            userController = app.UserController;
            roomController = app.RoomController;
        }

        private void InitializeData()
        {
            patient = patientController.GetLoggedPatient(userController.GetLoggedUser().Username);   
            FillComboData();
        }

        private void FillComboData()
        {

            foreach (Doctor d in doctorController.GetDoctorsBySpecialization(Specialization.GENERAL))
            {
                doctorComboBox.Add(new ComboBoxData<Doctor> { Name = d.FirstName + " " + d.LastName, Value = d });
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
                   //NewAppointmentValidation.IsComboBoxCheckedDoctor(DoctorData) &&
                   NewAppointmentValidation.IsDateAfterNow(StartDate, EndDate) &&
                   (_intValue == 1 || _intValue == 2) ;
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            GeneratedAppointments = new ObservableCollection<Appointment>(appointmentController.GenerateAvailableAppointments(startDateOnly,
                                                                                                                              endDateOnly,
                                                                                                                              DoctorData,
                                                                                                                              patient,
                                                                                                                              HospitalProject.Model.ExaminationType.GENERAL,
                                                                                                                              roomController.Get(3), _intValue));
           /* if (GeneratedAppointments.Count == 0) {

                if (_intValue == 1) {

                     GeneratedAppointments = new ObservableCollection<Appointment>(DoctorIsPriority(startDateOnly,endDateOnly, DoctorData, patient));
                }
                else if(_intValue == 2)
                {
                     GeneratedAppointments = new ObservableCollection<Appointment>(DateIsPriority(startDateOnly, endDateOnly, patient));
                }
            }*/

        }

        private IEnumerable<Appointment> DoctorIsPriority(DateOnly startDate, DateOnly endDate, Doctor doctor, Patient patient) {

             return appointmentController.GenerateAppointmentsPriorityDoctor(startDate, endDate, DoctorData, patient );


        }

        private IEnumerable<Appointment> DateIsPriority(DateOnly startDate, DateOnly endDate,Patient patient) {

            return appointmentController.GenerateAppointmentsPriorityDate(startDate,endDate,patient, DoctorData);

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

        public RelayCommand CancelCommand
        {

            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(param => CancelCommandExecute(), param => CanCancelCommandExecute()));
            }

        }

        private bool CanCancelCommandExecute()
        {
            return true;
        }

        private void CancelCommandExecute()
        {
            window.Close();
        }

        public bool FlagForValue1
        {
            get
            {
                return (_intValue == 1) ? true : false;
            }
            set
            {
                _intValue = 1;
                
                //RaisePropertyChanged("FlagForValue2");
                OnPropertyChanged(nameof(FlagForValue2));
                

            }
        }

        public bool FlagForValue2
        {
            get
            {
                return (_intValue == 2) ? true : false;
            }
            set
            {
                _intValue = 2;
                //RaisePropertyChanged("FlagForValue1");
                OnPropertyChanged(nameof(FlagForValue1));

            }
        }

    }
}
