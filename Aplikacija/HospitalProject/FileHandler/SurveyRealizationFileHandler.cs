using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;
using Model;

namespace HospitalProject.FileHandler;

public class SurveyRealizationFileHandler
{
    private string _path;

    private string _delimiter;
    


    private const string SURVEY_CSV = "-";

    //path allergy.csv
    public SurveyRealizationFileHandler(String path, String delimiter)
    {
        _path = path;
        _delimiter = delimiter;
    }

    public IEnumerable<SurveyRealization> ReadAll()
    {
        return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
               .Select(ConvertCSVFormatToSurveyRealization)        // 1 | Polen 
               .ToList();
    }


    public SurveyRealization ConvertCSVFormatToSurveyRealization(string CSVFormat)
    {
        var tokens = CSVFormat.Split(_delimiter.ToCharArray());
        return new SurveyRealization(int.Parse(tokens[0]),
                                     int.Parse(tokens[1]),
                                    int.Parse(tokens[2]),
                                    GetAnswersFromCSV(tokens[3]),
                                    int.Parse(tokens[4]));
    }

    public string ConvertSurveyRealizationToCSVFormat(SurveyRealization surveyRealization)
    {
        if (surveyRealization.Doctor != null)
        {
            return string.Join(_delimiter,
             surveyRealization.Id,
             surveyRealization.Survey.Id,
             surveyRealization.Patient.Id,
             ConvertAnswersToCSV(surveyRealization.Answers),
             surveyRealization.Doctor.Id);
        }
        else
        {
            return string.Join(_delimiter,
             surveyRealization.Id,
             surveyRealization.Survey.Id,
             surveyRealization.Patient.Id,
             ConvertAnswersToCSV(surveyRealization.Answers),
             -1);
        }

        
    }

    public void AppendLineToFile(SurveyRealization surveyRealization)
    {
        string line = ConvertSurveyRealizationToCSVFormat(surveyRealization);
        File.AppendAllText(_path, line + Environment.NewLine);
    }

    public void Save(IEnumerable<SurveyRealization> surveyRealizations)
    {
        using (StreamWriter file = new StreamWriter(_path))
        {
            foreach (SurveyRealization surveyRealization in surveyRealizations)
            {
                file.WriteLine(ConvertSurveyRealizationToCSVFormat(surveyRealization));
            }
        }
    }

    private string ConvertAnswersToCSV(List<Answer> answers)
    {
        if (answers.Count == 0)
        {
            return null;
        }

        string CSVOutput = answers.ElementAt(0).Id.ToString();

        for (int i = 1; i < answers.Count; i++)
        {
            CSVOutput += SURVEY_CSV + answers.ElementAt(i).Id.ToString();
        }

        return CSVOutput;
    }

    private List<Answer> GetAnswersFromCSV(string CSVToken)
    {
        string[] surveyRealizations = CSVToken.Split(SURVEY_CSV);
        List<Answer> answers = new List<Answer>();
        int lenght = surveyRealizations.Length;

        for (int i = 0; i < lenght; i++)
        {
            AddAnswerToList(answers, int.Parse(surveyRealizations[i]));
        }

        return answers;
    }

    private void AddAnswerToList(List<Answer> answers, int _id)
    {
        Answer answer = new Answer(_id);
        answers.Add(answer);
    }
}
