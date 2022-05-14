using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class QuestionRepository
    {

        private List<Question> questions;
        private QuestionsFileHandler questionsFileHandler;
        private int maxId;

        public QuestionRepository(QuestionsFileHandler _questionsFileHandler)
        {
            questionsFileHandler = _questionsFileHandler;
            questions = questionsFileHandler.ReadAll().ToList();
            maxId = GetMaxId();
            
        }

        public List<Question> GetQuestionsByCategory(string category)
        {
            Category _category = questionsFileHandler.ConvertStringToCategory(category);

            return questionsFileHandler.ReadAll().Where(question => question.Category == _category).ToList();

        }

        public Question GetQuestionById(int _id)
        {
            

            return questionsFileHandler.ReadAll().FirstOrDefault(question => question.Id == _id);

        }

        public List<Question> ReadAll()
        {
            return questionsFileHandler.ReadAll().ToList();
        }

        public int GetMaxId()
        {
            return questions.Count() == 0 ? 0 : questions.Max(question => question.Id);
        }

    }
}
