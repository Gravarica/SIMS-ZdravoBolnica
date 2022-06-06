using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class AllergiesFileHandler : GenericFileHandler<Allergies>
{

    public  AllergiesFileHandler(string path) : base(path) {}

    protected override string ConvertEntityToCSV(Allergies allergies)
    {
        return string.Join(CSV_DELIMITER,
                           allergies.Id,
                           allergies.Name);
    }

    protected override Allergies ConvertCSVToEntity(string csv)
    {
        var tokens = csv.Split(CSV_DELIMITER.ToCharArray());
        return new Allergies(int.Parse(tokens[0]), tokens[1]);
    }
}
