using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
    public class DoctorSurveyViewModel : ViewModelBase
    {
        private List<Question> questions;
        private Window window;
        private QuestionController questionController;
        private RelayCommand rateCommand;
        private SurveyController surveyController;
        private AnswerController answerController;
        private List<String> answers = new List<String> { "", "", "" };
        private Survey survey;
        private PatientController patientController;
        private UserController userController;
        private List<Answer> _answers = new List<Answer>();
        private SurveyRealizationController surveyRealizationController;
        private Doctor doctor;
        private DateTime date;



        public DoctorSurveyViewModel(Window _window, Anamnesis anamnesis)
        {
            window = _window;
            InitializeControllers();
            doctor = anamnesis.App.Doctor;
            


        }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;

            surveyController = app.SurveyController;
            answerController = app.AnswerController;
            patientController = app.PatientController;
            userController = app.UserController;
            surveyRealizationController = app.SurveyRealizationController;
            survey = surveyController.GetAll().ToList()[1];
            questions = survey.Questions;




        }


        public Question Question1
        {
            get
            {
                return questions[0];
            }

        }

        public Question Question2
        {
            get
            {
                return questions[1];
            }

        }

        public Question Question3
        {
            get
            {
                return questions[2];
            }

        }
        public string Answer1
        {
            get
            {
                return answers[0];
            }
            set
            {
                answers[0] = value;
                OnPropertyChanged(nameof(Answer1));
            }
        }

        public string Answer2
        {
            get
            {
                return answers[1];
            }
            set
            {
                answers[1] = value;
                OnPropertyChanged(nameof(Answer2));
            }
        }

        public string Answer3
        {
            get
            {
                return answers[2];
            }
            set
            {
                answers[2] = value;
                OnPropertyChanged(nameof(Answer3));
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


        public RelayCommand RateCommand
        {

            get
            {
                return rateCommand ?? (rateCommand = new RelayCommand(param => RateCommandExecute(), param => CanRateCommandExecute()));
            }

        }

        private bool CanRateCommandExecute()
        {
            return true;
        }

        private bool DoesSurveyRealizationExists()
        {
            List<SurveyRealization> _surveyRealizations = surveyRealizationController.GetAll().ToList();
            foreach (SurveyRealization surveyRealization in _surveyRealizations)
            {
                if (surveyRealization.Survey.Id == survey.Id && surveyRealization.Patient == patientController.GetLoggedPatient(userController.GetLoggedUser().Username)) ;
                return false;
            }
            return true;
        }

        private void RateCommandExecute()
        {
            Answer answer1 = new Answer(Question1.Id, int.Parse(Answer1));
            answerController.Create(answer1);
            Answer answer2 = new Answer(Question2.Id, int.Parse(Answer2));
            answerController.Create(answer2);
            Answer answer3 = new Answer(Question3.Id, int.Parse(Answer3));
            answerController.Create(answer3);
            _answers.Add(answer1);
            _answers.Add(answer2);
            _answers.Add(answer3);
            

            SurveyRealization _surveyRealization = new SurveyRealization(patientController.GetLoggedPatient(userController.GetLoggedUser().Username), survey, _answers, doctor);
            surveyRealizationController.Create(_surveyRealization);
            window.Close();
        }
    }
}
