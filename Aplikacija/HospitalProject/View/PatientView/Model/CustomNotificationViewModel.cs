using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
    public class CustomNotificationViewModel : ViewModelBase
    {
        private Window window;
        private DateTime startDate;
        private int interval;
        private string text;
        private string time;
        private RelayCommand customNotificationCommand;
        private CustomNotificationController customNotificationController;
        private UserController userController;
        private PatientController patientController;

        public CustomNotificationViewModel(Window _window) 
        {
            var app = System.Windows.Application.Current as App;
            window = _window;
            patientController = app.PatientController;
            userController = app.UserController;
            customNotificationController = app.CustomNotificationController;

        }


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

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));

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

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));

            }

        }


        public RelayCommand CustomNotificationCommand
        {

            get
            {
                return customNotificationCommand ?? (customNotificationCommand = new RelayCommand(param => CustomNotificationCommandExecute(), param => CanCustomNotificationCommandExecute()));
            }

        }

        private bool CanCustomNotificationCommandExecute()
        {
            return true;
        }

        private void CustomNotificationCommandExecute()
        {
            int patientId = patientController.GetLoggedPatient(userController.GetLoggedUser().Username).Id;
            DateTime insertTime = SetDateTime(Time);
            CustomNotification customNotification = new CustomNotification(patientId, insertTime, Interval, Text);
            customNotificationController.Insert(customNotification);
            window.Close();
        }

        private DateTime SetDateTime(string time)
        {
            
            var timeParts = time.Split(":");

            return new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, int.Parse(timeParts[0]), int.Parse(timeParts[1]),0);

        }
    }
}
