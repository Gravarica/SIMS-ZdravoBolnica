using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class MedicalCardViewModel : BaseViewModel
    {

        public RelayCommand PatientInformationViewCommand { get; set; }

        public RelayCommand PreviousExaminationViewCommand { get; set; }

        public PatientInformationViewModel PatientInformationVM { get; set; }

        public PreviousExaminationsViewModel PreviousExaminationsVM { get; set; }

        private object _CurrentView;

        private MedicalRecord _medicalRecord;

        public object CurrentView
        {
            get 
            { 
                return _CurrentView; 
            }
            set 
            { 
                _CurrentView = value;
                OnPropertyChanged();
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

        public MedicalCardViewModel(Patient patient)
        {
            var app = System.Windows.Application.Current as App;

            _medicalRecord = app.MedicalRecordController.GetMedicalRecordByPatient(patient);

            PatientInformationVM = new PatientInformationViewModel(_medicalRecord);           // Ovde prosledim pacijenta kako bih prikazao njegove podatke

            PreviousExaminationsVM = new PreviousExaminationsViewModel(_medicalRecord);       // Ovde prosledim iz medicinskog kartona listu anamneza kako bih prikazao na tom pogledu

            PatientInformationViewCommand = new RelayCommand(o =>
            {
                CurrentView = PatientInformationVM;
            });

            PreviousExaminationViewCommand = new RelayCommand(o =>
            {
                CurrentView = PreviousExaminationsVM;
            });
        }


    }
}
