using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.DataUtility;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class QuestionsFileHandler : GenericFileHandler<Question>
{
    public QuestionsFileHandler(string path) : base(path) {}

    protected override string ConvertEntityToCSV(Question question)
    {
        return string.Join(CSV_DELIMITER,
            question.Text,
            question.Category,
            question.Id);
    }

    protected override Question ConvertCSVToEntity(string csv)
    {
        var tokens = csv.Split(CSV_DELIMITER.ToCharArray());
        return new Question(tokens[0], EnumConverter.ConvertStringToCategory(tokens[1]), int.Parse(tokens[2]));
    }
}
