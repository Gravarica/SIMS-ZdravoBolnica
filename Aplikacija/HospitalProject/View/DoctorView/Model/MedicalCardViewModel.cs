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
        private ReturnFlag flag;

        public RelayCommand PatientInformationViewCommand { get; set; }

        public RelayCommand PreviousExaminationViewCommand { get; set; }

        public RelayCommand PrescriptionHistoryViewCommand { get; set; }

        private RelayCommand returnCommand;

        public PatientInformationViewModel PatientInformationVM { get; set; }

        public PreviousExaminationsViewModel PreviousExaminationsVM { get; set; }

        public PrescriptionHistoryViewModel PrescriptionHistoryVM { get; set; } 

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

        public ReturnFlag Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        public MedicalCardViewModel(Patient patient, ReturnFlag returnFlag)
        {
            var app = System.Windows.Application.Current as App;

            Flag = returnFlag;

            _medicalRecord = app.MedicalRecordController.GetMedicalRecordByPatient(patient);

            PatientInformationVM = new PatientInformationViewModel(_medicalRecord);           // Ovde prosledim pacijenta kako bih prikazao njegove podatke

            PreviousExaminationsVM = new PreviousExaminationsViewModel(_medicalRecord);       // Ovde prosledim iz medicinskog kartona listu anamneza kako bih prikazao na tom pogledu

            PrescriptionHistoryVM = new PrescriptionHistoryViewModel(_medicalRecord);

            CurrentView = PatientInformationVM;

            PatientInformationViewCommand = new RelayCommand(o =>
            {
                CurrentView = PatientInformationVM;
            });

            PreviousExaminationViewCommand = new RelayCommand(o =>
            {
                CurrentView = PreviousExaminationsVM;
            });

            PrescriptionHistoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = PrescriptionHistoryVM;
            });
        }

        public RelayCommand ReturnCommand
        {
            get
            {
                return returnCommand ?? (returnCommand =
                    new RelayCommand(o => ReturnCommandExecute(), o => CanReturnCommandExecute()));
            }
        }

        private void ReturnCommandExecute()
        {
            if (flag == ReturnFlag.PATIENT_VIEW)
            {
                MainViewModel.Instance.CurrentView = MainViewModel.Instance.PatientsVM;
            } else if (flag == ReturnFlag.APPOINTMENT_VIEW)
            {
                MainViewModel.Instance.CurrentView = MainViewModel.Instance.AppVM;
            }
            else
            {
                MainViewModel.Instance.CurrentView = MainViewModel.Instance.AnamnesisVM;
            }
        }

        private bool CanReturnCommandExecute()
        {
            return true;
        }


    }
}
