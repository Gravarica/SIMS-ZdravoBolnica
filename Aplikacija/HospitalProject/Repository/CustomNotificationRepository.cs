using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class CustomNotificationRepository
    {
        private IHandleData<CustomNotification> customNotificationFileHandler;
        private List<CustomNotification> customNotifications;
        private int customNotificationMaxId;

        public CustomNotificationRepository()
        {
            this.customNotificationFileHandler = new CustomNotificationFileHandler(FilePathStorage.CUSTOM_NOTIFICATION_FILE);
            InstantiateCustomNotificationList();
        }

        private int GetMaxId()
        {
            return customNotifications.Count() == 0 ? 0 : customNotifications.Max(customNotification => customNotification.Id);
        }


        private void InstantiateCustomNotificationList()
        {
            customNotifications = customNotificationFileHandler.ReadAll().ToList();
            customNotificationMaxId = GetMaxId();
        }

        public List<CustomNotification> GetAll()
        {
            return customNotifications;
        }

        public List<CustomNotification> GetCustomNotificationsByPatient(int patientId)
        {
            return customNotifications.Where(customNotification => customNotification.PatientId == patientId).ToList();
        }

        public void Insert(CustomNotification customNotification)
        {
            customNotification.Id = ++customNotificationMaxId;
            customNotifications.Add(customNotification);
            customNotificationFileHandler.SaveOneEntity(customNotification);
        }



    }
}
