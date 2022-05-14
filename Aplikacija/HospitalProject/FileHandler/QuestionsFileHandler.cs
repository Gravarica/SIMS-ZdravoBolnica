using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class QuestionsFileHandler
{
    private string _path;

    private string _delimiter;

    //path allergy.csv
    public QuestionsFileHandler(String path, String delimiter)
    {
        _path = path;
        _delimiter = delimiter;
    }

    public IEnumerable<Question> ReadAll()
    {
        return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
               .Select(ConvertCSVFormatToQuestion)   // 1 | Polen 
               .ToList();
    }


    public Question ConvertCSVFormatToQuestion(string CSVFormat)
    {
        var tokens = CSVFormat.Split(_delimiter.ToCharArray());
        return new Question(tokens[0], ConvertStringToCategory(tokens[1]), int.Parse(tokens[2]));
    }

    public string ConvertQuestionToCSVFormat(Question question)
    {
        return string.Join(_delimiter,
        question.Text,
        question.Category,
        question.Id);
    }

    public void AppendLineToFile(Question question)
    {
        string line = ConvertQuestionToCSVFormat(question);
        File.AppendAllText(_path, line + Environment.NewLine);
    }

    public void Save(IEnumerable<Question> questions)
    {
        using (StreamWriter file = new StreamWriter(_path))
        {
            foreach (Question question in questions)
            {
                file.WriteLine(ConvertQuestionToCSVFormat(question));
            }
        }
    }

    

    public Category ConvertStringToCategory(string _category) 
    {
        if (_category.Equals("DOCTOR_SURVEY"))
        {
            return Category.DOCTOR_SURVEY;
        }
        else if (_category.Equals("HOSPITAL_SURVEY"))
        {
            return Category.HOSPITAL_SURVEY;
        }
        else if (_category.Equals("APPLICATION_SURVEY"));
        {
            return Category.APPLICATION_SURVEY;
        }

        
    }


}
