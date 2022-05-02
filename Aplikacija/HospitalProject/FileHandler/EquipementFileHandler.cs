using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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
            if (tokens.Length == 4)
            {
                return new Equipement(int.Parse(tokens[0]),
                    int.Parse(tokens[1]),
                    tokens[2],
                    (EquipementType)Enum.Parse(typeof(EquipementType), tokens[3], true));
            }
            else
            {
                return new Equipement(int.Parse(tokens[0]),
                    int.Parse(tokens[1]),
                    tokens[2],
                    (EquipementType)Enum.Parse(typeof(EquipementType), tokens[3], true),
                    ConvertCSVToAlergens(tokens[4]));
            }
            
        }

        private List<Allergies> ConvertCSVToAlergens(string CSVFormat)
        {
            List<Allergies> alergens = new List<Allergies>();
            string[] alergenTokens = CSVFormat.Split("-");
            int alergenNum = alergenTokens.Length;
            for (int i = 0; i < alergenNum; i++)
            {
                
                Allergies al = new Allergies(int.Parse(alergenTokens[i]));
                alergens.Add(al);
            }

            return alergens;
        }

        private string ConvertAlergensToCSV(Equipement equipement)
        {
            string alergens = String.Empty;
            bool hasAlergens = false;
            if (equipement.EquipementType.Equals(EquipementType.MEDICINE))
            {
                if (equipement.Alergens.Count != 0)
                {
                    foreach (Allergies alergen in equipement.Alergens)
                    {
                        string id = alergen.Id.ToString();

                        alergens = alergens + id + "-";
                        hasAlergens = true;
                    }
                }
            }
            if(hasAlergens){alergens = alergens.Remove(alergens.Length - 1,1);}

            return alergens;
        }

        private string ConvertEquipementToCSVFormat(Equipement equipement)
        {
            if (equipement.Alergens.Count == 0)
            {
                return string.Join(_delimiter,
                    equipement.Id,
                    equipement.Quantity,
                    equipement.Name,
                    equipement.EquipementType.ToString());
            }
            else
            {
                return string.Join(_delimiter,
                    equipement.Id,
                    equipement.Quantity,
                    equipement.Name,
                    equipement.EquipementType.ToString(),
                    ConvertAlergensToCSV(equipement)
                    );
            }
            
            
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
