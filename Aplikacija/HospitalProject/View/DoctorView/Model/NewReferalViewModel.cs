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
    public class NewReferalViewModel : BaseViewModel
    {
        private Patient selectedPatient;
        private Appointment selectedAppointment;
        private DateTime startDate;
        private DateTime endDate;
        private int _intValue;
        private AnamnesisViewModel returnVM;

        private AppointmentController appointmentController;
        private DoctorController doctorController;

        private Window window;

        private RelayCommand submitCommand;
        private RelayCommand resetCommand;
        private RelayCommand saveCommand;
        private RelayCommand returnCommand;
        private RelayCommand fillComboDataCommand;

        private List<ComboBoxData<Specialization>> specializationComboBox;
        private ObservableCollection<ComboBoxData<Doctor>> doctorComboBox;

        private Specialization selectedSpecialization;
        private Doctor selectedDoctor;
        private ObservableCollection<Appointment> _generatedAppointments;
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

        public NewReferalViewModel(Patient ShowItem, AnamnesisViewModel am)
        {
            returnVM = am;
            InstantiateData(ShowItem);
            InstantiateControllers();
        }

        private void InstantiateData(Patient ShowItem)
        {
            specializationComboBox = new List<ComboBoxData<Specialization>>();
            doctorComboBox = new ObservableCollection<ComboBoxData<Doctor>>();
            FillComboData();
            SelectedPatient = ShowItem;
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            appointmentController = app.AppointmentController;
            doctorController = app.DoctorController;
        }

        public Patient SelectedPatient
        {
            get
            {
                return selectedPatient;
            }
            set
            {
                selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
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

        public Appointment SelectedItem
        {
            get
            {
                return selectedAppointment;
            }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
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
                OnPropertyChanged(nameof(FlagForValue1));

            }
        }
        
        public Specialization SelectedSpecialization
        {
            get
            {
                return selectedSpecialization;
            }
            set
            {
                if (selectedSpecialization != value)
                {
                    selectedSpecialization = value;
                    OnPropertyChanged(nameof(SelectedSpecialization));
                    FillDoctorComboBox(SelectedSpecialization);
                    OnPropertyChanged(nameof(DoctorComboBox));
                }
                
            }
        }

        public Doctor SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }

        public ObservableCollection<ComboBoxData<Doctor>> DoctorComboBox
        {
            get
            {
                return doctorComboBox;
            }
            set
            {
                if (doctorComboBox != value)
                {
                    doctorComboBox = value;
                    OnPropertyChanged(nameof(DoctorComboBox));
                }
            }
        }

        public List<ComboBoxData<Specialization>> SpecializationComboBox
        {
            get
            {
                return specializationComboBox;
            }
            set
            {
                if (specializationComboBox != value)
                {
                    specializationComboBox = value;
                    OnPropertyChanged(nameof(SpecializationComboBox));
                }
                
            }
        }

        private void FillSpecializationComboBox()
        {
            foreach(Specialization specialization in Enum.GetValues(typeof(Specialization)))
            {
                specializationComboBox.Add(new ComboBoxData<Specialization> { Name = specialization.ToString(), Value = specialization });
            }
        }

        private void FillDoctorComboBox(Specialization selected)
        {
            DoctorComboBox = new ObservableCollection<ComboBoxData<Doctor>>();

            foreach(Doctor doctor in doctorController.GetDoctorsBySpecialization(selected))
            {
                DoctorComboBox.Add(new ComboBoxData<Doctor> { Name = doctor.FirstName + " " + doctor.LastName, Value = doctor });
            }

        }

        private void FillComboData()
        {
            FillSpecializationComboBox();
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
            return NewAppointmentValidation.IsStartBeforeEnd(StartDate, EndDate);
        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            GeneratedAppointments = new ObservableCollection<Appointment>(appointmentController.GenerateAvailableAppointments(startDateOnly,
                                                                                                                              endDateOnly,
                                                                                                                              SelectedDoctor,
                                                                                                                              SelectedPatient,
                                                                                                                              ExaminationType.GENERAL,
                                                                                                                              SelectedDoctor.Ordination,
                                                                                                                              _intValue));

            if (GeneratedAppointments.Count() == 0)
            {
                MessageBox.Show("There are not free appointments for the interval selected. Please try another date", "No appointments", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveCommandExecute(), param => CanSaveCommandExecute()));
            }
        }

        private bool CanReturnCommandExecute()
        {
            return true;
        }

        private void ReturnCommandExecute()
        {
            MainViewModel.Instance.CurrentView = MainViewModel.Instance.AnamnesisVM;
        }

        public RelayCommand ReturnCommand
        {
            get
            {
                return returnCommand ?? (returnCommand =
                    new RelayCommand(param => ReturnCommandExecute(), param => CanReturnCommandExecute()));
            }
        }

        private bool CanSaveCommandExecute()
        {
            return SelectedItem != null;
        }

        public virtual void SaveCommandExecute()
        {
            appointmentController.Create(SelectedItem);
            GeneratedAppointments.Remove(SelectedItem);
            MainViewModel.Instance.CurrentView = returnVM;
        }

    }
}
