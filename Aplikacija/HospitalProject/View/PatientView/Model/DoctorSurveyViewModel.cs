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
        private RelayCommand star1IsClickedCommand;
        private RelayCommand star2IsClickedCommand;
        private RelayCommand star3IsClickedCommand;
        private RelayCommand star4IsClickedCommand;
        private RelayCommand star5IsClickedCommand;
        private RelayCommand star6IsClickedCommand;
        private RelayCommand star7IsClickedCommand;
        private RelayCommand star8IsClickedCommand;
        private RelayCommand star9IsClickedCommand;
        private RelayCommand star10IsClickedCommand;
        private RelayCommand star11IsClickedCommand;
        private RelayCommand star12IsClickedCommand;
        private RelayCommand star13IsClickedCommand;
        private RelayCommand star14IsClickedCommand;
        private RelayCommand star15IsClickedCommand;
        private string star1;
        private string star2;
        private string star3;
        private string star4;
        private string star5;
        private string star6;
        private string star7;
        private string star8;
        private string star9;
        private string star10;
        private string star11;
        private string star12;
        private string star13;
        private string star14;
        private string star15;



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

        public string Star1
        {
            get
            {
                return star1;
            }
            set
            {
                star1 = value;
                OnPropertyChanged(nameof(Star1));
            }
        }
        public string Star2
        {
            get
            {
                return star2;
            }
            set
            {
                star2 = value;
                OnPropertyChanged(nameof(Star2));
            }
        }
        public string Star3
        {
            get
            {
                return star3;
            }
            set
            {
                star3 = value;
                OnPropertyChanged(nameof(Star3));
            }
        }
        public string Star4
        {
            get
            {
                return star4;
            }
            set
            {
                star4 = value;
                OnPropertyChanged(nameof(Star4));
            }
        }
        public string Star5
        {
            get
            {
                return star5;
            }
            set
            {
                star5 = value;
                OnPropertyChanged(nameof(Star5));
            }
        }

        public string Star6
        {
            get
            {
                return star6;
            }
            set
            {
                star6 = value;
                OnPropertyChanged(nameof(Star6));
            }
        }
        public string Star7
        {
            get
            {
                return star7;
            }
            set
            {
                star7 = value;
                OnPropertyChanged(nameof(Star7));
            }
        }
        public string Star8
        {
            get
            {
                return star8;
            }
            set
            {
                star8 = value;
                OnPropertyChanged(nameof(Star8));
            }
        }
        public string Star9
        {
            get
            {
                return star9;
            }
            set
            {
                star9 = value;
                OnPropertyChanged(nameof(Star9));
            }
        }
        public string Star10
        {
            get
            {
                return star10;
            }
            set
            {
                star10 = value;
                OnPropertyChanged(nameof(Star10));
            }
        }

        public string Star11
        {
            get
            {
                return star11;
            }
            set
            {
                star11 = value;
                OnPropertyChanged(nameof(Star11));
            }
        }
        public string Star12
        {
            get
            {
                return star12;
            }
            set
            {
                star12 = value;
                OnPropertyChanged(nameof(Star12));
            }
        }
        public string Star13
        {
            get
            {
                return star13;
            }
            set
            {
                star13 = value;
                OnPropertyChanged(nameof(Star13));
            }
        }
        public string Star14
        {
            get
            {
                return star14;
            }
            set
            {
                star14 = value;
                OnPropertyChanged(nameof(Star14));
            }
        }
        public string Star15
        {
            get
            {
                return star15;
            }
            set
            {
                star15 = value;
                OnPropertyChanged(nameof(Star15));
            }
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

        public RelayCommand Star1IsClickedCommand
        {

            get
            {
                return star1IsClickedCommand ?? (star1IsClickedCommand = new RelayCommand(param => Star1IsClickedCommandExecute(), param => CanStar1IsClickedCommandExecute()));
            }

        }

        private bool CanStar1IsClickedCommandExecute()
        {
            return true;
        }

        private void Star1IsClickedCommandExecute()
        {
            Star1 = "#FFFF00";
            Star2 = "#C0C0C0";
            Star3 = "#C0C0C0";
            Star4 = "#C0C0C0";
            Star5 = "#C0C0C0";
            Answer1 = "1";
        }

        public RelayCommand Star2IsClickedCommand
        {

            get
            {
                return star2IsClickedCommand ?? (star2IsClickedCommand = new RelayCommand(param => Star2IsClickedCommandExecute(), param => CanStar2IsClickedCommandExecute()));
            }

        }

        private bool CanStar2IsClickedCommandExecute()
        {
            return true;
        }

        private void Star2IsClickedCommandExecute()
        {
            Star1 = "#FFFF00";
            Star2 = "#FFFF00";
            Star3 = "#C0C0C0";
            Star4 = "#C0C0C0";
            Star5 = "#C0C0C0";
            Answer1 = "2";
        }

        public RelayCommand Star3IsClickedCommand
        {

            get
            {
                return star3IsClickedCommand ?? (star3IsClickedCommand = new RelayCommand(param => Star3IsClickedCommandExecute(), param => CanStar3IsClickedCommandExecute()));
            }

        }

        private bool CanStar3IsClickedCommandExecute()
        {
            return true;
        }

        private void Star3IsClickedCommandExecute()
        {
            Star1 = "#FFFF00";
            Star2 = "#FFFF00";
            Star3 = "#FFFF00";
            Star4 = "#C0C0C0";
            Star5 = "#C0C0C0";
            Answer1 = "3";
        }

        public RelayCommand Star4IsClickedCommand
        {

            get
            {
                return star4IsClickedCommand ?? (star4IsClickedCommand = new RelayCommand(param => Star4IsClickedCommandExecute(), param => CanStar4IsClickedCommandExecute()));
            }

        }

        private bool CanStar4IsClickedCommandExecute()
        {
            return true;
        }

        private void Star4IsClickedCommandExecute()
        {
            Star1 = "#FFFF00";
            Star2 = "#FFFF00";
            Star3 = "#FFFF00";
            Star4 = "#FFFF00";
            Star5 = "#C0C0C0";
            Answer1 = "4";
        }

        public RelayCommand Star5IsClickedCommand
        {

            get
            {
                return star5IsClickedCommand ?? (star5IsClickedCommand = new RelayCommand(param => Star5IsClickedCommandExecute(), param => CanStar5IsClickedCommandExecute()));
            }

        }

        private bool CanStar5IsClickedCommandExecute()
        {
            return true;
        }

        private void Star5IsClickedCommandExecute()
        {
            Star1 = "#FFFF00";
            Star2 = "#FFFF00";
            Star3 = "#FFFF00";
            Star4 = "#FFFF00";
            Star5 = "#FFFF00";
            Answer1 = "5";
        }


        public RelayCommand Star6IsClickedCommand
        {

            get
            {
                return star6IsClickedCommand ?? (star6IsClickedCommand = new RelayCommand(param => Star6IsClickedCommandExecute(), param => CanStar6IsClickedCommandExecute()));
            }

        }

        private bool CanStar6IsClickedCommandExecute()
        {
            return true;
        }

        private void Star6IsClickedCommandExecute()
        {
            Star6 = "#FFFF00";
            Star7 = "#C0C0C0";
            Star8 = "#C0C0C0";
            Star9 = "#C0C0C0";
            Star10 = "#C0C0C0";
            Answer2 = "1";
        }

        public RelayCommand Star7IsClickedCommand
        {

            get
            {
                return star7IsClickedCommand ?? (star7IsClickedCommand = new RelayCommand(param => Star7IsClickedCommandExecute(), param => CanStar7IsClickedCommandExecute()));
            }

        }

        private bool CanStar7IsClickedCommandExecute()
        {
            return true;
        }

        private void Star7IsClickedCommandExecute()
        {
            Star6 = "#FFFF00";
            Star7 = "#FFFF00";
            Star8 = "#C0C0C0";
            Star9 = "#C0C0C0";
            Star10 = "#C0C0C0";
            Answer2 = "2";
        }

        public RelayCommand Star8IsClickedCommand
        {

            get
            {
                return star8IsClickedCommand ?? (star8IsClickedCommand = new RelayCommand(param => Star8IsClickedCommandExecute(), param => CanStar8IsClickedCommandExecute()));
            }

        }

        private bool CanStar8IsClickedCommandExecute()
        {
            return true;
        }

        private void Star8IsClickedCommandExecute()
        {
            Star6 = "#FFFF00";
            Star7 = "#FFFF00";
            Star8 = "#FFFF00";
            Star9 = "#C0C0C0";
            Star10 = "#C0C0C0";
            Answer2 = "3";
        }

        public RelayCommand Star9IsClickedCommand
        {

            get
            {
                return star9IsClickedCommand ?? (star9IsClickedCommand = new RelayCommand(param => Star9IsClickedCommandExecute(), param => CanStar9IsClickedCommandExecute()));
            }

        }

        private bool CanStar9IsClickedCommandExecute()
        {
            return true;
        }

        private void Star9IsClickedCommandExecute()
        {
            Star6 = "#FFFF00";
            Star7 = "#FFFF00";
            Star8 = "#FFFF00";
            Star9 = "#FFFF00";
            Star10 = "#C0C0C0";
            Answer2 = "4";
        }

        public RelayCommand Star10IsClickedCommand
        {

            get
            {
                return star10IsClickedCommand ?? (star10IsClickedCommand = new RelayCommand(param => Star10IsClickedCommandExecute(), param => CanStar10IsClickedCommandExecute()));
            }

        }

        private bool CanStar10IsClickedCommandExecute()
        {
            return true;
        }

        private void Star10IsClickedCommandExecute()
        {
            Star6 = "#FFFF00";
            Star7 = "#FFFF00";
            Star8 = "#FFFF00";
            Star9 = "#FFFF00";
            Star10 = "#FFFF00";
            Answer2 = "5";
        }
        //

        public RelayCommand Star11IsClickedCommand
        {

            get
            {
                return star11IsClickedCommand ?? (star11IsClickedCommand = new RelayCommand(param => Star11IsClickedCommandExecute(), param => CanStar11IsClickedCommandExecute()));
            }

        }

        private bool CanStar11IsClickedCommandExecute()
        {
            return true;
        }

        private void Star11IsClickedCommandExecute()
        {
            Star11 = "#FFFF00";
            Star12 = "#C0C0C0";
            Star13 = "#C0C0C0";
            Star14 = "#C0C0C0";
            Star15 = "#C0C0C0";
            Answer3 = "1";
        }

        public RelayCommand Star12IsClickedCommand
        {

            get
            {
                return star12IsClickedCommand ?? (star12IsClickedCommand = new RelayCommand(param => Star12IsClickedCommandExecute(), param => CanStar12IsClickedCommandExecute()));
            }

        }

        private bool CanStar12IsClickedCommandExecute()
        {
            return true;
        }

        private void Star12IsClickedCommandExecute()
        {
            Star11 = "#FFFF00";
            Star12 = "#FFFF00";
            Star13 = "#C0C0C0";
            Star14 = "#C0C0C0";
            Star15 = "#C0C0C0";
            Answer3 = "2";
        }

        public RelayCommand Star13IsClickedCommand
        {

            get
            {
                return star13IsClickedCommand ?? (star13IsClickedCommand = new RelayCommand(param => Star13IsClickedCommandExecute(), param => CanStar13IsClickedCommandExecute()));
            }

        }

        private bool CanStar13IsClickedCommandExecute()
        {
            return true;
        }

        private void Star13IsClickedCommandExecute()
        {
            Star11 = "#FFFF00";
            Star12 = "#FFFF00";
            Star13 = "#FFFF00";
            Star14 = "#C0C0C0";
            Star15 = "#C0C0C0";
            Answer3 = "3";
        }

        public RelayCommand Star14IsClickedCommand
        {

            get
            {
                return star14IsClickedCommand ?? (star4IsClickedCommand = new RelayCommand(param => Star14IsClickedCommandExecute(), param => CanStar14IsClickedCommandExecute()));
            }

        }

        private bool CanStar14IsClickedCommandExecute()
        {
            return true;
        }

        private void Star14IsClickedCommandExecute()
        {
            Star11 = "#FFFF00";
            Star12 = "#FFFF00";
            Star13 = "#FFFF00";
            Star14 = "#FFFF00";
            Star15 = "#C0C0C0";
            Answer3 = "4";
        }

        public RelayCommand Star15IsClickedCommand
        {

            get
            {
                return star15IsClickedCommand ?? (star15IsClickedCommand = new RelayCommand(param => Star15IsClickedCommandExecute(), param => CanStar15IsClickedCommandExecute()));
            }

        }

        private bool CanStar15IsClickedCommandExecute()
        {
            return true;
        }

        private void Star15IsClickedCommandExecute()
        {
            Star11 = "#FFFF00";
            Star12 = "#FFFF00";
            Star13 = "#FFFF00";
            Star14 = "#FFFF00";
            Star15 = "#FFFF00";
            Answer3 = "5";
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
