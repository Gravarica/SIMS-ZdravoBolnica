using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace HospitalProject.Model
{
    internal class EmergencyAppointments : ViewModelBase
    {
        private int appointmentId;
        private TimeSpan timespan;


        public int AppointmentId
        {
            get
            {
                return appointmentId;
            }
            set
            {
                appointmentId = value;
                OnPropertyChanged(nameof(AppointmentId));
            }
        }

        public TimeSpan Timespan
        {
            get
            {
                return timespan;
            }
            set
            {
                timespan = value;
                OnPropertyChanged(nameof(Timespan));
            }
        }

        public EmergencyAppointments() { }

        public EmergencyAppointments(int id, TimeSpan time) {

            AppointmentId = id;
            Timespan = time;
        }

    }
}
