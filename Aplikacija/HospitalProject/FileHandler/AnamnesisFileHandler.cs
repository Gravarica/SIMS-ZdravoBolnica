using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class AnamnesisFileHandler
    {
        private string _path;

        private string _delimiter;

        public AnamnesisFileHandler(String path, String delimiter)
        {
            _path = path;
            _delimiter = delimiter;
        }

        public IEnumerable<Anamnesis> ReadAll()
        {
            return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
                   .Select(ConvertCSVFormatToAnamnesis)   // 1 | 20.01.2000 12:15| 20 | 2 | 3 => app1(...) 
                   .ToList();
        }

        public Anamnesis ConvertCSVFormatToAnamnesis(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new Anamnesis(int.Parse(tokens[0]),
                                   int.Parse(tokens[1]),
                                   DateTime.Parse(tokens[2]),
                                   tokens[3]);
                                   
        }

        public string ConvertAnamnesisToCSVFormat(Anamnesis anamnesis)
        {
            return string.Join(_delimiter,
            anamnesis.Id,
            anamnesis.App.Id,
            anamnesis.Date,
            anamnesis.Description);
        }

        public void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        public void Save(IEnumerable<Anamnesis> anamneses)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (Anamnesis anamnesis in anamneses)
                {
                    file.WriteLine(ConvertAnamnesisToCSVFormat(anamnesis));
                }
            }
        }

    }
}
