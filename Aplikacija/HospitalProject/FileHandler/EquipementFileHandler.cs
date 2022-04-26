using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Model;

namespace HospitalProject.FileHandler
{
    public class EquipementFileHandler
    {
        private readonly string _path;

        private readonly string _delimiter;

        public EquipementFileHandler(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
        }

        
        public IEnumerable<Equipement> ReadAll()
        {
            return File.ReadAllLines(_path)
                   .Select(ConvertCSVFormatToEquipement)
                   .ToList();
        }

        private Equipement ConvertCSVFormatToEquipement(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new Equipement(int.Parse(tokens[0]),
                                   int.Parse(tokens[1]),
                                   tokens[2],
                                   (EquipementType)Enum.Parse(typeof(EquipementType), tokens[3], true));
        }

        private string ConvertEquipementToCSVFormat(Equipement equipement)
        {
            return string.Join(_delimiter,
               equipement.Id,
               equipement.Quantity,
               equipement.Name,
               equipement.EquipementType.ToString());
        }

        public void AppendLineToFile(Equipement equipement)
        {
            string line = ConvertEquipementToCSVFormat(equipement);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Save(IEnumerable<Equipement> _equipements)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (Equipement equipement in _equipements)
                {
                    file.WriteLine(ConvertEquipementToCSVFormat(equipement));
                }
            }
        }
    }
}
