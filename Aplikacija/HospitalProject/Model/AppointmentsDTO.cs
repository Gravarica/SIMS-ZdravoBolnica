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
        public Doctor doctor;
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



        public Doctor DoctorData
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged(nameof(Doctor));
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

        public AppointmentsDTO(Appointment id, Doctor doctor, Appointment Pid, TimeSpan time) {

            Appointment = id;
            doctor = DoctorData;
            PotentialAppointment = Pid;
            Timespan = time;
        }

    }
}
