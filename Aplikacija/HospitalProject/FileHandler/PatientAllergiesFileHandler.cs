using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class PatientAllergiesFileHandler
{
    private readonly string _path;

    private readonly string _delimiter;

    public PatientAllergiesFileHandler(string path, string delimiter)
    {
        _path=path;

        _delimiter=delimiter;
    }

    // patient ID allergy ID
    public string ConvertPatientAllergiesToCSVFormat(PatientAllergies patientAllergies)
    {
        return string.Join(_delimiter,
            patientAllergies.patientID,
            patientAllergies.allergyID);
    }

    public PatientAllergies ConvertCSVFormatToPatientAllergies(string CSVFormat)
    {
        string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
        return new PatientAllergies(int.Parse(tokens[0]), int.Parse(tokens[1]));
    }

    public IEnumerable<PatientAllergies> ReadAll()
    {
        return File.ReadAllLines(_path)                 
            .Select(ConvertCSVFormatToPatientAllergies)   
            .ToList();
    }

    public void Save(IEnumerable<PatientAllergies> patientAllergies)
    {
        using (StreamWriter file = new StreamWriter(_path))
        {
            foreach (PatientAllergies patientAllergy in patientAllergies)
            {
                file.WriteLine(ConvertPatientAllergiesToCSVFormat(patientAllergy));
            }
        }
    }

    public void AppendLineToFile(PatientAllergies patientAllergy)
    {
        string line = ConvertPatientAllergiesToCSVFormat(patientAllergy);
        File.AppendAllText(_path, line + Environment.NewLine);
    }
}