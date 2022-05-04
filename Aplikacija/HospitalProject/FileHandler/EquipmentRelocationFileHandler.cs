using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;
using Model;

namespace HospitalProject.FileHandler
{
    public class EquipmentRelocationFileHandler
    {
        private string path;
        private string delimiter;

        public EquipmentRelocationFileHandler(string path, string delimiter)
        {
            this.path = path;
            this.delimiter = delimiter;
        }
        
        public IEnumerable<EquipmentRelocation> ReadAll()
        {
            return File.ReadAllLines(path).Select(ConvertCSVFormatToRelocation).ToList();
        }

        private string ConvertRelocationToCSV(EquipmentRelocation relocation)
        {
            return string.Join(delimiter,
                relocation.Id,
                relocation.SourceRoom.Id,
                relocation.DestinationRoom.Id,
                relocation.Equipement.Id,
                relocation.Quantity,
                relocation.Date.ToString()
            );
        }

        private EquipmentRelocation ConvertCSVFormatToRelocation(string csv)
        {
            string[] tokens = csv.Split(delimiter);
            return new EquipmentRelocation(int.Parse(tokens[0]),
                new Room(int.Parse(tokens[1])),
                new Room(int.Parse(tokens[2])),
                new Equipement(int.Parse(tokens[3])),
                int.Parse(tokens[4]),
                DateOnly.Parse(tokens[5])
            );
        }
        
        public void AppendLineToFile(EquipmentRelocation relocation)
        {
            string line = ConvertRelocationToCSV(relocation);
            File.AppendAllText(path, line + Environment.NewLine);
        }
        
        public void Save(List<EquipmentRelocation> relocations) 
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                foreach (EquipmentRelocation r in relocations)
                {
                    file.WriteLine(ConvertRelocationToCSV(r));
                }
            }
        }
    }
}

