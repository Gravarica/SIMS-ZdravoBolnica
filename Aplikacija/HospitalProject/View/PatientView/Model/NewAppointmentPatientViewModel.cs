
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

namespace HospitalProject.View.PatientView.Model
{
    public class NewAppointmentPatientViewModel : BaseViewModel
    {

        private AppointmentController appointmentController;
        private DoctorController doctorController;
        private PatientController patientController;

        private DateTime startDate;
        private DateTime endDate;
        private Patient patient;
        private Doctor doctor;
        private ObservableCollection<Appointment> _generatedAppointments;
        private Appointment selectedItem;
        private ObservableCollection<Appointment> _appointmentItems;

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

        public NewAppointmentPatientViewModel(ObservableCollection<Appointment> AppointmentItems)
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
        }

        private void InitializeData()
        {
            patient = patientController.Get(3);   
            FillComboData();
        }

        private void FillComboData()
        {

            foreach (Doctor d in doctorController.GetAll())
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
                   NewAppointmentValidation.IsComboBoxCheckedDoctor(DoctorData) &&
                   NewAppointmentValidation.IsDateAfterNow(StartDate, EndDate);
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            GeneratedAppointments = new ObservableCollection<Appointment>(appointmentController.GenerateAvailableAppointments(startDateOnly,
                                                                                                                              endDateOnly,
                                                                                                                              DoctorData,
                                                                                                                              patient));
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
        }


        
    }
}
