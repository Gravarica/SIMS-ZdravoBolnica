using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public static class FilePathStorage
    {
        public static string _projectPath = System.Reflection.Assembly.GetExecutingAssembly().Location
            .Split(new string[] { "bin" }, StringSplitOptions.None)[0];
        public static string DOCTOR_FILE = _projectPath + "\\Resources\\Data\\doctors.csv";
        public static string PATIENT_FILE = _projectPath + "\\Resources\\Data\\patients.csv";
        public static string APPOINTMENT_FILE = _projectPath + "\\Resources\\Data\\appointments.csv";
        public static string EQUIPEMENT_FILE = _projectPath + "\\Resources\\Data\\equipement.csv";
        public static string ROOM_RENOVATION_FILE = _projectPath + "\\Resources\\Data\\roomRenovations.csv";
        public static string ROOM_FILE = _projectPath + "\\Resources\\Data\\rooms.csv";
        public static string ANAMNESIS_FILE = _projectPath + "\\Resources\\Data\\anamneses.csv";
        public static string ALLERGIES_FILE = _projectPath + "\\Resources\\Data\\allergy.csv";
        public static string MEDICALRECORD_FILE = _projectPath + "\\Resources\\Data\\medicalrecords.csv";
        public static string USER_FILE = _projectPath + "\\Resources\\Data\\users.csv";
        public static string PRESCRIPTION_FILE = _projectPath + "\\Resources\\Data\\prescriptions.csv";
        public static string NOTIFICATION_FILE = _projectPath + "\\Resources\\Data\\notifications.csv";
        public static string EQUIPMENT_RELOCATION_FILE = _projectPath + "\\Resources\\Data\\equipmentRelocations.csv";
        public static string QUESTIONS_FILE = _projectPath + "\\Resources\\Data\\questions.csv";
        public static string SURVEYS_FILE = _projectPath + "\\Resources\\Data\\surveys.csv";
        public static string SURVEY_REALIZATIONS_FILE = _projectPath + "\\Resources\\Data\\surveyRealizations.csv";
        public static string ANSWERS_FILE = _projectPath + "\\Resources\\Data\\answers.csv";
        public static string MEDICINE_REPORT_FILE = _projectPath + "\\Resources\\Data\\medicineReports.csv";
        public static string VACATION_REQUEST_FILE = _projectPath + "\\Resources\\Data\\vacationRequests.csv";
        public static string CUSTOM_NOTIFICATION_FILE = _projectPath + "\\Resources\\Data\\customNotifications.csv";
        public static string NOTE_FILE = _projectPath + "\\Resources\\Data\\notes.csv";

    }
}
