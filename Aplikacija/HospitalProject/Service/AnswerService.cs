using HospitalProject.Model;
using HospitalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class AnswerService
    {
        private List<Answer> answers;
        private AnswerRepository answerRepository;

        public AnswerService(AnswerRepository _answerRepository)
        {
            answerRepository = _answerRepository;
        }

        public List<Answer> GetAll()
        {
            return answerRepository.GetAll().ToList();
        }

        public void Create(Answer answer)
        {
            answerRepository.Create(answer);
        }

        public int GetMaxId()
        {
            return answerRepository.GetMaxId();
        }

    }
}
