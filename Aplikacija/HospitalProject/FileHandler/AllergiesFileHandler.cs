using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;

namespace HospitalProject.FileHandler;

public class AllergiesFileHandler
{
            private string _path;
    
            private string _delimiter;
    
     //path allergy.csv
            public  AllergiesFileHandler(String path, String delimiter)
            {
                _path = path;
                _delimiter = delimiter;
            }
    
            public IEnumerable<Allergies> ReadAll()
            {
                return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
                       .Select(ConvertCSVFormatToAllergies)   // 1 | Polen 
                       .ToList();
            }
    
            
            public Allergies ConvertCSVFormatToAllergies(string CSVFormat)
            {
              var tokens = CSVFormat.Split(_delimiter.ToCharArray());
                return new Allergies(int.Parse(tokens[0]),tokens[1]);
             }

        public string ConvertAllergiesToCSVFormat(Allergies allergies)
            {
                return string.Join(_delimiter,
                allergies.Id,
                allergies.Name);
            }
    
            public void AppendLineToFile(Allergies allergies)
            {
                string line = ConvertAllergiesToCSVFormat(allergies);
                File.AppendAllText(_path, Environment.NewLine + line + Environment.NewLine);
            }
    
            public void Save(IEnumerable<Allergies> allergies)
            {
                using (StreamWriter file = new StreamWriter(_path))
                {
                    foreach (Allergies allergy in allergies)
                    {
                        file.WriteLine(ConvertAllergiesToCSVFormat(allergy));
                    }
                }
            }
    
        }
