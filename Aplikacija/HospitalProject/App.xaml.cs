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
        private string NOTIFICATION_FILE = _projectPath + "\\Resources\\Data\\notifications.csv";
        private string EQUIPMENT_RELOCATION_FILE = _projectPath + "\\Resources\\Data\\equipmentRelocations.csv";
        private string QUESTIONS_FILE = _projectPath + "\\Resources\\Data\\questions.csv";
        private string SURVEYS_FILE = _projectPath + "\\Resources\\Data\\surveys.csv";
        private string SURVEY_REALIZATIONS_FILE = _projectPath + "\\Resources\\Data\\surveyRealizations.csv";
        private string ANSWERS_FILE = _projectPath + "\\Resources\\Data\\answers.csv";
        private string MEDICINE_REPORT_FILE = _projectPath + "\\Resources\\Data\\medicineReports.csv";
        private string VACATION_REQUEST_FILE = _projectPath + "\\Resources\\Data\\vacationRequests.csv";

        private const string CSV_DELIMITER = "|";
        private const string DATETIME_FORMAT = "MM/dd/yyyy HH:mm";
        private const string ONLY_DATE_FORMAT = "MM/dd/yyyy";
        
        
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

        public NotificationController NotificationController { get; set; }
        
        public EquipmentRelocationController EquipmentRelocationController{ get; set; }

        public QuestionController QuestionController { get; set; }

        public SurveyController SurveyController { get; set; }

        public SurveyRealizationController SurveyRealizationController { get; set; }  
        
        public AnswerController AnswerController { get; set; }

        public MedicineReportController MedicineReportController { get; set; }  

        public VacationRequestController VacationRequestController { get; set; }    


        public App()
        {

            var _allergiesFileHandler = new AllergiesFileHandler(ALLERGIES_FILE, CSV_DELIMITER);

            var _roomRenovationFileHandler = new RoomRenovationFileHandler(ROOM_RENOVATION_FILE, CSV_DELIMITER, ONLY_DATE_FORMAT);
            
            var _equipementFileHandler = new EquipementFileHandler(EQUIPEMENT_FILE, CSV_DELIMITER);

            var _appointmentFileHandler = new AppointmentFileHandler(APPOINTMENT_FILE, CSV_DELIMITER, DATETIME_FORMAT);

            var _doctorFileHandler = new DoctorFileHandler(DOCTOR_FILE, CSV_DELIMITER);

            var _anamnesisFileHandler = new AnamnesisFileHandler(ANAMNESIS_FILE, CSV_DELIMITER);

            var _medicalRecordFileHandler = new MedicalRecordFileHandler(MEDICALRECORD_FILE, CSV_DELIMITER);

            var _userFileHandler = new UserFileHandler(USER_FILE, CSV_DELIMITER);

            var _prescriptionFileHandler = new PrescriptionFileHandler(PRESCRIPTION_FILE, CSV_DELIMITER, ONLY_DATE_FORMAT);

            var _roomRenovationRepository = new RoomRenovationRepository(_roomRenovationFileHandler);

            var _notificationFileHandler = new NotificationFileHandler(NOTIFICATION_FILE, CSV_DELIMITER);

            var _equipmentRelocationFileHandler = new EquipmentRelocationFileHandler(EQUIPMENT_RELOCATION_FILE, CSV_DELIMITER);

            var _questionsFileHandler = new QuestionsFileHandler(QUESTIONS_FILE, CSV_DELIMITER);

            var _surveyFileHandler = new SurveyFileHandler(SURVEYS_FILE, CSV_DELIMITER);

            var _surveyRealizationFileHandler = new SurveyRealizationFileHandler(SURVEY_REALIZATIONS_FILE, CSV_DELIMITER);

            var _answerFileHandler = new AnswerFileHandler(ANSWERS_FILE, CSV_DELIMITER);

            var _medicineReportFileHandler = new MedicineReportFileHandler(MEDICINE_REPORT_FILE, CSV_DELIMITER, ONLY_DATE_FORMAT);

            var _vacationRequestFileHandler = new VacationRequestFileHandler(VACATION_REQUEST_FILE, CSV_DELIMITER, ONLY_DATE_FORMAT);

            var _equipmentRelocationRepository = new EquipmentRelocationRepository(_equipmentRelocationFileHandler);
            
            var _appointmentRepository = new AppointmentRepository(_appointmentFileHandler); 

            var _appointmentRepository_patient = new AppointmentRepository(_appointmentFileHandler);

            var _patientFileHandler = new PatientFileHandler(PATIENT_FILE, CSV_DELIMITER, DATETIME_FORMAT);

            var _allergiesRepository = new AllergiesRepository(_allergiesFileHandler);

            var _equipementRepository = new EquipementRepository(_equipementFileHandler, _allergiesRepository);

            var _doctorRepository = new DoctorRepository(_doctorFileHandler);
            
            var _roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);

            var _patientRepository = new PatientRepository(_patientFileHandler);

            var _anamnesisRepository = new AnamnesisRepository(_anamnesisFileHandler);

            var _medicalRecordRepository = new MedicalRecordRepository(_medicalRecordFileHandler, _allergiesRepository);

            var _prescriptionRepository = new PrescriptionRepository(_prescriptionFileHandler, _appointmentRepository);

            var _notificationRepository = new NotificationRepository(_notificationFileHandler, _prescriptionRepository);

            var _userRepository = new UserRepository(_userFileHandler);

            var _questionRepository = new QuestionRepository(_questionsFileHandler);

            var _surveysRepository = new SurveyRepository(_surveyFileHandler,_questionRepository);

            var _answerRepository = new AnswerRepository(_answerFileHandler);

            var _surveyRealizationRepository = new SurveyRealizationRepository(_surveyRealizationFileHandler, _patientRepository, _surveysRepository, _answerRepository);

            var _medicineReportRepository = new MedicineReportRepository(_medicineReportFileHandler, _equipementRepository, _doctorRepository);

            var _vacationRequestRepository = new VacationRequestRepository(_vacationRequestFileHandler, _doctorRepository);

            var _allergiesService = new AllergiesService(_allergiesRepository);
            
            var _equipementService = new EquipementService(_equipementRepository);

            var _roomService = new RoomService(_roomRepository);
            
            var _doctorService = new DoctorService(_doctorRepository, _roomService);

            var _patientService = new PatientService(_patientRepository);

            var _appointmentService = new AppointmentService(_appointmentRepository, _patientService, _doctorService, _roomService);

            var _anamnesisService = new AnamnesisService(_anamnesisRepository, _appointmentService);

            var _medicalRecordService = new MedicalRecordService(_allergiesService, _anamnesisService, _medicalRecordRepository, _patientService);

            var _userService = new UserService(_userRepository);

            var _prescriptionService = new PrescriptionService(_prescriptionRepository, _appointmentService, _equipementService, _medicalRecordService);

            var _roomRenovationService = new RoomRenovationService(_roomRenovationRepository,_appointmentService,_roomService);

            var _equipmentRelocationService = new EquipmentRelocationService(_equipmentRelocationRepository,_roomService);

            var _questionService = new QuestionService(_questionRepository);

            var _surveyService = new SurveyService(_surveysRepository);

            var _surveyRealizationService = new SurveyRealizationService(_surveyRealizationRepository);

            var _answerService = new AnswerService(_answerRepository);

            var _medicineReportService = new MedicineReportService(_medicineReportRepository);

            var _vacationRequestService = new VacationRequestService(_vacationRequestRepository);
            
            EquipmentRelocationController = new EquipmentRelocationController(_equipmentRelocationService);
            
            AllergiesController = new AllergiesController(_allergiesService);

            RenovationController = new RoomRenovationController(_roomRenovationService);

            var _notificationService = new NotificationService(_notificationRepository, _prescriptionService);

            EquipementController = new EquipementController(_equipementService);

            AppointmentController = new AppointmentController(_appointmentService);

            DoctorController = new DoctorController(_doctorService);

            PatientController = new PatientController(_patientService,_userService, _medicalRecordService, _appointmentService);

            RoomController = new RoomControoler(_roomService);

            AnamnesisController = new AnamnesisController(_anamnesisService);

            AllergiesController = new AllergiesController(_allergiesService);

            MedicalRecordController = new MedicalRecordController(_medicalRecordService);

            UserController = new UserController(_userService);

            PrescriptionController = new PrescriptionController(_prescriptionService);

            NotificationController = new NotificationController(_notificationService);

            QuestionController = new QuestionController(_questionService);

            SurveyController = new SurveyController(_surveyService);

            SurveyRealizationController = new SurveyRealizationController(_surveyRealizationService);

            AnswerController = new AnswerController(_answerService);

            VacationRequestController = new VacationRequestController(_vacationRequestService);

            MedicineReportController = new MedicineReportController(_medicineReportService);

        }

       
    }
}
