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
using HospitalProject.View.Util;
using Model;
using Syncfusion.Data.Extensions;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class EditProfileVM : BaseViewModel
    {
        private Patient _patient;
        private Allergies _selectedAllergy;
        private MedicalRecord _medicalRecord;
        private RelayCommand _saveCommand;
        
        private RelayCommand _edit;
        private RelayCommand _addComand;
        private int _gender = 0;
        
        
        private Allergies _selectedAllergyCB;
        private RelayCommand _delete;
        private PatientController patientController;
        
        private AllergiesController _allergiesController;
        private List<ComboBoxData<Allergies>> allergies = new List<ComboBoxData<Allergies>>();
        private MedicalRecordController _medicalRecordController;
        
        public ObservableCollection<Allergies> Allergy { get; set; }
        public EditProfileVM(Patient patient)
        {
            _patient = patient;
            var app = System.Windows.Application.Current as App;
            this._allergiesController  = app.AllergiesController;
            patientController = app.PatientController;
            _medicalRecordController = app.MedicalRecordController;

            _medicalRecord = _medicalRecordController.GetMedicalRecordByPatient(_patient);
            Allergy = _medicalRecord.Allergies.ToObservableCollection();
            
            FillComboData();
        }

        private void FillComboData()
        {
            foreach (Allergies allergy in _allergiesController.GetAll().ToList())
            {
                allergies.Add(new ComboBoxData<Allergies> { Name = allergy.Name, Value = allergy });
            }
        }

        public Patient Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }
 
        
        public MedicalRecord MedicalRecord
        {
            get
            {
                return _medicalRecord;
            }
            set
            {
                _medicalRecord = value;
                OnPropertyChanged(nameof(MedicalRecord));
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
        
        public Allergies SelectedAllergyCB
        {
            get
            {
                return _selectedAllergyCB;
            }
            set
            {
                _selectedAllergyCB = value;
                OnPropertyChanged(nameof(SelectedAllergyCB));
            }
        }
        public List<ComboBoxData<Allergies>> Allergies
        {
            get
            {
                return allergies;
            }
            set
            {
                allergies = value;
                OnPropertyChanged(nameof(Allergies));
            }
        }
        public bool GenderFemale
        {
            get
            {
                return Patient.Gender == Gender.female;
            }
            set
            {
                _gender = 1;
                OnPropertyChanged(nameof(GenderMale));
            }
        }
        
        public bool GenderMale
        {
            get
            {
                return Patient.Gender == Gender.male;
            }
            set
            {
                _gender = 2;
                OnPropertyChanged(nameof(GenderFemale));
            }
        }
 
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => ExecuteSaveCommand(), param => CanExecute()));
            }
        }

        private bool CanExecute()
        {
            return true;
        }

        private void ExecuteSaveCommand()
        {   
            patientController.Update(Patient);
        }

        private bool CanExecuteAdd()
        {

            return SelectedAllergyCB != null;
        }
        public RelayCommand Delete
        {
            get
            {
                return _delete ?? (_delete = new RelayCommand(param => ExecuteDelete()));
            }
        }
        public RelayCommand Edit
        {
            get
            {
                return _edit ?? (_edit = new RelayCommand(param => ExecuteEdit()));
            }
        }
        public RelayCommand Add
        {
            get
            {
                return _addComand ?? (_addComand = new RelayCommand(param => ExecuteAdd(), param => CanExecuteAdd()));
            }
        }
        
        private void ExecuteDelete()
        {
            _medicalRecordController.RemoveAllergiesFromMedicalRecord(SelectedAllergy, Patient);
            Allergy.Remove(SelectedAllergy);
        }
        private void ExecuteEdit()
        {
            patientController.Update(Patient);
            MessageBox.Show("Patient profile edited!", "note", MessageBoxButton.OK);
        }
        private bool AlreadyAdded()
        {
            foreach (Allergies allergy in Allergy)
            {
                if (allergy.Id == SelectedAllergyCB.Id)
                {
                    return true;
                }
            }
            return false ;
        }
        private void ExecuteAdd()
        { 

            if (AlreadyAdded() == false)
            {
                
            _medicalRecordController.AddNewAllergiesToMedicalRecord(SelectedAllergyCB, Patient);
            Allergy.Add(SelectedAllergyCB);
            }
            else
            {
                MessageBox.Show("Allergy is already added!", "warning", MessageBoxButton.OK);

            }
        }
    }
}
