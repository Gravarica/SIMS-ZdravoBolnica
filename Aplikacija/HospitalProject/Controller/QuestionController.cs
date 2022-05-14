using HospitalProject.Model;
using HospitalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class QuestionController
    {

        private QuestionService questionService;

        public QuestionController(QuestionService _questionService)
        {
            questionService = _questionService;
        }

        public List<Question> ReadAll()
        {
            return questionService.ReadAll();

        }

        public List<Question> GetQuestionsByCategory(string category)
        {
            return questionService.GetQuestionsByCategory(category);
        }

        public int GetMaxId()
        {
            return questionService.GetMaxId();
        }
    }
}
