using HospitalProject.Model;
using HospitalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class AnswerController
    {
        private AnswerService answerService;

        public AnswerController(AnswerService _answerService)
        {
            answerService = _answerService;
        }

        public List<Answer> GetAll()
        {
            return answerService.GetAll().ToList();
        }

        public void Create(Answer answer)
        {
            answerService.Create(answer);
        }

        public int GetMaxId()
        {
            return answerService.GetMaxId();
        }
    }
}
