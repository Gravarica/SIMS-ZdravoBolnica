﻿using System;
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
            else if (tokens.Length == 5)
            {
                
                if(tokens[4].Contains("*"))
                {
                    return new Equipement(int.Parse(tokens[0]),
                        int.Parse(tokens[1]),
                        tokens[2],
                        (EquipementType)Enum.Parse(typeof(EquipementType), tokens[3], true),
                        ConvertCSVToAlergens(tokens[4]));
                }
                else
                {
                    return new Equipement(int.Parse(tokens[0]),
                        int.Parse(tokens[1]),
                        tokens[2],
                        (EquipementType)Enum.Parse(typeof(EquipementType), tokens[3], true),
                        ConvertCSVToReplacements(tokens[4]));
                }
                
            }
            else
            {
                return new Equipement(int.Parse(tokens[0]),
                    int.Parse(tokens[1]),
                    tokens[2],
                    (EquipementType)Enum.Parse(typeof(EquipementType), tokens[3], true),
                    ConvertCSVToAlergens(tokens[4]),
                    ConvertCSVToReplacements(tokens[5]));
            }
            
        }

       
        private List<Allergies> ConvertCSVToAlergens(string CSVFormat)
        {

            string format = CSVFormat.Remove(0, 1);
            List<Allergies> alergens = new List<Allergies>();
            string[] alergenTokens = format.Split("-");
            int alergenNum = alergenTokens.Length;
            for (int i = 0; i < alergenNum; i++)
            {
                
                Allergies al = new Allergies(int.Parse(alergenTokens[i]));
                alergens.Add(al);
            }

            return alergens;
        }
        
        private List<Equipement> ConvertCSVToReplacements(string CSVFormat)
        {

            string format = CSVFormat.Remove(0, 1);
            List<Equipement> replacements = new List<Equipement>();
            string[] replacementTokens = format.Split("-");
            int replacementNum = replacementTokens.Length;
            for (int i = 0; i < replacementNum; i++)
            {
                
                Equipement al = new Equipement(int.Parse(replacementTokens[i]));
                replacements.Add(al);
            }

            return replacements;
        }

        private string ConvertAlergensToCSV(Equipement equipement)
        {
            string alergens = "*";
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
        
        private string ConvertReplacementsToCSV(Equipement equipement)
        {
            string replacements = "$";
            bool hasReplacements = false;
            if (equipement.EquipementType.Equals(EquipementType.MEDICINE))
            {
                if (equipement.Replacements.Count != 0)
                {
                    foreach (Equipement medicine in equipement.Replacements)
                    {
                        string id = medicine.Id.ToString();

                        replacements = replacements + id + "-";
                        hasReplacements = true;
                    }
                }
            }
            if(hasReplacements){replacements = replacements.Remove(replacements.Length - 1,1);}

            return replacements;
        }

        private string ConvertEquipementToCSVFormat(Equipement equipement)
        {
            if (equipement.Alergens.Count == 0 && equipement.Replacements.Count == 0)
            {
                return string.Join(_delimiter,
                    equipement.Id,
                    equipement.Quantity,
                    equipement.Name,
                    equipement.EquipementType.ToString());
            }
            else if(equipement.Replacements.Count == 0)
            {
                return string.Join(_delimiter,
                    equipement.Id,
                    equipement.Quantity,
                    equipement.Name,
                    equipement.EquipementType.ToString(),
                    ConvertAlergensToCSV(equipement)
                    );
            }
            else if (equipement.Alergens.Count == 0)
            {
                return string.Join(_delimiter,
                    equipement.Id,
                    equipement.Quantity,
                    equipement.Name,
                    equipement.EquipementType.ToString(),
                    ConvertReplacementsToCSV(equipement)
                );
            }
            else
            {
                return string.Join(_delimiter,
                    equipement.Id,
                    equipement.Quantity,
                    equipement.Name,
                    equipement.EquipementType.ToString(),
                    ConvertAlergensToCSV(equipement),
                    ConvertReplacementsToCSV(equipement)
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
