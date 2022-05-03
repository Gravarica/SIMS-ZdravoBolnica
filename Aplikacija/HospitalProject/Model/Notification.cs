using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Notification : ViewModelBase
    {
        private int id;
        private string name;
        private DateTime startTime;
        private Prescription prescription;

        public Notification(int id,string name, int prescriptionId, DateTime time) {

            Id = id;
            Prescription = new Prescription();
            Prescription.Id = prescriptionId;
            StartTime = time;


        }


        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged(nameof(Id));
            }
            
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged(nameof(Name));
            }

        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                this.startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }

        }

        public Prescription Prescription
        {
            get
            {
                return prescription;
            }
            set
            {
                this.prescription = value;
                OnPropertyChanged(nameof(Prescription));
            }

        }
    }
}
