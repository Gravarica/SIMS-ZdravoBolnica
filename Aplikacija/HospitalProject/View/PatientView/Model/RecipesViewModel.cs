using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.PatientView.View;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
    public class RecipesViewModel :ViewModelBase
    {

        private DateTime startDate;
        private DateTime endDate;
        private int interval;
        public ObservableCollection<Prescription> Prescriptions { get; set; }
        private Prescription selectedItem;
        private UserController _userControler;
        private AppointmentController _appointmentController;
        private PatientController _patientController;
        private Window window;

        private RelayCommand makeNotificationCommand;

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

        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
                OnPropertyChanged(nameof(Interval));
            }
        }
        public Prescription SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(selectedItem));
            }
        }

        public RecipesViewModel()
        {
            var app = System.Windows.Application.Current as App;

            
            string username = app.UserController.GetLoggedUser().Username;
            int id = app.PatientController.GetLoggedPatient(username).Id;
            

            Prescriptions = new ObservableCollection<Prescription>(app.PrescriptionController.GetPrescriptionsForPatient(id));

        }


        public RelayCommand MakeNotificationCommand
        {

            get
            {
                return makeNotificationCommand ?? (makeNotificationCommand = new RelayCommand(param => MakeNotificationCommandExecute(), param => CanMakeNotificationCommandExecute()));
            }

        }

        private bool CanMakeNotificationCommandExecute()
        {
            return true;
        }



        private void MakeNotificationCommandExecute()
        {
            MakeNotification view = new MakeNotification();
            view.DataContext = new MakeNotificationViewModel(selectedItem,view);
            view.ShowDialog();
        }
    }
}
