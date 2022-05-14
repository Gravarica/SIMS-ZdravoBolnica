using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class SurveyRealization : ViewModelBase
    {
        private Patient patient;
        private Doctor doctor;
        private Survey survey;
        private List<Answer> answers;
        private int id;

        // Konstruktor za citanje iz fajla
        public SurveyRealization(int _id, int patientId, int surveyId, List<Answer> _answers, int doctorId)
        {
            Id = _id;
            Patient = new Patient(patientId);
            Answers = _answers;
            Survey = new Survey(surveyId);
            Answers = _answers;
            if(doctorId != null)
            {
                Doctor = new Doctor(doctorId);
            }
        }
        
        // Konstruktor za front
        public SurveyRealization(Patient _patient, Survey _survey, List<Answer> _answers, Doctor _doctor)
        {
            Patient= _patient;
            Answers= _answers;
            Survey= _survey;
            Doctor= _doctor;

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

        public List<Answer> Answers
        {
            get
            {
                return answers;
            }
            set
            {
                answers = value;
                OnPropertyChanged(nameof(Answers));
            }
        }

        public Survey Survey
        {
            get
            {
                return survey;
            }
            set
            {
                survey = value;
                OnPropertyChanged(nameof(Survey));
            }
        }

        public Doctor Doctor
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

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }



    }
}
