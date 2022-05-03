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
        private string EQUIPEMENT_FILE = _projectPath + "\\Resources\\Data\\equipement.csv";
        private string ROOM_RENOVATION_FILE = _projectPath + "\\Resources\\Data\\roomRenovations.csv";
        private string ROOM_FILE = _projectPath + "\\Resources\\Data\\rooms.csv";
        private string ANAMNESIS_FILE = _projectPath + "\\Resources\\Data\\anamneses.csv";
        private string ALLERGIES_FILE = _projectPath + "\\Resources\\Data\\allergy.csv";
        private string MEDICALRECORD_FILE = _projectPath + "\\Resources\\Data\\medicalrecords.csv";
        private string USER_FILE = _projectPath + "\\Resources\\Data\\users.csv";
        private string PRESCRIPTION_FILE = _projectPath + "\\Resources\\Data\\prescriptions.csv";
        private const string CSV_DELIMITER = "|";
        private const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";
        
        
        public AllergiesController AllergiesController { get; set; }
        public RoomRenovationController RenovationController { get; set; }

        public DoctorController DoctorController { get; set; }

        public PatientController PatientController { get; set; }

        public AppointmentController AppointmentController { get; set; }
        
        public EquipementController EquipementController { get; set; }

        public RoomControoler RoomController { get; set; }

        public AppointmentController AppointmentControllerPatient { get; set; }

        public AnamnesisController AnamnesisController { get; set; }


        public MedicalRecordController MedicalRecordController { get; set; }    

        public UserController UserController { get; set; }

        public PrescriptionController PrescriptionController { get; set; }

        public App()
        {

            var _allergiesFileHandler = new AllergiesFileHandler(ALLERGIES_FILE, CSV_DELIMITER);

            var _roomRenovationFileHandler = new RoomRenovationFileHandler(ROOM_RENOVATION_FILE, CSV_DELIMITER, DATETIME_FORMAT);
            
            var _equipementFileHandler = new EquipementFileHandler(EQUIPEMENT_FILE, CSV_DELIMITER);

            var _appointmentFileHandler = new AppointmentFileHandler(APPOINTMENT_FILE, CSV_DELIMITER, DATETIME_FORMAT);

            var _doctorFileHandler = new DoctorFileHandler(DOCTOR_FILE, CSV_DELIMITER);

            var _anamnesisFileHandler = new AnamnesisFileHandler(ANAMNESIS_FILE, CSV_DELIMITER);

            var _medicalRecordFileHandler = new MedicalRecordFileHandler(MEDICALRECORD_FILE, CSV_DELIMITER);

            var _userFileHandler = new UserFileHandler(USER_FILE, CSV_DELIMITER);

            var _prescriptionFileHandler = new PrescriptionFileHandler(PRESCRIPTION_FILE, CSV_DELIMITER);

            var _roomRenovationRepository = new RoomRenovationRepository(_roomRenovationFileHandler);

            var _patientFileHandler = new PatientFileHandler(PATIENT_FILE, CSV_DELIMITER, DATETIME_FORMAT);

            var _appointmentRepository = new AppointmentRepository(_appointmentFileHandler);

            var _allergiesRepository = new AllergiesRepository(_allergiesFileHandler);

            var _equipementRepository = new EquipementRepository(_equipementFileHandler, _allergiesRepository);

            var _doctorRepository = new DoctorRepository(_doctorFileHandler);
            
            var _roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);

            var _patientRepository = new PatientRepository(_patientFileHandler);

            var _anamnesisRepository = new AnamnesisRepository(_anamnesisFileHandler, _appointmentRepository);

            var _medicalRecordRepository = new MedicalRecordRepository(_medicalRecordFileHandler, _allergiesRepository);

            var _prescriptionRepository = new PrescriptionRepository(_prescriptionFileHandler, _appointmentRepository);

            var _userRepository = new UserRepository(_userFileHandler);

            var _allergiesService = new AllergiesService(_allergiesRepository);
            
            var _equipementService = new EquipementService(_equipementRepository);

            var _doctorService = new DoctorService(_doctorRepository);
            
            var _roomService = new RoomService(_roomRepository);

            var _patientService = new PatientService(_patientRepository);

            var _appointmentService = new AppointmentService(_appointmentRepository, _patientService, _doctorService, _roomService);

            var _anamnesisService = new AnamnesisService(_anamnesisRepository, _appointmentService);

            var _medicalRecordService = new MedicalRecordService(_allergiesService, _anamnesisService, _medicalRecordRepository, _patientService);

            var _userService = new UserService(_userRepository);

            var _prescriptionService = new PrescriptionService(_prescriptionRepository, _appointmentService, _equipementService, _medicalRecordService);

            var _roomRenovationService = new RoomRenovationService(_roomRenovationRepository,_appointmentService,_roomService);



            AllergiesController = new AllergiesController(_allergiesService);

            RenovationController = new RoomRenovationController(_roomRenovationService);

            EquipementController = new EquipementController(_equipementService);

            AppointmentController = new AppointmentController(_appointmentService);

            DoctorController = new DoctorController(_doctorService);

            PatientController = new PatientController(_patientService,_userService);

            RoomController = new RoomControoler(_roomService);

            AnamnesisController = new AnamnesisController(_anamnesisService);

            AllergiesController = new AllergiesController(_allergiesService);

            MedicalRecordController = new MedicalRecordController(_medicalRecordService);

            UserController = new UserController(_userService);

            PrescriptionController = new PrescriptionController(_prescriptionService);
        }

       
    }
}
