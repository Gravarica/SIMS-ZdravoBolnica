using HospitalProject.Model;
using HospitalProject.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class CustomNotificationService
    {
        private CustomNotificationRepository customNotificationRepository;
        

        public CustomNotificationService(CustomNotificationRepository customNotificationRepository)
        {
            this.customNotificationRepository = customNotificationRepository;
            
        }
        public void Insert(CustomNotification customNotification)
        {
            customNotificationRepository.Insert(customNotification);

        }


        public List<CustomNotification> GetAll()
        {
            var customNotifications = customNotificationRepository.GetAll();
            return customNotifications;
        }

        public List<CustomNotification> GetCustomNotificationsByPatient(int patientId)
        {
            var notifications = customNotificationRepository.GetCustomNotificationsByPatient(patientId);
            return notifications;
        }

        public CustomNotification CheckIfThereAreCustomNotificationsForUser(Patient patient)
        {
            foreach (CustomNotification customNotification in GetCustomNotificationsByPatient(patient.Id))
            {
                if (GetNotificationIfTimeMatches(customNotification)) return customNotification;
            }

            return null;
        }
        private int GetFrequency(CustomNotification customNotification)
        {
            return 24 / customNotification.Interval;
        }

        private bool GetNotificationIfTimeMatches(CustomNotification customNotification)
        {
            for (int i = 0; i < GetFrequency(customNotification); i++)
            {
                if (TimeMatches(customNotification, i)) return true;
            }

            return false;
        }

        private bool TimeMatches(CustomNotification customNotification, int frequency)
        {
            return DateIsInDateInterval(customNotification.StartDate) &&
                   IntervalMatches(customNotification, frequency);
        }

        private bool DateIsInDateInterval(DateTime startDate)
        {

            return DateTime.Now >= startDate;
        }

        private bool IntervalMatches(CustomNotification customNotification, int frequency)
        {
            return customNotification.StartDate.AddHours(frequency * customNotification.Interval).Hour == DateTime.Now.Hour &&
                   customNotification.StartDate.Minute == DateTime.Now.Minute;
        }

    }
}
