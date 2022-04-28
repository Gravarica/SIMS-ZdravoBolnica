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
using HospitalProject.FileHandler;
using HospitalProject.Repository;

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
        private string ROOM_FILE = _projectPath + "\\Resources\\Data\\rooms.csv";
        private string ANAMNESIS_FILE = _projectPath + "\\Resources\\Data\\anamneses.csv";
        private string MEDICALRECORD_FILE = _projectPath + "\\Resources\\Data\\medicalrecords.csv";
        private string USER_FILE = _projectPath + "\\Resources\\Data\\users.csv";
        private const string CSV_DELIMITER = "|";
        private const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";

        public DoctorController DoctorController { get; set; }

        public PatientController PatientController { get; set; }

        public AppointmentController AppointmentController { get; set; }

        public RoomControoler RoomController { get; set; }

        public AppointmentController AppointmentControllerPatient { get; set; }

        public AnamnesisController AnamnesisController { get; set; }

        public MedicalRecordController MedicalRecordController { get; set; }    

        public UserController UserController { get; set; }

        public App()
        {
            var _appointmentFileHandler = new AppointmentFileHandler(APPOINTMENT_FILE, CSV_DELIMITER, DATETIME_FORMAT);

            var _doctorFileHandler = new DoctorFileHandler(DOCTOR_FILE, CSV_DELIMITER);

            var _anamnesisFileHandler = new AnamnesisFileHandler(ANAMNESIS_FILE, CSV_DELIMITER);

            var _medicalRecordFileHandler = new MedicalRecordFileHandler(MEDICALRECORD_FILE, CSV_DELIMITER);

            var _userFileHandler = new UserFileHandler(USER_FILE, CSV_DELIMITER);

            var _appointmentRepository = new AppointmentRepository(_appointmentFileHandler);

            var _appointmentRepository_patient = new AppointmentRepository(_appointmentFileHandler);

            var _doctorRepository = new DoctorRepository(_doctorFileHandler);
            
            var _roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);

            var _patientRepository = new PatientRepository(PATIENT_FILE, CSV_DELIMITER);

            var _anamnesisRepository = new AnamnesisRepository(_anamnesisFileHandler, _appointmentRepository);

            var _medicalRecordRepository = new MedicalRecordRepository(_medicalRecordFileHandler);

            var _userRepository = new UserRepository(_userFileHandler);

            var _doctorService = new DoctorService(_doctorRepository);
            
            var _roomService = new RoomService(_roomRepository);

            var _patientService = new PatientService(_patientRepository);

            var _appointmentService = new AppointmentService(_appointmentRepository, _patientService, _doctorService, _roomService);

            var _anamnesisService = new AnamnesisService(_anamnesisRepository, _appointmentService);

            var _appointment_patient_Service = new AppointmentService(_appointmentRepository_patient, _patientService, _doctorService, _roomService);

            var _medicalRecordService = new MedicalRecordService(_anamnesisService, _medicalRecordRepository, _patientService);

            var _userService = new UserService(_userRepository);

            AppointmentController = new AppointmentController(_appointmentService);

            DoctorController = new DoctorController(_doctorService);

            AppointmentControllerPatient = new AppointmentController(_appointment_patient_Service);

            PatientController = new PatientController(_patientService);

            RoomController = new RoomControoler(_roomService);

            AnamnesisController = new AnamnesisController(_anamnesisService);

            MedicalRecordController = new MedicalRecordController(_medicalRecordService);

            UserController = new UserController(_userService);
        }

       
    }
}
