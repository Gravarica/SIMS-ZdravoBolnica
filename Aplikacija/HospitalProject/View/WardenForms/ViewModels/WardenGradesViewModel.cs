using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;
using Syncfusion.Data.Extensions;

namespace HospitalProject.View.WardenForms.ViewModels;

public class WardenGradesViewModel : ViewModelBase
{
    private QuestionController _questionController;
    private AnswerController _answerController;
    private SurveyRealizationController _surveyRealizationController;
    private DoctorController _doctorController;

    public Question selectedQuestion;

    public Category selectedCategory;

    private ObservableCollection<Question> questionsItems;
    public ObservableCollection<Answer> AnswersItems { get; set; }
    
    public ObservableCollection<SurveyRealization> SurveyRealisationItems { get; set; }
    
    public ObservableCollection<Doctor> DoctorItems { get; set; }

    public RelayCommand SelectedQuestionChange { get; set; }


    private Doctor selectedDocotr;
    
    public ObservableCollection<Category> Categories { get; set; }

    private int grade1;
    private int grade2;
    private int grade3;
    private int grade4;
    private int grade5;
    private double avgGrade;

    private double doctorsAvg;

    private Visibility visable = Visibility.Hidden;

    public ObservableCollection<Question> QuestionsItems
    {
        get
        {
            return questionsItems;
        }
        set
        {
            questionsItems = value;
            OnPropertyChanged(nameof(QuestionsItems));
        }
    }

    public Doctor SelectedDoctor
    {
        get
        {
            return selectedDocotr;
        }
        set
        {
            selectedDocotr = value;
            OnPropertyChanged(nameof(SelectedDoctor));
            CalculateDoctorsAvg();
        }
    }

    public Visibility Visable
    {
        get
        {
            return visable;
        }
        set
        {
            visable = value;
            OnPropertyChanged(nameof(Visable));
        }
    } 

    public int Grade1
    {
        get
        {
            return grade1;
        }
        set
        {
            grade1 = value;
            OnPropertyChanged(nameof(Grade1));
        }
    }
    
    public int Grade2
    {
        get
        {
            return grade2;
        }
        set
        {
            grade2 = value;
            OnPropertyChanged(nameof(Grade2));
        }
    }
    
    public int Grade3
    {
        get
        {
            return grade3;
        }
        set
        {
            grade3 = value;
            OnPropertyChanged(nameof(Grade3));
        }
    }
    
    public int Grade4
    {
        get
        {
            return grade4;
        }
        set
        {
            grade4 = value;
            OnPropertyChanged(nameof(Grade4));
        }
    }
    
    public int Grade5
    {
        get
        {
            return grade5;
        }
        set
        {
            grade5 = value;
            OnPropertyChanged(nameof(Grade5));
        }
    }

    public double AvgGrade
    {
        get
        {
            return Math.Round(avgGrade,2);
        }
        set
        {
            avgGrade = value;
            OnPropertyChanged(nameof(AvgGrade));
        }
    }
    
    public double DoctorsAvg
    {
        get
        {
            return Math.Round(doctorsAvg,2);
        }
        set
        {
            doctorsAvg = value;
            OnPropertyChanged(nameof(DoctorsAvg));
        }
    }

    public Category SelectedCategory
    {
        get
        {
            return selectedCategory;
        }
        set
        {
            selectedCategory = value;
            OnPropertyChanged(nameof(SelectedCategory));
            SetVisibility();
            SetQuestionCategory();
        }
    }

    private void SetQuestionCategory()
    {
        QuestionsItems = _questionController.GetQuestionsByType(SelectedCategory).ToObservableCollection();
    }

    private void SetVisibility()
    {
        if (selectedCategory == Category.DOCTOR_SURVEY)
        {
            Visable = Visibility.Visible;
        }
        else
        {
            Visable = Visibility.Hidden;
        }
    }
    public Question SelectedQuestion
    {
        get
        {
            return selectedQuestion;
        }
        set
        {
            selectedQuestion = value;
            OnPropertyChanged(nameof(SelectedQuestion));
            SetGrades();
        }
    }

    private void SetGrades()
    {
        Grade1 = CountGrades(SelectedQuestion, 1);
        Grade2 = CountGrades(SelectedQuestion, 2);
        Grade3 = CountGrades(SelectedQuestion, 3);
        Grade4 = CountGrades(SelectedQuestion, 4);
        Grade5 = CountGrades(SelectedQuestion, 5);
        AvgGrade = CalculateAvg(SelectedQuestion);
    }

    public WardenGradesViewModel()
    {
        InstantiateControllers();
        InstantiateCategorys();
        InstantiateData();
    }

    private void InstantiateCategorys()
    {
        Categories = new ObservableCollection<Category>();
        Categories.Add(Category.DOCTOR_SURVEY);
        Categories.Add(Category.HOSPITAL_SURVEY);
        Categories.Add(Category.APPLICATION_SURVEY);
    }

    private void InstantiateControllers()
    {
        var app = System.Windows.Application.Current as App;
        _answerController = app.AnswerController;
        _questionController = app.QuestionController;
        _surveyRealizationController = app.SurveyRealizationController;
        _doctorController = app.DoctorController;
    }

    private void InstantiateData()
    {
        QuestionsItems = new ObservableCollection<Question>();
        AnswersItems = new ObservableCollection<Answer>(_answerController.GetAll().ToList());
        SelectedQuestionChange = new RelayCommand( parm=> ExecuteSelectedQuestionChangeCommand(), param => true);
        SurveyRealisationItems =
            new ObservableCollection<SurveyRealization>(_surveyRealizationController.GetAll().ToList());
        DoctorItems = new ObservableCollection<Doctor>(_doctorController.GetAll().ToList());
    }

    private void ExecuteSelectedQuestionChangeCommand()
    {
        Grade1 = CountGrades(SelectedQuestion, 1);
        AvgGrade = CalculateAvg(SelectedQuestion);
    }

    private int CountGrades(Question question, int grade)
    {
        int number = 0;
        if (question == null)
        {
            return 0;
        }
        else if (SelectedCategory == Category.DOCTOR_SURVEY)
        {
            foreach (SurveyRealization surveyRealisation in SurveyRealisationItems)
            {
                if (SelectedDoctor.Id == surveyRealisation.Doctor.Id)
                {
                    foreach (Answer answer in surveyRealisation.Answers)
                    {
                        Answer ans = _answerController.GetById(answer.Id);
                        if (ans.QuestionId == SelectedQuestion.Id)
                        {
                            if (grade == ans.Rating)
                            {
                                number = number + 1;
                            }
                        }
                    }
                }
            }
            return number;
        }
        else
        {

            foreach (Answer ans in AnswersItems)
            {
                if (question.Id == ans.QuestionId)
                {
                    if (grade == ans.Rating)
                    {
                        number = number + 1;
                    }
                }
            }

            return number;
        }
    }


    private void CalculateDoctorsAvg()
    {
        if (SelectedCategory == Category.DOCTOR_SURVEY && SelectedDoctor != null)
        {
            double number = 0;
            double grade = 0;
            foreach (SurveyRealization srr in SurveyRealisationItems)
            {
                if (srr.Doctor.Id == SelectedDoctor.Id)
                {
                    foreach (Answer answer in srr.Answers)
                    {
                        Answer ans = _answerController.GetById(answer.Id);

                        number = number + 1;
                        grade = grade + ans.Rating;
                    }
                }
            }

            DoctorsAvg = grade / number;
        }
    }

    private double CalculateAvg(Question question)
    {
        if (question == null)
        {
            return 0;
        }

        double number = 0;
        double grade = 0;
        
        if (SelectedCategory == Category.DOCTOR_SURVEY)
        {
            foreach (SurveyRealization srr in SurveyRealisationItems)
            {
                if (srr.Doctor.Id == SelectedDoctor.Id)
                {
                    foreach (Answer answer in srr.Answers)
                    {
                        Answer ans = _answerController.GetById(answer.Id);
                        if (ans.QuestionId == SelectedQuestion.Id)
                        {
                            number = number + 1 ;
                            grade = grade + ans.Rating;
                        }
                    }
                }
            }
            return grade / number;
        }
        
        foreach (Answer answer in AnswersItems)
        { 
            if (answer.QuestionId == question.Id)
            { 
                number = number + 1 ;
                grade = grade + answer.Rating;
            }
        }

        return grade / number;
        
        
    }

}