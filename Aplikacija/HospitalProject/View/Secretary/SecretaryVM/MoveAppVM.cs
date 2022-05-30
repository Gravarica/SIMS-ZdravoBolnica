using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class MoveAppVM : BaseViewModel
    {
        public Appointment appointment;
        public Appointment Potentialappointment;
        public TimeSpan timespan;

        //private AppointmentsDTO selectedItem;

        private Patient patient;
        private Room room;
        private Specialization specialization;
        private ExaminationType examination;


       /* private RelayCommand saveCommand;
        private AppointmentController appointmentController;
        private List<AppointmentsDTO> bestAppointments;

        public MoveAppVM(List<AppointmentsDTO> best, Specialization SelectedSpecialization, Patient SelectedItem, ExaminationType SelectedExamination, Room SelectedRoom) {

            bestAppointments = best;
            var app = System.Windows.Application.Current as App;
            appointmentController = app.AppointmentController;


                examination = SelectedExamination;
                room = SelectedRoom ;
                specialization = SelectedSpecialization;
                patient = SelectedItem;
   }
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveCommandExecute(),
                                                                                     param => CanExecuteSaveCommand()));
            }
        }

        private bool CanExecuteSaveCommand()
        {
            return SelectedItem != null;
        }


        public void SaveCommandExecute()
        {
            PotentialAppointment.Doctor = Appointment.Doctor;
            PotentialAppointment.Patient = Appointment.Patient;
            PotentialAppointment.ExaminationType = Appointment.ExaminationType;

            appointmentController.Create(PotentialAppointment);


            Appointment.Patient = Patient;
            Appointment.ExaminationType = Examination;
            Appointment.Room = Room;

            appointmentController.Update(Appointment);
        }
        public AppointmentsDTO SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }*/

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

        public TimeSpan TimeSpan
        {
            get
            {
                return timespan;
            }
            set
            {
                timespan = value;
                OnPropertyChanged(nameof(TimeSpan));
            }
        }

      /*  public List<AppointmentsDTO> BestAppointments
        {
            get
            {
                return bestAppointments;
            }
            set
            {
                bestAppointments = value;
                OnPropertyChanged(nameof(BestAppointments));
            }
        }*/
        public ExaminationType Examination
        {
            get
            {
                return examination;
            }
            set
            {
                examination = value;
                OnPropertyChanged(nameof(Examination));
            }
        }

        public Specialization Specialization
        {
            get
            {
                return specialization;
            }
            set
            {
                specialization = value;
                OnPropertyChanged(nameof(Specialization));
            }
        }

        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
                OnPropertyChanged(nameof(Room));
            }
        }



        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }



    }
}
