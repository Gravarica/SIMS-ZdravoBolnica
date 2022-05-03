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
using HospitalProject.View.Secretary.SecretaryV;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class PatientAllergiesVM : BaseViewModel
    {
        public ObservableCollection<Allergies> Allergies { get; set; }
        private Patient patient;

        private RelayCommand deleteAllergyCommand;
        private RelayCommand addAllergyCommand;

        private Allergies selectedAllergy;

        private String allergyName;
        private int id;
        PatientController _patientController;
        public ObservableCollection<Allergies> patientAllergies { get; set; }
        AllergiesController _allergiesController;
        MedicalRecordController _medicalRecordController;

        private List<ComboBoxData<Allergies>> allergije = new List<ComboBoxData<Allergies>>();
        public PatientAllergiesVM(Patient patient)
        {
            var app = System.Windows.Application.Current as App;
             Patient _patient = patient;
            _medicalRecordController = app.MedicalRecordController;
            MedicalRecord MR = _medicalRecordController.GetMedicalRecordByPatient(patient);

            patientAllergies = new ObservableCollection<Allergies>(MR.Allergies);
            _allergiesController = app.AllergiesController;
            List<Allergies> list = new(_allergiesController.GetAll());
            FillComboData(list);
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

            patientAllergies.Add(SelectedAllergyCB);

        }

        private void ExecuteDeleteAllergyCommand()
        {

            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

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

        private void OnPropertyChanged(string v)
        {
            throw new NotImplementedException();
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
