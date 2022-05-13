using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.Service;
using HospitalProject.View.Secretary.SecretaryV;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class PatientProfileVM : BaseViewModel
    {

        public ObservableCollection<Allergies> Allergies { get; set; }
        private Patient patient;

        private RelayCommand deleteAllergyCommand;
        private RelayCommand addAllergyCommand;

        private Allergies selectedAllergy;

        private String allergyName;
        private MedicalRecord MR;
        private int id;
        private MedicalRecordService medicalRecordService;
        PatientController _patientController;
        AllergiesController _allergiesController;
        MedicalRecordController _medicalRecordController;
        public ObservableCollection<Allergies> patientAllergies { get; set; }

        private List<ComboBoxData<Allergies>> allergije = new List<ComboBoxData<Allergies>>();

        MedicalRecord _medicalRecord;
        private Patient _patient;
        private MedicalRecordService _medicalRecordService;
        private RelayCommand showAllergiesCommand;
        private RelayCommand edit;
        public PatientProfileVM(MedicalRecord medicalRecord)
        {
            _medicalRecord = medicalRecord;
        }

        public PatientProfileVM(Patient patient, MedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
            _patient = patient;
            var app = System.Windows.Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;
            this.medicalRecordService = medicalRecordService;

            MedicalRecord Mer = _medicalRecordController.GetMedicalRecordByPatient(_patient);
            MR = Mer;
            patientAllergies = new ObservableCollection<Allergies>(MR.Allergies);
            _allergiesController = app.AllergiesController;
            List<Allergies> list = new(_allergiesController.GetAll());
            FillComboData(list);

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
 
        public Allergies SelectedAllergy
        {
            get
            {
                return selectedAllergy;
            }
            set
            {
                selectedAllergy = value;
                OnPropertyChanged(nameof(selectedAllergy));
            }
        }

        public Allergies SelectedAllergyCB
        {
            get
            {
                return selectedAllergy;
            }
            set
            {
                selectedAllergy = value;
                OnPropertyChanged(nameof(SelectedAllergyCB));
            }
        }

        public RelayCommand DeleteAllergyCommand
        {
            get
            {
                return deleteAllergyCommand ?? (deleteAllergyCommand = new RelayCommand(param => ExecuteDeleteAllergyCommand(),
                                                                                     param => CanExecute()));
            }
        }
        public RelayCommand AddAllergyCommand
        {
            get
            {
                return addAllergyCommand ?? (addAllergyCommand = new RelayCommand(param => ExecuteaddAllergyCommand()));
            }
        }



        public void ExecuteaddAllergyCommand()
        {

            medicalRecordService.AddNewAllergiesToMedicalRecord(SelectedAllergyCB, Patient.Id);
            _medicalRecordController.Update(MR);



        }

        private void ExecuteDeleteAllergyCommand()
        {

            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                MedRec.Allergies.Remove(SelectedAllergy);
                _medicalRecordController.Update(MR);
                patientAllergies.Remove(SelectedAllergy);
            }

        }

        private bool CanExecute()
        {
            return SelectedAllergy != null;
        }




        public string AllergyName
        {
            get { return allergyName; }
            set
            {
                allergyName = value;
                OnPropertyChanged(nameof(AllergyName));
            }

        }

       

        public MedicalRecord MedRec
        {
            get
            {
                return MR;
            }
            set
            {
                MR = value;
                OnPropertyChanged(nameof(MedRec));
            }
        }


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private void FillComboData(List<Allergies> list)
        {
            foreach (Allergies al in list)
            {
                allergije.Add(new ComboBoxData<Allergies> { Name = al.Name, Value = al });
            }

        }

        public List<ComboBoxData<Allergies>> AllergiesCB
        {

            get
            {
                return allergije;
            }
            set
            {
                allergije = value;
                OnPropertyChanged(nameof(AllergiesCB));
            }
        }


    }
}


