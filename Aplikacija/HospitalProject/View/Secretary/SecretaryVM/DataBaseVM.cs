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
using HospitalProject.Service;
using HospitalProject.View.Secretary.SecretaryV;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class DataBaseVM : BaseViewModel
    {
       
        private RelayCommand showProfileCommand;
        private RelayCommand deleteAllergyCommand;
        EquipementController _equipmentController;

        AllergiesController _allergiesController;
        PatientController _patientController;
        DoctorController _doctorController;
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Equipement> Equipment { get; set; }
        public ObservableCollection<Allergies> Allergies { get; set; }

        private Doctor _doctor;

        private Patient _patient;
        private string _firstname;
        private string _lastname;

        private string _name;
        private Specialization _specialization;

        private int _jmbg;
        private int _quantity;
        private Room _room;
        private int _id;
        private Equipement _equipment;

        private Allergies _allergies;
        private Patient selectedPatient;
        private Doctor selectedDoctor;
        private Allergies selectedAllergy;
        private Equipement selectedEquipment;

        public DataBaseVM()
        {

            InstantiateControllers();
            InstantiateData();
          

        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _allergiesController = app.AllergiesController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
            _equipmentController = app.EquipementController;
        }

        private void InstantiateData()
        {
            Patients = new ObservableCollection<Patient>(_patientController.GetAll().ToList());
            Doctors = new ObservableCollection<Doctor>(_doctorController.GetAll().ToList());
            Allergies = new ObservableCollection<Allergies>(_allergiesController.GetAll().ToList());
            Equipment = new ObservableCollection<Equipement>(_equipmentController.GetAll().ToList());


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

        public RelayCommand ShowProfileCommand
        {
            get
            {
                return showProfileCommand ?? (showProfileCommand = new RelayCommand(param => ExecuteShowProfileCommand(),
                                                                                     param => CanExecute()));
            }
        }

        private bool CanExecute()
        {
            return SelectedPatient!= null;
        }

        private MedicalRecordService medicalRecordService;
        private void ExecuteShowProfileCommand()
        {

            PatientV view = new PatientV();
            view.DataContext = new PatientProfileVM(SelectedPatient, medicalRecordService);
            view.ShowDialog();
        }

        public Allergies SelectedAllergy
        {
            get
            {
                return selectedAllergy;
            }
            set
            {
                selectedAllergy = value;
                OnPropertyChanged(nameof(SelectedAllergy));
            }
        }

        public RelayCommand DeleteAllergyCommand
        {
            get
            {
                return deleteAllergyCommand ?? (deleteAllergyCommand = new RelayCommand(param => ExecuteDeleteAllergyCommand(),
                                                                                     param => CanExecuteA()));
            }
        }


        private void ExecuteDeleteAllergyCommand()
        {

            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                _allergiesController.Delete(SelectedAllergy.Id);
                Allergies.Remove(SelectedAllergy);

            }

        }

        private bool CanExecuteA()
        {
            return SelectedAllergy != null;
        }
        public Equipement SelectedEquipment
        {
            get
            {
                return selectedEquipment;
            }
            set
            {
                selectedEquipment = value;
                OnPropertyChanged(nameof(SelectedEquipment));
            }
        }

        public String FirstName
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public String LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int Jmbg
        {
            get
            {
                return _jmbg;
            }
            set
            {
                _jmbg = value;
                OnPropertyChanged(nameof(Jmbg));
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity= value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public Room Room
        {
            get
            {
                return _room;
            }
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
            }
        }



    }


}
