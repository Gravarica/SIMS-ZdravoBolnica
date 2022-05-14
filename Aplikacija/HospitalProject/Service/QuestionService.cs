using HospitalProject.Model;
using HospitalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class QuestionService
    {
        private QuestionRepository questionRepository;
        private List<Question> questions;

        public QuestionService(QuestionRepository _questionRepository)
        {
            questionRepository = _questionRepository;
            
        }

        public List<Question> ReadAll()
        { 
            return questionRepository.ReadAll().ToList();

        }

        public List<Question> GetQuestionsByCategory(string category)
        {
            return questionRepository.GetQuestionsByCategory(category).ToList();
        }

        public int GetMaxId()
        {
            return questionRepository.GetMaxId();
        }

    }
}
