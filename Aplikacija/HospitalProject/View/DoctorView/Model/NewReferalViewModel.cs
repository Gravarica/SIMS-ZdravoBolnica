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

        private AppointmentController appointmentController;
        private DoctorController doctorController;

        private Window window;

        private RelayCommand submitCommand;
        private RelayCommand resetCommand;
        private RelayCommand saveCommand;
        private RelayCommand fillComboDataCommand;

        private List<ComboBoxData<Specialization>> specializationComboBox;
        private List<ComboBoxData<Doctor>> doctorComboBox;

        private Specialization selectedSpecialization;
        private Doctor selectedDoctor;
        private ObservableCollection<Appointment> _generatedAppointments;

        public ObservableCollection<Doctor> SpecializationDoctors { get; set; }
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

        public NewReferalViewModel(Patient ShowItem, Window window)
        {
            InstantiateData(ShowItem, window);
            InstantiateControllers();
        }

        private void InstantiateData(Patient ShowItem, Window window)
        {
            specializationComboBox = new List<ComboBoxData<Specialization>>();
            doctorComboBox = new List<ComboBoxData<Doctor>>();
            FillComboData();
            SelectedPatient = ShowItem;
            this.window = window;
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
                    OnPropertyChanged(nameof(Specialization));
                    FillDoctorComboBox(selectedSpecialization);
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

        public List<ComboBoxData<Specialization>> SpecializationComboBox
        {
            get
            {
                return specializationComboBox;
            }
            set
            {
                specializationComboBox = value;
                OnPropertyChanged(nameof(SpecializationComboBox));
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
            foreach(Doctor doctor in doctorController.GetDoctorsBySpecialization(selected))
            {
                doctorComboBox.Add(new ComboBoxData<Doctor> { Name = doctor.FirstName + " " + doctor.LastName, Value = doctor });
            }
            OnPropertyChanged("DoctorComboBox");
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
                                                                                                                              SelectedDoctor.Ordination));

            if (GeneratedAppointments.Count() == 0)
            {
                MessageBox.Show("There are not free appointments for the inverval selected. Please try another date", "No appointments", MessageBoxButton.OK, MessageBoxImage.Information);
            }

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
            appointmentController.Create(SelectedItem);
            GeneratedAppointments.Remove(SelectedItem);
            window.Close();
        }

    }
}
