using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.Repository;
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
    public class MakeNotificationViewModel : ViewModelBase
    {

        private Prescription prescription;
        private string startTime;
        private DateOnly startDate;
        private DateOnly endDate;
        private Window window;
        
        
        private int interval;

        private RelayCommand submitCommand;

        private ObservableCollection<Prescription> prescriptionItems;

        private NotificationController notificationController;
        private UserController userController;
        private PatientController patientController;
        


        public MakeNotificationViewModel(Prescription _prescription, Window window)
        {
            
            Interval = _prescription.Interval;
            Prescription = _prescription;


            InitializeControllers();
            this.window = window;
        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;
            

            
            patientController = app.PatientController;
            notificationController = app.NotificationController;
            userController = app.UserController;
            
            
        }


        public Prescription Prescription
        {
            get
            {
                return prescription;
            }
            set
            {
                prescription = value;
                OnPropertyChanged(nameof(Prescription));
            }
        }

        public string StartTime
        {
            get
            {
                return prescription.StartTime.ToString();
            }
            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public DateOnly StartDate
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

        public DateOnly EndDate
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
                return prescription.Interval;
            }
            set
            {
                interval = value;
                OnPropertyChanged(nameof(Interval));
            }
        }

        public RelayCommand SubmitCommand
        {

            get
            {
                return submitCommand ?? (submitCommand = new RelayCommand(param => SubmitCommandExecute(), param => CanSubmitCommandExecute()));
            }

        }

        private bool CanSubmitCommandExecute()
        {
            return Prescription != null &&
                NotificationExists();
                
        }

        private bool NotificationExists()
        {
            Patient p = patientController.GetLoggedPatient(userController.GetLoggedUser().Username);
            List<Notification> notifications = new List<Notification>();
            notifications = notificationController.GetNotificationsByPatient(p.Id);
            foreach (Notification n in notifications) {

                if (n.Prescription == prescription)
                {
                    return false;
                }
                
            }
            return true;
        }

        private void SubmitCommandExecute()
        {
            Patient p = patientController.GetLoggedPatient(userController.GetLoggedUser().Username);
            //notificationController.CheckForNotifications(p);

            string[] res = startTime.Split(":");

            DateTime dt = new DateTime(StartDate.Year, startDate.Month, startDate.Day, Int32.Parse(res[0]), Int32.Parse(res[1]), 1 );

            Notification notification = new Notification(prescription.Medicine.Name, prescription, dt);

            notificationController.Insert(notification);
            window.Close();

        }
    }
}
