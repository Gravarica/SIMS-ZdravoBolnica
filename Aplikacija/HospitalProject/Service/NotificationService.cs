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

        private void SetPrescriptionForNotification(Notification notification)
        {
            notification.Prescription = prescriptionService.GetById(notification.Prescription.Id);
        }

        private void BindPrescriptionsWithNotifications(List<Notification> notifications)
        {
            notifications.ForEach(notification => SetPrescriptionForNotification(notification));
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
            IEnumerable<Notification> notifications = GetNotificationsByPatient(patient.Id);
            Notification returnNotification;

            DateTime startDate;
            DateTime endDate;
            int checkCounter;

            foreach(Notification notification in notifications)
            {
                startDate = new DateTime(notification.Prescription.StartDate.Year, notification.Prescription.StartDate.Month, notification.Prescription.StartDate.Day);
                endDate = new DateTime(notification.Prescription.EndDate.Year, notification.Prescription.EndDate.Month, notification.Prescription.EndDate.Day);
                checkCounter = 24 / notification.Prescription.Interval;

                for(int i = 0; i < checkCounter; i++)
                {
                    if (DateTime.Now >= startDate && DateTime.Now <= endDate)
                    {
                        if (notification.StartTime.AddHours(i*notification.Prescription.Interval).Hour == DateTime.Now.Hour &&
                            notification.StartTime.Minute == DateTime.Now.Minute)
                        {
                            return notification;
                        }
                    }
                }
                
            }

            return null;
        }
    }
}
