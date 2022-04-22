using Controller;
using HospitalProject.Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Repository;
using Model;
using Service;
using HospitalProject.Service;

namespace HospitalProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string _projectPath = System.Reflection.Assembly.GetExecutingAssembly().Location
            .Split(new string[] { "bin" }, StringSplitOptions.None)[0];
        private string DOCTOR_FILE = _projectPath + "\\Resources\\Data\\doctors.csv";
        private string PATIENT_FILE = _projectPath + "\\Resources\\Data\\patients.csv";
        private string APPOINTMENT_FILE = _projectPath + "\\Resources\\Data\\appointments.csv";
        private string APPOINTMENTS_PATIENT_FILE = _projectPath + "\\Resources\\Data\\appointments_patient.csv";
        private string ROOM_FILE = _projectPath + "\\Resources\\Data\\rooms.csv";private const string CSV_DELIMITER = "|";
        private const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";

        public DoctorController DoctorController { get; set; }

        public PatientController PatientController { get; set; }

        public AppointmentController AppointmentController { get; set; }

        public RoomControoler RoomController { get; set; }

        public AppointmentController AppointmentControllerPatient { get; set; }


        public App()
        {


            var _appointmentRepository = new AppointmentRepository(APPOINTMENT_FILE, CSV_DELIMITER, DATETIME_FORMAT);

            var _appointmentRepository_patient = new AppointmentRepository(APPOINTMENTS_PATIENT_FILE, CSV_DELIMITER, DATETIME_FORMAT);

            var _doctorRepository = new DoctorRepository(DOCTOR_FILE, CSV_DELIMITER);
            
            var _roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);

            var _patientRepository = new PatientRepository(PATIENT_FILE, CSV_DELIMITER);

            var _doctorService = new DoctorService(_doctorRepository);
            
            var _roomService = new RoomService(_roomRepository);

            var _patientService = new PatientService(_patientRepository);

            var _appointmentService = new AppointmentService(_appointmentRepository, _patientService, _doctorService);

            var _appointment_patient_Service = new AppointmentService(_appointmentRepository_patient, _patientService, _doctorService);
            AppointmentController = new AppointmentController(_appointmentService);
            DoctorController = new DoctorController(_doctorService);

            AppointmentControllerPatient = new AppointmentController(_appointment_patient_Service);

            PatientController = new PatientController(_patientService);

            RoomController = new RoomControoler(_roomService);

        }

       
    }
}
