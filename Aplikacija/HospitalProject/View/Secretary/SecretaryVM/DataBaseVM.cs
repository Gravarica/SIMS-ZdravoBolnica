using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.Secretary.SecretaryV;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class DataBaseVM : BaseViewModel
    {
       
        //mozda selectedItem i selectedItem.getType
        private Patient _selectedPatient;
        private Doctor _selectedDoctor;
        private Allergies _selectedAllergy;
        private Equipement _selectedEquipment;
        private RelayCommand _pdf;
        private RelayCommand _showProfileCommand;
        private RelayCommand _addToMeeting;
        private RelayCommand _addToList;
        private RelayCommand _deleteAllergyCommand;
        private RelayCommand _deletePatientCommand;
        private EquipementController _equipmentController;
        private AllergiesController _allergiesController;
        private PatientController _patientController;
        private DoctorController _doctorController;
        private MeetingsController _meetingsController;
        
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Equipement> Equipment { get; set; }
        public ObservableCollection<Allergies> Allergies { get; set; } 
        public ObservableCollection<Meetings> Meetings { get; set; }

        private ObservableCollection<Doctor> _selectedDoctors = new ObservableCollection<Doctor>();

        public DataBaseVM()
        {
            InstantiateControllers();
            InstantiateData();
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
             _patientController = app.PatientController;
            _allergiesController = app.AllergiesController;
           
            _doctorController = app.DoctorController;
            _equipmentController = app.EquipementController;
            _meetingsController = app.MeetingsController;
        }

       

        private void InstantiateData()
        {
            Patients = new ObservableCollection<Patient>(_patientController.GetAll().ToList());
            Doctors = new ObservableCollection<Doctor>(_doctorController.GetAll().ToList());
            Allergies = new ObservableCollection<Allergies>(_allergiesController.GetAll().ToList());
            Equipment = new ObservableCollection<Equipement>(_equipmentController.GetAll().ToList());
            Meetings = new ObservableCollection<Meetings>(_meetingsController.GetAll().ToList());
        }
     

   
                
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public ObservableCollection<Doctor> SelectedDoctors
        {
            get
            {
                return _selectedDoctors;
                
            }
            set
            {
                _selectedDoctors = value;
                OnPropertyChanged(nameof(SelectedDoctors));
            }
        }

        public Allergies SelectedAllergy
        {
            get
            {
                return _selectedAllergy;
            }
            set
            {
                _selectedAllergy = value;
                OnPropertyChanged(nameof(SelectedAllergy));
            }
        }

        public Doctor SelectedDoctor
        {
            get
            {
                return _selectedDoctor;
            }
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public Equipement SelectedEquipment
        {
            get
            {
                return _selectedEquipment;
            }
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged(nameof(SelectedEquipment));
            }
        }


        public RelayCommand ShowProfileCommand
        {
            get
            {
                return _showProfileCommand ?? (_showProfileCommand = new RelayCommand(param => ExecuteShowProfileCommand(),
                                                                                     param => CanExecuteShowPatientProfileCommand()));
            }
        }

        public RelayCommand AddToMeeting
        {
            get
            {
                return _addToMeeting ?? (_addToMeeting = new RelayCommand(param => ExecuteAddToMeeting(),
                    param => CanExecuteAddToMeeting()));
            }
        }
        public RelayCommand AddToList
        {
            get
            {
                return _addToList ?? (_addToList = new RelayCommand(param => ExecuteAddToList(),
                    param => CanExecuteAddToList()));
            }
        }
      
      
        private void ExecuteAddToMeeting()
        {
            MeetingV view = new MeetingV();
            view.DataContext = new MeetingVM(SelectedDoctors);
            SecretaryMainViewVM.Instance.CurrentView = view;
        }

     
        private void ExecuteAddToList()
        {
           _selectedDoctors.Add(SelectedDoctor);
        }

        
        private void ExecuteShowProfileCommand()
        {
            PatientV view = new PatientV();
            view.DataContext = new PatientProfileVM(SelectedPatient); 
            SecretaryMainViewVM.Instance.CurrentView = view;
        }


        private void ExecuteDeleteAllergyCommand()
        {

            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
               // _allergiesController.Delete(SelectedAllergy.Id);
                Allergies.Remove(SelectedAllergy);
            }

        }

        public event EventHandler OnRequestClose ;

     
        private void ExecuteDeletePatientCommand()
        {

            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //  _patientController.Delete(SelectedPatient.Id); moras obrisati i sve appointment anamneze i medical recorde
                Patients.Remove(SelectedPatient);
                
            }
           
        }
        public RelayCommand DeleteAllergyCommand
        {
         
          get
            {
                return _deleteAllergyCommand ?? (_deleteAllergyCommand = new RelayCommand(param => ExecuteDeleteAllergyCommand(),
                                                                                     param => CanExecuteDeleteAllergyCommand()));
            }
        }

   
        public RelayCommand DeletePatientCommand
        {
         
            get
            {
                return _deletePatientCommand ?? (_deletePatientCommand = new RelayCommand(param => ExecuteDeletePatientCommand(),
                    param => CanExecuteShowPatientProfileCommand()));
            }
        }
        

        private bool CanExecuteDeleteAllergyCommand()
        {
            return SelectedAllergy != null;
        }
 
         
        private bool CanExecuteShowPatientProfileCommand()
        {
            return SelectedPatient!= null;
        }

        private bool CanExecuteAddToList()
        {
            return (SelectedDoctor!= null && DoctorAlreadyAdded());
        }
        
        private bool CanExecutePdf()
        {
            return (SelectedDoctor!= null);
        }
        private bool DoctorAlreadyAdded()
        {
            foreach (Doctor doctor in SelectedDoctors)
            {
                if (doctor.Id == SelectedDoctor.Id)
                {
                    return false;
                }
            }
            return true ;
        }
        private bool CanExecuteAddToMeeting()
        {
            return SelectedDoctors.Count!= 0;
        }

    }
}
