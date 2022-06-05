using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class SurveyFileHandler : GenericFileHandler<Survey>
{
    private const string SURVEY_CSV = "-";

    public SurveyFileHandler(string path) : base(path) {}

    protected override string ConvertEntityToCSV(Survey survey)
    {
        return string.Join(CSV_DELIMITER,
            survey.Id,
            survey.Name,
            ConvertQuestionsToCSV(survey.Questions));
    }

    protected override Survey ConvertCSVToEntity(string csv)
    {
        var tokens = csv.Split(CSV_DELIMITER.ToCharArray());
        return new Survey(int.Parse(tokens[0]),
            tokens[1],
            GetQuestionsFromCSV(tokens[2]));
    }

    private string ConvertQuestionsToCSV(List<Question> questions)
    {
        if (questions.Count == 0)
        {
            return null;
        }

        string CSVOutput = questions.ElementAt(0).Id.ToString();

        for (int i = 1; i < questions.Count; i++)
        {
            CSVOutput = SURVEY_CSV + questions.ElementAt(i).Id.ToString();
        }

        return CSVOutput;
    }

    private List<Question> GetQuestionsFromCSV(string CSVToken)
    {
        string [] questionNames = CSVToken.Split(SURVEY_CSV);
        List<Question> questions = new List<Question>();
        int lenght = questionNames.Length;

        for (int i = 0; i < lenght; i++)
        {
            AddQuestionToList(questions, int.Parse(questionNames[i]));
        }

        return questions;
    }

    private void AddQuestionToList(List <Question> questions, int _id)
    {
        Question question = new Question(_id);
        questions.Add(question);
    }
}
