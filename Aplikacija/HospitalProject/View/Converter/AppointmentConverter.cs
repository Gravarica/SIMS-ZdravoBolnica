using HospitalProject.View.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.Converter
{
    public class AppointmentConverter : AbstractConverter
    {
        public static AppointmentViewModel ConvertAppointmentToAppointmentView(Appointment appointment)
            => new AppointmentViewModel
            {
                AppointmentId = appointment.Id,
                Date = appointment.Date,
                Duration = appointment.Duration,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient.FirstName + " " + appointment.Patient.LastName,
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor.FirstName + " " + appointment.Doctor.LastName
            };

        public static Appointment ConvertAppointmentViewToAppointment(AppointmentViewModel avm)
            => new Appointment
            {
                Id = avm.AppointmentId,
                Date = avm.Date,
                Duration = avm.Duration,
                PatientId = avm.PatientId,
                DoctorId = avm.DoctorId
            };


        public static IList<AppointmentViewModel> ConvertAppointmentListToAppointmentViewList(IList<Appointment> appointments)
            => ConvertEntityListToViewList(appointments, ConvertAppointmentToAppointmentView);

        public static IList<Appointment> ConvertAppointmentViewListToAppointmentList(IList<AppointmentViewModel> items)
            => ConvertViewListToEntityList(items, ConvertAppointmentViewToAppointment);
    }
}
