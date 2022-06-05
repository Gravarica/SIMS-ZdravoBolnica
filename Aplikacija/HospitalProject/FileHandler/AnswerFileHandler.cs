using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class AnswerFileHandler : GenericFileHandler<Answer>
{
    
    public AnswerFileHandler(string path) : base(path) {}

    protected override Answer ConvertCSVToEntity(string csv)
    {
        var tokens = csv.Split(CSV_DELIMITER.ToCharArray());
        return new Answer(int.Parse(tokens[0]),
            int.Parse(tokens[1]),
            int.Parse(tokens[2]));
    }

    protected override string ConvertEntityToCSV(Answer answer)
    {
        return string.Join(CSV_DELIMITER,
            answer.Id,
            answer.Rating,
            answer.QuestionId);
    }
}



