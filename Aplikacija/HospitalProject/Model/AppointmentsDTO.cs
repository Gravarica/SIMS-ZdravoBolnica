using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace HospitalProject.Model
{
    public class AppointmentsDTO : ViewModelBase
    {
        public Appointment appointment;
        public Appointment Potentialappointment;
        public TimeSpan timespan;


        public Appointment Appointment
        {
            get
            {
                return appointment;
            }
            set
            {
                appointment = value;
                OnPropertyChanged(nameof(Appointment));
            }
        }



       
        public Appointment PotentialAppointment
        {
            get
            {
                return Potentialappointment;
            }
            set
            {
                Potentialappointment = value;
                OnPropertyChanged(nameof(PotentialAppointment));
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

        public AppointmentsDTO() { }

        public AppointmentsDTO(Appointment id, Appointment Pid, TimeSpan time) {

            Appointment = id;
            PotentialAppointment = Pid;
            Timespan = time;
        }

    }
}
