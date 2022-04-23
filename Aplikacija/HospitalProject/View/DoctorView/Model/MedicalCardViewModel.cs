using HospitalProject.Core;
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

        public MedicalCardViewModel()
        {
            PatientInformationVM = new PatientInformationViewModel();

            PreviousExaminationsVM = new PreviousExaminationsViewModel();

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
