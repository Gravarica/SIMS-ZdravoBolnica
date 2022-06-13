using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.ValidationRules.DoctorValidation;
using HospitalProject.View.Model;
using HospitalProject.View.Util;
using Model;

using System.Threading.Tasks;
using HospitalProject.View.Secretary.SecretaryV;
using Syncfusion.ProjIO;
using Syncfusion.UI.Xaml.Scheduler;
using Task = System.Threading.Tasks.Task;

namespace HospitalProject.View.Secretary.SecretaryVM;

public  class DemoVM : BaseViewModel
{
      
        private DateTime _startDate;
        private DateTime _endDate;
        private Patient _patient;
        private Doctor _doctor;
        private Appointment _selectedItem;
        private bool _priority;
        private bool _isChecked;
        private AppointmentController _appointmentController; 
        private DoctorController _doctorController;
        private PatientController _patientController;
        private string _fullName;
        private RelayCommand _submitCommand;
        private RelayCommand _resetCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _cancelCommand;

        private RelayCommand _stopCommand;
        private List<ComboBoxData<Doctor>> _doctorComboBox = new List<ComboBoxData<Doctor>>();
        private List<ComboBoxData<Patient>> _patientComboBox = new List<ComboBoxData<Patient>>();
        public System.Windows.Visibility _showLabel1;
        public System.Windows.Visibility _showLabel2;
        private ObservableCollection<Appointment> _generatedAppointments;
        private ObservableCollection<Appointment> _appointmentItems;

        public DemoVM()
        {
            _showLabel1 = System.Windows.Visibility.Hidden;
            
            _showLabel2 = System.Windows.Visibility.Hidden;
            IsChecked = true;
            InitializeControllers();
            InitializeData();
            Task.Delay(3000);
            Demo();
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
            AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAll().ToList());

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

        public ObservableCollection<Appointment> AppointmentItems
        {
            get
            {
                return _appointmentItems;
            }
            set
            {
                _appointmentItems = value;
                OnPropertyChanged(nameof(AppointmentItems));
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
                return _submitCommand ?? (_submitCommand = new RelayCommand(param => SubmitCommandExecute()));
            }
        }

        public RelayCommand StopDemo
        {
            get
            {
                return _stopCommand ?? (_stopCommand = new RelayCommand(param => StopDemoExecute()));
            }
        }

        private void StopDemoExecute()
        {
            SelectedItem = null; //ne moze da se execute
            AddNewAppointmentV view = new AddNewAppointmentV();
            view.DataContext = new AddNewAppointmentVM(AppointmentItems);
            SecretaryMainViewVM.Instance.CurrentView = view;
        }


        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
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
            
                GeneratedAppointments = new ObservableCollection<Appointment>(DoctorIsPriority(startDateOnly, endDateOnly, DoctorData, PatientData));
       
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
            return (SelectedItem != null);
        }

        public async void SaveCommandExecute()
        {
           
            Appointment demo = _appointmentController.Create(SelectedItem);
                
               await Task.Delay(1000);
                            
               _generatedAppointments.Remove(SelectedItem);
                            
               await Task.Delay(2000);
       
        }

      

        public bool FlagForValue1
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = true;

            }
        }

        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
    
        public System.Windows.Visibility  ShowLabel1
        {
            get
            {
                return _showLabel1;
            }
            set
            {
                _showLabel1 = value;
                OnPropertyChanged(nameof(ShowLabel1));
            }
        }

        public System.Windows.Visibility  ShowLabel2
        {
            get
            {
                return _showLabel2;
            }
            set
            {
                _showLabel2 = value;
                OnPropertyChanged(nameof(ShowLabel2));
            }
        }
        public async  void Demo()
        {
            
            await Task.Delay(2000);
            ShowLabel1 = System.Windows.Visibility.Visible;
            DoctorData = _doctorController.Get(1); 
            await Task.Delay(2000);

            PatientData = _patientController.Get(3);
            FullName = PatientData.FirstName + " " + PatientData.LastName;
            
            await Task.Delay(2000);
            
            ShowLabel1 = System.Windows.Visibility.Hidden;
            
            await Task.Delay(1000);
            ShowLabel2 = System.Windows.Visibility.Visible;
            
            await Task.Delay(2000);
           StartDate = new DateTime(2022, 10, 7, 10, 0, 0);
            await Task.Delay(2000);
           EndDate  = new DateTime(2022, 10, 10, 10, 0, 0);
           await Task.Delay(2000);
           
           FlagForValue1 = true;
              
            await Task.Delay(2000);
            
           SubmitCommandExecute();
           
           ShowLabel2 = System.Windows.Visibility.Hidden;
           await Task.Delay(1000);
           
           SelectedItem = GeneratedAppointments[1];
           await Task.Delay(1000);

           SaveCommandExecute();

        }
}
    
          
          
  
      

