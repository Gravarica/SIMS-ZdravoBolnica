using HospitalProject.Model;
using HospitalProject.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class NotificationController
    {
        private NotificationService notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public Notification CheckForNotifications(Patient patient)
        {
            return notificationService.CheckIfThereAreNotificationsForUser(patient);
        }

        public void Insert(Notification notification)
        {
            notificationService.Insert(notification);
        }

        public List<Notification> GetNotificationsByPatient(int id)
        {
            return notificationService.GetNotificationsByPatient(id);
        }
    }
}
