using HospitalProject.Core;
using HospitalProject.View.DoctorView.Model;
using HospitalProject.View.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class MainViewModel : ViewModelBase
    {

        public RelayCommand AppointmentsViewCommand { get; set; }
        
        public RelayCommand PatientsViewCommand { get; set; }

        public MainDoctorViewModel AppVM { get; set; }

        public PatientsViewModel PatientsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get 
            { 
                return _currentView; 
            }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            AppVM = new MainDoctorViewModel();
            PatientsVM = new PatientsViewModel();
            CurrentView = AppVM;

            AppointmentsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AppVM;
            });

            PatientsViewCommand = new RelayCommand(o =>
            {
                CurrentView = PatientsVM;
            });
        }


    }
}
