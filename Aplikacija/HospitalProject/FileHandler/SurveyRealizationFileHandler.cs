using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;
using Model;

namespace HospitalProject.FileHandler;

public class SurveyRealizationFileHandler : GenericFileHandler<SurveyRealization>
{
    private const string SURVEY_CSV = "-";

    public SurveyRealizationFileHandler(string path) : base(path) {}

    protected override string ConvertEntityToCSV(SurveyRealization surveyRealization)
    {
        if (surveyRealization.Doctor != null)
        {
            return string.Join(CSV_DELIMITER,
                surveyRealization.Id,
                surveyRealization.Survey.Id,
                surveyRealization.Patient.Id,
                ConvertAnswersToCSV(surveyRealization.Answers),
                surveyRealization.Doctor.Id);
        }
        else
        {
            return string.Join(CSV_DELIMITER,
                surveyRealization.Id,
                surveyRealization.Survey.Id,
                surveyRealization.Patient.Id,
                ConvertAnswersToCSV(surveyRealization.Answers),
                -1);
        }
    }

    protected override SurveyRealization ConvertCSVToEntity(string csv)
    {
        var tokens = csv.Split(CSV_DELIMITER.ToCharArray());
        return new SurveyRealization(int.Parse(tokens[0]),
            int.Parse(tokens[1]),
            int.Parse(tokens[2]),
            GetAnswersFromCSV(tokens[3]),
            int.Parse(tokens[4]));
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
