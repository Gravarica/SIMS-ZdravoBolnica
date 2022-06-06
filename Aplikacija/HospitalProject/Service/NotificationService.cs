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
    public class NotificationService
    {

        private NotificationRepository notificationRepository;
        private PrescriptionService prescriptionService;

        public NotificationService(NotificationRepository notificationRepository, PrescriptionService prescriptionService)
        {
            this.notificationRepository = notificationRepository;
            this.prescriptionService = prescriptionService;
        }
        public void Insert(Notification notification)
        {
            notificationRepository.Insert(notification);

        }

        private void SetPrescriptionForNotification(Notification notification)
        {
            notification.Prescription = prescriptionService.GetById(notification.Prescription.Id);
        }

        private void BindPrescriptionsWithNotifications(List<Notification> notifications)
        {
            notifications.ForEach(SetPrescriptionForNotification);
        }

        public List<Notification> GetAll()
        {
            var notifications = notificationRepository.GetAll();
            BindPrescriptionsWithNotifications(notifications);
            return notifications;
        }

        public List<Notification> GetNotificationsByPatient(int patientId)
        {
            var notifications = notificationRepository.GetNotificationsByPatient(patientId);
            BindPrescriptionsWithNotifications(notifications);
            return notifications;
        }

        public Notification CheckIfThereAreNotificationsForUser(Patient patient)
        {
            return GetNotificationsByPatient(patient.Id).FirstOrDefault(GetNotificationIfTimeMatches);
        }

        private int GetFrequency(Notification notification)
        {
            return 24 / notification.Prescription.Interval;
        }

        private bool GetNotificationIfTimeMatches(Notification notification)
        {
            for (int i = 0; i < GetFrequency(notification); i++)
            {
                if (TimeMatches(notification, i)) return true;
            }

            return false;
        }

        private bool TimeMatches(Notification notification, int frequency)
        {
            return DateIsInDateInterval(notification.Prescription.StartDate, notification.Prescription.EndDate) &&
                   IntervalMatches(notification, frequency);
        }

        private bool DateIsInDateInterval(DateTime startDate, DateTime endDate)
        {
            return DateTime.Now >= startDate && DateTime.Now <= endDate;
        }

        private bool IntervalMatches(Notification notification, int frequency)
        {
            return notification.StartTime.AddHours(frequency * notification.Prescription.Interval).Hour == DateTime.Now.Hour &&
                   notification.StartTime.Minute == DateTime.Now.Minute;
        }
    }

}
