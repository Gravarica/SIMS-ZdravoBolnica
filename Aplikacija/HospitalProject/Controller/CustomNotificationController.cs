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
    public class CustomNotificationController
    {

        private CustomNotificationService customNotificationService;

        public CustomNotificationController(CustomNotificationService customNotificationService)
        {
            this.customNotificationService = customNotificationService;
        }

        public CustomNotification CheckForCustomNotifications(Patient patient)
        {
            return customNotificationService.CheckIfThereAreCustomNotificationsForUser(patient);
        }

        public void Insert(CustomNotification customNotification)
        {
            customNotificationService.Insert(customNotification);
        }

        public List<CustomNotification> GetCustomNotificationsByPatient(int id)
        {
            return customNotificationService.GetCustomNotificationsByPatient(id);
        }

    }
}
