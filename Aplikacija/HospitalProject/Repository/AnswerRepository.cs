using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class AnswerRepository
    {

        private int id;
        private List<Answer> answers;
        private IHandleData<Answer> answerFileHandler;
        private int maxId;

        public AnswerRepository()
        {
            answerFileHandler = new AnswerFileHandler(FilePathStorage.ANSWERS_FILE);
            InstantiateData();
            maxId = GetMaxId();

        }

        public int GetMaxId()
        {
            return answers.Count() == 0 ? 0 : answers.Max(answer => answer.Id);
        }


        private void InstantiateData()
        {
            answers = answerFileHandler.ReadAll().ToList();
        }

        public Answer GetById(int id)
        {
            return answers.FirstOrDefault(x => x.Id == id);
        }

        public List<Answer> GetAll()
        {
            return answerFileHandler.ReadAll().ToList();
        }

        public void Create(Answer answer)
        {
            answer.Id = ++maxId;
            answerFileHandler.SaveOneEntity(answer);
        }

       
    }
}
