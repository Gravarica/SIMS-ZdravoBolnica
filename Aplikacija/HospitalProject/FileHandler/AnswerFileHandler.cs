using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class AnswerFileHandler
{
    private string _path;

    private string _delimiter;

    //path allergy.csv
    public AnswerFileHandler(String path, String delimiter)
    {
        _path = path;
        _delimiter = delimiter;
    }

    public IEnumerable<Answer> ReadAll()
    {
        return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
               .Select(ConvertCSVFormatToAnswer)   // 1 | Polen 
               .ToList();
    }


    public Answer ConvertCSVFormatToAnswer(string CSVFormat)
    {
        var tokens = CSVFormat.Split(_delimiter.ToCharArray());
        return new Answer(int.Parse(tokens[0]), 
                          int.Parse(tokens[1]),
                          int.Parse(tokens[2]));
    }

    public string ConvertAnswerToCSVFormat(Answer answer)
    {
        return string.Join(_delimiter,
                            answer.Id,
                            answer.Rating,
                            answer.QuestionId);
    }

    public void AppendLineToFile(Answer answer)
    {
        string line = ConvertAnswerToCSVFormat(answer);
        File.AppendAllText(_path, line + Environment.NewLine);
    }

    public void Save(IEnumerable<Answer> answers)
    {
        using (StreamWriter file = new StreamWriter(_path))
        {
            foreach (Answer answer in answers)
            {
                file.WriteLine(ConvertAnswerToCSVFormat(answer));
            }
        }
    }





    
    }



