using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class SurveyRepository
    {

        private int id;
        private List<Question> questions;
        private SurveyFileHandler surveyFileHandler;
        private List<Survey> surveys;
        private QuestionRepository questionRepository;
        private int maxId;
        

        public SurveyRepository(SurveyFileHandler _surveyFileHandler, QuestionRepository _questionRepository)
        {
            surveyFileHandler = _surveyFileHandler;
            questionRepository = _questionRepository;
            InstantiateData();
            maxId = GetMaxId();
           
        }

        public Survey GetById(int id)
        {
            return surveys.FirstOrDefault(survey => survey.Id == id);
        }

        public int GetMaxId()
        {
            return surveys.Count() == 0 ? 0 : surveys.Max(survey => survey.Id);
        }


        private void InstantiateData()
        {
            surveys = surveyFileHandler.ReadAll().ToList();

            BindQuestionsForSurvey();

        }

        private void BindQuestionsForSurvey()
        {
            surveys.ForEach(SetQuestionsForSurvey);
        }

        private void SetQuestionsForSurvey(Survey survey)
        { 
            survey.Questions.ForEach(SetQuestion);
        }

        private void SetQuestion(Question question)
        {
            question.Text = questionRepository.GetQuestionById(question.Id).Text;
        }

        public List<Survey> GetAll()
        {
            return surveys.ToList();
        }

        
        
    }
}
