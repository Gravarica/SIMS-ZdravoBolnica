using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class NotificationRepository
    {
        private NotificationFileHandler notificationFileHandler;
        private PrescriptionRepository prescriptionRepository;
        private List<Notification> notifications;
        private int notificationMaxId;

        public NotificationRepository(NotificationFileHandler notificationFileHandler, PrescriptionRepository prescriptionRepository)
        {
            this.notificationFileHandler = notificationFileHandler;
            this.prescriptionRepository = prescriptionRepository;
            InstantiateNotificationList();
        }      

        private int GetMaxId()
        {
            return notifications.Count() == 0 ? 0 : notifications.Max(notification => notification.Id);
        }

        private void SetPrescriptionForNotification(Notification notification)
        {
            notification.Prescription = prescriptionRepository.GetById(notification.Prescription.Id);
        }

        private void InstantiateNotificationList()
        {
            notifications = notificationFileHandler.ReadAll().ToList();
            notifications.ForEach(notification => SetPrescriptionForNotification(notification));
            notificationMaxId = GetMaxId();
        }

        public List<Notification> GetAll()
        {
            return notifications;
        }

        public List<Notification> GetNotificationsByPatient(int patientId)
        {
            return notifications.Where(notification => notification.Prescription.Appointment.Patient.Id == patientId).ToList();
        }

        public void Insert(Notification notification)
        {
            notification.Id = ++notificationMaxId;
            notifications.Add(notification);
            notificationFileHandler.AppendLineToFile(notification);
        }
    }
}
