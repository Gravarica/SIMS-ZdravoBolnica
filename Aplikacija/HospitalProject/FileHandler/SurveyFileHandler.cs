using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class SurveyFileHandler
{
    private string _path;

    private string _delimiter;
    

    private const string SURVEY_CSV = "-";

    //path allergy.csv
    public SurveyFileHandler(String path, String delimiter)
    {
        _path = path;
        _delimiter = delimiter;
    }

    public IEnumerable<Survey> ReadAll()
    {
        return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
               .Select(ConvertCSVFormatToSurvey)   // 1 | Polen 
               .ToList();
    }


    public Survey ConvertCSVFormatToSurvey(string CSVFormat)
    {
        var tokens = CSVFormat.Split(_delimiter.ToCharArray());
        return new Survey(int.Parse(tokens[0]),
                            tokens[1],
                           GetQuestionsFromCSV(tokens[2]));
    }

    public string ConvertSurveyToCSVFormat(Survey survey)
    {
        return string.Join(_delimiter,
        survey.Id,
        survey.Name,
        ConvertQuestionsToCSV(survey.Questions));
    }

    public void AppendLineToFile(Survey survey)
    {
        string line = ConvertSurveyToCSVFormat(survey);
        File.AppendAllText(_path, line + Environment.NewLine);
    }

    public void Save(IEnumerable<Survey> surveys)
    {
        using (StreamWriter file = new StreamWriter(_path))
        {
            foreach (Survey survey in surveys)
            {
                file.WriteLine(ConvertSurveyToCSVFormat(survey));
            }
        }
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
