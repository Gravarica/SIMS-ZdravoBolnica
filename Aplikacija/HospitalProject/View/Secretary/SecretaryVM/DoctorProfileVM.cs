using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class DoctorProfileVM : BaseViewModel
    {
        
        private Doctor _doctor;
        private AppointmentController _appointmentController;
        private PatientController _patientController;
        private DoctorController _doctorController;
        public ObservableCollection<Appointment> AppointmentItems { get; set; }

        
        public DoctorProfileVM(Doctor doctor)
        {
            _doctor = doctor;
            InstantiateControllers();
            InstantiateData();
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
        }

        private void InstantiateData()
        {
            AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAllUnifinishedAppointmentsForDoctor(_doctor.Id).ToList());
        }
        
        public Doctor Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                OnPropertyChanged(nameof(Doctor));
            }
        }
        
    }
}
